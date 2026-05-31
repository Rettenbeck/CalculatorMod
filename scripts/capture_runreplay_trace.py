from __future__ import annotations

import argparse
import json
import re
import sys
import time
from datetime import UTC, datetime
from pathlib import Path
from typing import Any
from urllib.request import Request, urlopen

ROOT = Path(__file__).resolve().parents[1]
sys.path.insert(0, str(ROOT / "scripts"))

from trace_real_game_run import compact_state  # noqa: E402

BASE_URL = "http://localhost:15526"
API_PATH = "/api/v1/singleplayer"


def request_json(
    method: str,
    path: str = API_PATH,
    payload: dict[str, Any] | None = None,
    timeout: float = 10.0,
) -> dict[str, Any]:
    body = None if payload is None else json.dumps(payload).encode("utf-8")
    headers = {"Content-Type": "application/json"} if body is not None else {}
    request = Request(BASE_URL + path, data=body, headers=headers, method=method)
    with urlopen(request, timeout=timeout) as response:
        return json.loads(response.read().decode("utf-8"))


def get_state() -> dict[str, Any]:
    return request_json("GET")


def post_action(payload: dict[str, Any]) -> dict[str, Any]:
    return request_json("POST", payload=payload)


def load_replay_commands(path: str) -> list[str]:
    commands: list[str] = []
    for line in Path(path).read_text(encoding="utf-8").splitlines():
        line = line.strip()
        if not line or line.startswith("#"):
            continue
        if not is_runreplays_parseable_command(line):
            continue
        commands.append(line)
    return commands


def is_runreplays_parseable_command(command: str) -> bool:
    command_text = command.split(" || ", 1)[0].split(" # ", 1)[0]
    return command_text.startswith(
        (
            "PlayCard ",
            "EndTurn",
            "MoveToMapCoord ",
            "ChooseRestSiteOption ",
            "RestSiteOption ",
            "ChooseEventOption ",
            "ClaimReward ",
            "TakeCard ",
            "SelectGridCard ",
            "SelectHandCards ",
            "OpenShop",
            "OpenFakeShop",
            "BuyCard ",
            "BuyRelic ",
            "BuyCardRemoval",
            "BuyPotion ",
            "UsePotion ",
            "DiscardPotion",
            "ProceedToNextAct",
            "OpenChest",
            "TakeChestRelic",
            "CrystalSphereClick",
            "SelectCardFromScreen ",
            "ProceedToMap",
            "CloseShop",
            "ClickGridCard ",
            "ConfirmGridSelection",
            "CancelGridSelection",
            "SkipRewards",
        ),
    )


def action_from_command(command: str) -> dict[str, Any]:
    command_text = command.split(" || ", 1)[0]
    payload: dict[str, Any] = {"run_replay_command": command}

    if match := re.match(r"^PlayCard\s+(\d+)(?:\s+(\d+))?", command_text):
        payload["action"] = "play_card"
        payload["combat_card_id"] = int(match.group(1))
        if match.group(2) is not None:
            payload["target"] = int(match.group(2))
        return payload
    if command_text.startswith("EndTurn"):
        payload["action"] = "end_turn"
        return payload
    if match := re.match(r"^MoveToMapCoord\s+(-?\d+)", command_text):
        payload["action"] = "choose_map_node"
        payload["x"] = int(match.group(1))
        return payload
    if match := re.match(r"^ChooseEventOption\s+(-?\d+)", command_text):
        payload["action"] = "choose_event_option"
        payload["index"] = int(match.group(1))
        return payload
    if match := re.match(r"^ClaimReward\s+(-?\d+)", command_text):
        payload["action"] = "claim_reward"
        payload["index"] = int(match.group(1))
        return payload
    if match := re.match(r"^TakeCard\s+(\S+)", command_text):
        choice = match.group(1).lower()
        if choice == "skip":
            payload["action"] = "skip_card_reward"
        else:
            payload["action"] = "take_card"
            payload["index"] = int(choice) if choice.lstrip("-").isdigit() else choice
        return payload
    if match := re.match(r"^UsePotion\s+(\d+)(?:\s+(\d+))?", command_text):
        payload["action"] = "use_potion"
        payload["slot"] = int(match.group(1))
        if match.group(2) is not None:
            payload["target"] = int(match.group(2))
        return payload
    if command_text == "OpenShop":
        payload["action"] = "open_shop"
        return payload
    if command_text.startswith("CloseShop"):
        payload["action"] = "shop_option"
        payload["option"] = "proceed"
        return payload
    if match := re.match(r"^BuyCard\s+(.+)", command_text):
        payload["action"] = "buy_card"
        payload["name"] = match.group(1)
        return payload
    if match := re.match(r"^BuyRelic\s+(.+)", command_text):
        payload["action"] = "buy_relic"
        payload["name"] = match.group(1)
        return payload
    if match := re.match(r"^BuyPotion\s+(.+)", command_text):
        payload["action"] = "buy_potion"
        payload["name"] = match.group(1)
        return payload
    if command_text.startswith("BuyCardRemoval"):
        payload["action"] = "buy_card_removal"
        return payload
    if match := re.match(r"^DiscardPotion(?:\s+(\d+))?", command_text):
        payload["action"] = "discard_potion"
        if match.group(1) is not None:
            payload["slot"] = int(match.group(1))
        return payload
    if command_text.startswith("OpenChest"):
        payload["action"] = "open_chest"
        return payload
    if command_text.startswith("TakeChestRelic"):
        payload["action"] = "take_chest_relic"
        return payload
    if match := re.match(
        r"^(?:ChooseRestSiteOption|RestSiteOption)\s+(\S+)",
        command_text,
    ):
        payload["action"] = "choose_rest_site_option"
        payload["option"] = match.group(1)
        return payload
    if match := re.match(r"^SelectGridCard\s+(.+)", command_text):
        indices = [int(value) for value in re.findall(r"-?\d+", match.group(1))]
        payload["action"] = "select_grid_card"
        payload["indices"] = indices
        return payload
    if command_text.startswith("ConfirmGridSelection"):
        payload["action"] = "confirm_selection"
        return payload
    if command_text.startswith("ProceedToNextAct"):
        payload["action"] = "proceed_to_next_act"
        return payload

    payload["action"] = command_text.split(" ", 1)[0]
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
        },
    )


