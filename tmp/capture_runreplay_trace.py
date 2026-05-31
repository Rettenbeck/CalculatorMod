from __future__ import annotations

import argparse
import json
import re
import time
from datetime import datetime, timezone
from pathlib import Path
from typing import Any
from urllib.request import Request, urlopen

BASE_URL = "http://localhost:15526"
STATE_PATH = Path("tmp/ironclad2_capture_state.json")
OUTPUT_PATH = Path("traces/full-run/ironclad-2.json")


def request_json(
    method: str,
    path: str,
    payload: dict[str, Any] | None = None,
    timeout: float = 10.0,
) -> dict[str, Any]:
    body = None if payload is None else json.dumps(payload).encode("utf-8")
    headers = {"Content-Type": "application/json"} if body is not None else {}
    request = Request(f"{BASE_URL}{path}", data=body, headers=headers, method=method)
    with urlopen(request, timeout=timeout) as response:
        return json.loads(response.read().decode("utf-8"))


def post_action(action: str, **kwargs: Any) -> dict[str, Any]:
    return request_json("POST", "/api/v1/singleplayer", {"action": action, **kwargs})


def get_state() -> dict[str, Any]:
    return request_json("GET", "/api/v1/singleplayer")


def get_replay_status() -> dict[str, Any]:
    return post_action("get_replay_status")


def latest_ironclad_replay() -> dict[str, Any]:
    replays = post_action("get_replays")
    entries = [
        entry
        for entry in replays.get("replays", [])
        if entry.get("character_id") == "IRONCLAD" and not entry.get("is_sample")
    ]
    if not entries:
        raise RuntimeError("No non-sample Ironclad replays found.")
    entries.sort(
        key=lambda entry: (
            str(entry.get("saved_at") or ""),
            int(entry.get("floor") or 0),
        ),
        reverse=True,
    )
    return entries[0]


def compact_state(state: dict[str, Any]) -> dict[str, Any]:
    player = state.get("player") or {}
    battle = state.get("battle") or {}
    return {
        "state_type": state.get("state_type"),
        "run": state.get("run") or {},
        "player": {
            "character": player.get("character"),
            "hp": player.get("hp"),
            "max_hp": player.get("max_hp"),
            "block": player.get("block"),
            "energy": player.get("energy"),
            "gold": player.get("gold"),
            "deck_size": len(player.get("deck") or []),
            "relics": simplify_named_list(player.get("relics") or []),
            "potions": simplify_named_list(player.get("potions") or []),
            "hand": simplify_cards(player.get("hand") or []),
        },
        "battle": (
            {
                "round": battle.get("round"),
                "turn": battle.get("turn"),
                "is_play_phase": battle.get("is_play_phase"),
                "enemies": [
                    {
                        "entity_id": enemy.get("entity_id"),
                        "name": enemy.get("name"),
                        "hp": enemy.get("hp"),
                        "max_hp": enemy.get("max_hp"),
                        "block": enemy.get("block"),
                        "intents": enemy.get("intents"),
                        "status": enemy.get("status"),
                    }
                    for enemy in battle.get("enemies") or []
                ],
            }
            if battle
            else None
        ),
        "event": state.get("event") or {},
        "rewards": state.get("rewards") or {},
        "card_reward": state.get("card_reward") or {},
        "map": state.get("map") or {},
        "shop": state.get("shop") or {},
        "rest": state.get("rest") or {},
        "rest_site": state.get("rest_site") or {},
        "treasure": state.get("treasure") or {},
        "bundle_select": state.get("bundle_select") or {},
        "menu_screen": state.get("menu_screen"),
        "options": state.get("options"),
    }


def simplify_named_list(items: list[Any]) -> list[dict[str, Any]]:
    result = []
    for item in items:
        if isinstance(item, dict):
            result.append(
                {
                    "id": item.get("id"),
                    "name": item.get("name"),
                    "slot": item.get("slot"),
                    "counter": item.get("counter"),
                }
            )
        else:
            result.append({"value": item})
    return result


def simplify_cards(cards: list[dict[str, Any]]) -> list[dict[str, Any]]:
    return [
        {
            "index": card.get("index"),
            "id": card.get("id"),
            "name": card.get("name"),
            "type": card.get("type"),
            "cost": card.get("cost"),
            "description": card.get("description"),
            "can_play": card.get("can_play"),
            "target_type": card.get("target_type"),
            "is_upgraded": card.get("is_upgraded"),
        }
        for card in cards
    ]


