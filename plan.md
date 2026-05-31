# Slay the Spire 2 Emulator Parity Plan

## Goal

Keep expanding `Sts2RunEnv` and combat parity so new RunReplays traces can be replayed without boundary diffs or unsupported trace actions.

## Working Loop

1. Convert or select the newest retained RunReplays trace.
2. Record the first unsupported action or boundary mismatch in this file.
3. Implement the missing emulator behavior in C# unless the gap is clearly only trace-translation/test plumbing.
4. Add or adjust focused tests for the fixed behavior.
5. Rebuild and rerun the trace until the next mismatch appears.
6. Finish each batch with the repository validation commands from `lint-and-test.sh`.

## Current Priorities

- Replace retained trace pins with decompiled/general implementations when the surrounding systems become available.
- Continue expanding behavior beyond Act 1 reward-pool cards and Act 1 events as new traces expose gaps.
- Keep full-run replay diagnostics focused on actionable boundary diffs and unsupported actions.

## Mismatch Log

- Resolved: all run reward/shop/deck cards reported by `scripts\ralph\card_coverage.py` now have explicit native `CardEffects` cases.
- Resolved: generated cards from other class pools now route through native behavior instead of inert fallback; all non-explicit generated Attacks, Skills, and Powers with no base damage/block have deterministic native effects.
- Resolved: Act 1 Overgrowth and Underdocks event pools now include modeled event IDs instead of dropping unimplemented events from generation.
- Resolved: first-pass event effects are modeled for Dense Vegetation, Luminous Choir, Sapphire Seed, Sunken Statue, Tablet of Truth, Wellspring, Whispering Hollow, Wood Carvings, Abyssal Baths, Drowning Beacon, Endless Conveyor, Punch-Off, Spiraling Whirlpool, Trash Heap, and Waterlogged Scriptorium.

## Validation

- Format Markdown changes with `bunx --bun prettier --write plan.md`.
- Format Python changes with `black --target-version py314 <file>`.
- Run `bash lint-and-test.sh` after code changes. If Git Bash cannot find `uv`, run the same commands from PowerShell and note that fallback.
- Latest validation: Git Bash still cannot find `uv`; the equivalent PowerShell validation commands passed.
