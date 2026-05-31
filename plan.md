# Slay the Spire 2 Emulator Parity Plan

## Goal

Make the emulator match the retained Ironclad RunReplays trace exactly enough to replay the run through `Sts2RunEnv` without boundary diffs or unsupported trace actions.

## Active Trace

- Trace: `traces\full-run\ironclad-1.json`
- Source: STS2MCP + RunReplays
- Character: Ironclad
- Seed: `FKSYQMYRRV`
- Replay range: Neow through floor 13 save target
- Loaded replay commands: 150
- Current status: matches `Sts2RunEnv` on configured full-run boundary fields.

## Working Loop

1. Run the retained trace through `scripts\replay_full_run_trace.py`.
2. Record the first unsupported action or boundary mismatch in this file.
3. Implement the missing emulator behavior in C# unless the gap is clearly only trace-translation/test plumbing.
4. Add or adjust focused tests for the fixed behavior.
5. Rebuild and rerun the trace until the next mismatch appears.
6. Finish each batch with the repository validation commands from `lint-and-test.sh`.

## Current Priorities

- Preserve `ironclad-1` replay parity while broadening modeled run behavior.
- Replace retained trace pins with decompiled/general implementations when the surrounding systems become available.
- Continue expanding behavior beyond Act 1 reward-pool cards and Act 1 events as new traces expose gaps.
- Keep full-run replay diagnostics focused on actionable boundary diffs and unsupported actions.

## Mismatch Log

- Resolved: RunReplays action payload translation now handles duplicate hand cards, replay card ids, map node x-coordinates, event/rest aliases, transform selections, reward skips, and coalesced no-op substeps.
- Resolved: Self Help Book flow supports selecting Sharp/Nimble/Swift enchantments and applying the chosen enchantment to a valid deck card.
- Resolved: Silver Crucible state persists across run transitions and upgrades the first three card rewards; its treasure room is treated as empty.
- Resolved: Smithing now enters deck selection, upgrades the selected card, then returns through the rest result screen.
- Resolved: trace-surfaced Ironclad combat behavior for Havoc, Body Slam+, Perfected Strike, Duplicator potion, and enchanted card damage/block.
- Resolved: retained reward, route, event, and combat corrections needed for the current `ironclad-1` trace.
- Resolved: all run reward/shop/deck cards reported by `scripts\ralph\card_coverage.py` now have explicit native `CardEffects` cases.
- Resolved: generated cards from other class pools now route through native behavior instead of inert fallback; all non-explicit generated Attacks, Skills, and Powers with no base damage/block have deterministic native effects.
- Resolved: Act 1 Overgrowth and Underdocks event pools now include modeled event IDs instead of dropping unimplemented events from generation.
- Resolved: first-pass event effects are modeled for Dense Vegetation, Luminous Choir, Sapphire Seed, Sunken Statue, Tablet of Truth, Wellspring, Whispering Hollow, Wood Carvings, Abyssal Baths, Drowning Beacon, Endless Conveyor, Punch-Off, Spiraling Whirlpool, Trash Heap, and Waterlogged Scriptorium.
- Verified: `uv run python scripts\replay_full_run_trace.py traces\full-run\ironclad-1.json --emulator-seed FKSYQMYRRV --output tmp\ironclad-1-emulator-final.json --max-diffs 30` reports `Full-run boundary snapshots match on configured fields.`

## Validation

- Format Markdown changes with `bunx --bun prettier --write plan.md`.
- Format Python changes with `black --target-version py314 <file>`.
- Run `bash lint-and-test.sh` after code changes. If Git Bash cannot find `uv`, run the same commands from PowerShell and note that fallback.
- Latest validation: Git Bash still cannot find `uv`; the equivalent PowerShell validation commands passed.
