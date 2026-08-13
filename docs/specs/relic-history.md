---
status: current
updated: 2026-08-14
source: changes/persist-former-relic-status
---
# Relic history

Security Force weapon instances persist a monotonic `wasRelic` flag. The flag
becomes true whenever the weapon is currently recognized by RimWorld as a relic,
including immediately before save serialization, and never returns to false.

The relic-history component must precede `CompStyleable` in the weapon's comp
list so it can capture a stale `Precept_Relic` before vanilla serializes or
clears that reference. This preserves the status when a removed Ideology relic
precept cannot be resolved after loading.

Canicula projectiles force their normal damage to the target's brain when the
launcher's primary weapon is either a current relic or has the persisted former
relic flag.

Items whose relic precept reference was already lost in an older save cannot be
identified retroactively and begin with the flag unset.