def main() -> None:
    parser = argparse.ArgumentParser()
    parser.add_argument("--target", required=True)
    parser.add_argument("--seed")
    parser.add_argument("--floor", type=int)
    parser.add_argument("--start-floor", type=int)
    parser.add_argument("--name", required=True)
    parser.add_argument("--output", type=Path, required=True)
    parser.add_argument("--poll", type=float, default=0.1)
    parser.add_argument("--stall-timeout", type=float, default=90.0)
    parser.add_argument("--format", choices=["pretty", "compact"], default="pretty")
    args = parser.parse_args()

    replays = post_action({"action": "get_replays"})
    entry = next(
        replay for replay in replays["replays"] if replay.get("target") == args.target
    )
    commands = load_replay_commands(entry["minimal_log_path"])

    start_payload: dict[str, Any] = {"action": "start_replay", "target": args.target}
    if args.seed is not None:
        start_payload = {"action": "start_replay", "seed": args.seed}
        if args.floor is not None:
            start_payload["floor"] = args.floor
        if args.start_floor is not None:
            start_payload["start_floor"] = args.start_floor
    start_result = post_action(start_payload)
    trace: list[dict[str, Any]] = []
    append_snapshot(trace, 0, None, start_result, get_state(), note="runreplays_start")

    status = post_action({"action": "get_replay_status"})
    last_consumed = int(status.get("consumed_count") or 0)
    last_progress = time.monotonic()

    while True:
        time.sleep(args.poll)
        status = post_action({"action": "get_replay_status"})
        consumed = int(status.get("consumed_count") or 0)
        if consumed > last_consumed:
            state = get_state()
            for index in range(last_consumed + 1, consumed + 1):
                command = commands[index - 1] if index - 1 < len(commands) else None
                append_snapshot(
                    trace,
                    index,
                    action_from_command(command) if command is not None else None,
                    status,
                    state,
                )
            last_consumed = consumed
            last_progress = time.monotonic()
            print(
                f"captured {last_consumed}/{status.get('loaded_count')} "
                f"pending={status.get('pending_count')} current={status.get('current_command')}",
                flush=True,
            )

        if not status.get("is_active"):
            append_snapshot(
                trace,
                last_consumed + 1,
                None,
                status,
                get_state(),
                note="runreplays_complete",
            )
            break

        if time.monotonic() - last_progress > args.stall_timeout:
            append_snapshot(
                trace,
                last_consumed + 1,
                None,
                status,
                get_state(),
                note="runreplays_stall",
            )
            break

    result = {
        "source": "sts2mcp",
        "kind": "full_run",
        "base_url": BASE_URL,
        "seed": entry.get("seed"),
        "character": entry.get("character_id"),
        "name": args.name,
        "replay_target": entry.get("target"),
        "replay_log_path": entry.get("minimal_log_path"),
        "raw_command_count": len(commands),
        "loaded_command_count": status.get("loaded_count"),
        "captured_at": datetime.now(UTC).isoformat(),
        "trace": trace,
    }
    text = json.dumps(result, indent=None if args.format == "compact" else 2)
    args.output.parent.mkdir(parents=True, exist_ok=True)
    args.output.write_text(text + "\n", encoding="utf-8")


if __name__ == "__main__":
    main()
