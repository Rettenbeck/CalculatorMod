# Slay the Spire 2 Emulator Parity Plan

## Goal

Use fresh STS2MCP `.replay` captures to drive `Sts2RunEnv` parity without relying on obsolete RunReplays traces or retained trace fixtures.

## Working Loop

1. Capture a new STS2MCP replay and convert it into a full-run trace.
2. Record the first unsupported action or boundary mismatch in this file.
3. Implement the missing emulator behavior in C# unless the gap is clearly only trace-translation/test plumbing.
4. Add or adjust focused tests for the fixed behavior.
5. Rebuild and rerun the trace until the next mismatch appears.
6. Finish each batch with `bash lint-and-test.sh`.

## Current Priorities

- Wait for new STS2MCP replay traces before doing more trace-specific parity work.
- Prefer decompiled reward, relic, event, map, and combat-start semantics over new retained trace pins.
- Model special card reward alternatives such as Pael's Wing sacrifice as first-class run actions so they do not get confused with taking a real card.

## Mismatch Log

- No active retained traces. The old `ironclad-*` trace fixtures were removed because they came from the previous RunReplays workflow or contained bad reference state.

## Validation

- Format Markdown changes with `bunx --bun prettier --write plan.md`.
- Format Python changes with `black --target-version py314 <file>`.
- Run `bash lint-and-test.sh` after code changes.