def parse_command(raw: str) -> dict[str, Any]:
    command = raw.split(" || ", 1)[0]
    text = command.split(" # ", 1)[0].strip()
    parts = text.split()
    name = parts[0] if parts else ""
    payload: dict[str, Any] = {"run_replay_command": raw}
    try:
        if name == "ChooseEventOption":
            payload.update(action="choose_event_option", index=int(parts[1]))
        elif name == "MoveToMapCoord":
            payload.update(action="choose_map_node", x=int(parts[1]))
        elif name == "PlayCard":
            payload.update(action="play_card", combat_card_id=int(parts[1]))
            if len(parts) > 2:
                payload["target"] = int(parts[2])
        elif name == "EndTurn":
            payload.update(action="end_turn")
        elif name == "ClaimReward":
            payload.update(action="claim_reward", index=int(parts[1]))
        elif name == "TakeCard":
            payload.update(action="take_card")
            if len(parts) > 1 and re.fullmatch(r"-?\d+", parts[1]):
                payload["index"] = int(parts[1])
        elif name == "SelectGridCard":
            payload.update(
                action="select_grid_card",
                indices=[
                    int(part) for part in parts[1:] if re.fullmatch(r"-?\d+", part)
                ],
            )
        elif name == "SelectHandCards":
            payload.update(
                action="select_hand_cards",
                indices=[
                    int(part) for part in parts[1:] if re.fullmatch(r"-?\d+", part)
                ],
            )
        elif name == "ChooseRestSiteOption":
            payload.update(
                action="choose_rest_site_option",
                option=parts[1] if len(parts) > 1 else "",
            )
        elif name == "UsePotion":
            payload.update(action="use_potion", slot=int(parts[1]))
        elif name == "DiscardPotion":
            payload.update(action="discard_potion", slot=int(parts[1]))
        elif name == "OpenShop":
            payload.update(action="open_shop")
        elif name == "CloseShop":
            payload.update(action="close_shop")
        elif name == "BuyCard":
            payload.update(action="buy_card", name=text[len("BuyCard") :].strip())
        elif name == "BuyRelic":
            payload.update(action="buy_relic", name=text[len("BuyRelic") :].strip())
        elif name == "BuyPotion":
            payload.update(action="buy_potion", name=text[len("BuyPotion") :].strip())
        elif name == "BuyCardRemoval":
            payload.update(action="buy_card_removal")
        elif name in {"ProceedToMap", "Proceed"}:
            payload.update(action="proceed")
        elif name == "OpenChest":
            payload.update(action="open_chest")
        elif name == "TakeChestRelic":
            payload.update(action="take_chest_relic")
        elif name == "CrystalSphereClick":
            payload.update(action="crystal_sphere_click")
        elif name == "SelectCardFromScreen":
            index = (
                int(parts[1])
                if len(parts) > 1 and re.fullmatch(r"-?\d+", parts[1])
                else 0
            )
            payload.update(action="select_card_from_screen", index=index)
        elif name == "ClickGridCard":
            payload.update(action="click_grid_card", index=int(parts[1]))
        elif name == "ConfirmGridSelection":
            payload.update(action="confirm_selection")
        elif name == "CancelGridSelection":
            payload.update(action="cancel_selection")
        elif name == "SkipRewards":
            payload.update(action="skip_card_reward")
        elif name == "ProceedToNextAct":
            payload.update(action="proceed_to_next_act")
        else:
            payload.update(action=name)
    except IndexError, ValueError:
        payload.update(action=name, parse_error=True)
    return payload


def append_snapshot(
    trace: list[dict[str, Any]],
    step: int,
    action: dict[str, Any] | None,
    post_result: dict[str, Any] | None,
    state: dict[str, Any],
    note: str | None = None,
) -> None:
    trace.append(
        {
            "step": step,
            "action": action,
            "post_result": post_result,
            "summary": compact_state(state),
            "raw_state": state,
            "note": note,
        }
    )


def read_commands(log_path: Path) -> list[str]:
    return [
        line.strip()
        for line in log_path.read_text(encoding="utf-8").splitlines()
        if line.strip() and not line.startswith("#")
    ]


def write_json(path: Path, payload: dict[str, Any]) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps(payload, indent=2) + "\n", encoding="utf-8")


def initialize_capture() -> dict[str, Any]:
    entry = latest_ironclad_replay()
    log_path = Path(str(entry["minimal_log_path"]).replace("\\", "/"))
    raw_commands = read_commands(log_path)
    start = post_action("start_replay", target=entry["target"])
    if start.get("status") != "ok":
        raise RuntimeError(f"start_replay failed: {start}")

    time.sleep(0.5)
    payload = {
        "source": "sts2mcp_runreplays",
        "kind": "full_run",
        "base_url": BASE_URL,
        "seed": entry.get("seed"),
        "character": entry.get("character_id"),
        "name": "ironclad-2",
        "replay_target": entry.get("target"),
        "replay_log_path": str(log_path),
        "raw_command_count": len(raw_commands),
        "loaded_command_count": len(raw_commands),
        "captured_at": datetime.now(timezone.utc).isoformat(),
        "trace": [],
    }
    append_snapshot(payload["trace"], 0, None, start, get_state(), "runreplays_start")
    state = {
        "done": False,
        "last_consumed": 0,
        "raw_commands": raw_commands,
        "parsed_commands": [parse_command(command) for command in raw_commands],
    }
    write_json(OUTPUT_PATH, payload)
    write_json(STATE_PATH, state)
    return state


def load_capture(fresh: bool) -> tuple[dict[str, Any], dict[str, Any]]:
    if fresh:
        OUTPUT_PATH.unlink(missing_ok=True)
        STATE_PATH.unlink(missing_ok=True)
    state = (
        initialize_capture()
        if not STATE_PATH.exists()
        else json.loads(STATE_PATH.read_text())
    )
    payload = json.loads(OUTPUT_PATH.read_text(encoding="utf-8"))
    return state, payload


def run_chunk(max_seconds: float, fresh: bool) -> int:
    state, payload = load_capture(fresh)
    parsed_commands = state["parsed_commands"]
    last_consumed = int(state["last_consumed"])
    started = time.monotonic()
    progressed = False

    while time.monotonic() - started < max_seconds:
        status = get_replay_status()
        consumed = int(status.get("consumed_count") or 0)
        if consumed > last_consumed:
            current_state = get_state()
            for count in range(last_consumed + 1, consumed + 1):
                post_result = dict(status)
                post_result["consumed_count"] = count
                action = (
                    parsed_commands[count - 1]
                    if count - 1 < len(parsed_commands)
                    else None
                )
                append_snapshot(
                    payload["trace"], count, action, post_result, current_state
                )
            last_consumed = consumed
            state["last_consumed"] = last_consumed
            progressed = True
            print(
                f"captured {last_consumed}/{len(parsed_commands)} "
                f"current={status.get('current_command')}",
                flush=True,
            )
            write_json(OUTPUT_PATH, payload)
            write_json(STATE_PATH, state)

        loaded = int(status.get("loaded_count") or len(parsed_commands))
        if not status.get("is_active") and consumed >= loaded:
            append_snapshot(
                payload["trace"],
                last_consumed + 1,
                None,
                status,
                get_state(),
                "runreplays_done",
            )
            payload["loaded_command_count"] = loaded
            state["done"] = True
            write_json(OUTPUT_PATH, payload)
            write_json(STATE_PATH, state)
            print(f"done {last_consumed}/{len(parsed_commands)}", flush=True)
            return 0

        time.sleep(0.1)

    status = get_replay_status()
    print(
        json.dumps(
            {
                "done": False,
                "progressed": progressed,
                "last_consumed": last_consumed,
                "loaded_count": status.get("loaded_count"),
                "pending_count": status.get("pending_count"),
                "current_command": status.get("current_command"),
            },
            indent=2,
        ),
        flush=True,
    )
    return 1


def main() -> None:
    parser = argparse.ArgumentParser()
    parser.add_argument("--fresh", action="store_true")
    parser.add_argument("--max-seconds", type=float, default=25.0)
    args = parser.parse_args()
    raise SystemExit(run_chunk(args.max_seconds, args.fresh))


if __name__ == "__main__":
    main()
