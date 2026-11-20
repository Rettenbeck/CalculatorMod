using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Hooks;
using System.Collections.Generic;

namespace CalculatorMod;





// [HarmonyPatch(typeof(Hook), "BeforeCombatStart")] public static class HookPatch_BeforeCombatStart {
//     public static void Postfix(IRunState runState, CombatState? combatState) { CalcManager.Update(runState); } }

// [HarmonyPatch(typeof(Hook), "AfterRoomEntered")] public static class HookPatch_AfterRoomEntered {
//     public static void Postfix(IRunState runState, AbstractRoom room) { CalcManager.Update(runState); } }

// [HarmonyPatch(typeof(Hook), "AfterCombatEnd")] public static class HookPatch_AfterCombatEnd {
//     public static void Postfix(IRunState runState, AbstractRoom room) { CalcManager.Update(runState); } }

// [HarmonyPatch(typeof(Hook), "AfterCombatVictory")] public static class HookPatch_AfterCombatVictory {
//     public static void Postfix(IRunState runState, AbstractRoom room) { CalcManager.Update(runState); } }

// [HarmonyPatch(typeof(Hook), "BeforeCardRemoved")] public static class HookPatch_BeforeCardRemoved {
//     public static void Postfix(IRunState runState, CardModel card) { CalcManager.Update(runState); } }


[HarmonyPatch(typeof(Hook), "ModifyDamageInternal")] public static class HookPatch_ModifyDamageInternal { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "ModifyGeneratedMap")] public static class HookPatch_ModifyGeneratedMap { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ModifyGeneratedMapLate")] public static class HookPatch_ModifyGeneratedMapLate { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "AfterActEntered")] public static class HookPatch_AfterActEntered { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "AfterArrowDislodged")] public static class HookPatch_AfterArrowDislodged { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterArrowImpaled")] public static class HookPatch_AfterArrowImpaled { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterArrowShot")] public static class HookPatch_AfterArrowShot { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterAttack")] public static class HookPatch_AfterAttack { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterAutoPostPlayPhaseEntered")] public static class HookPatch_AfterAutoPostPlayPhaseEntered { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterAutoPrePlayPhaseEntered")] public static class HookPatch_AfterAutoPrePlayPhaseEntered { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterBlockBroken")] public static class HookPatch_AfterBlockBroken { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterBlockCleared")] public static class HookPatch_AfterBlockCleared { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterBlockGained")] public static class HookPatch_AfterBlockGained { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterCardChangedPiles")] public static class HookPatch_AfterCardChangedPiles { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "AfterCardDiscarded")] public static class HookPatch_AfterCardDiscarded { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterCardDrawn")] public static class HookPatch_AfterCardDrawn { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterCardEnteredCombat")] public static class HookPatch_AfterCardEnteredCombat { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterCardExhausted")] public static class HookPatch_AfterCardExhausted { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterCardGeneratedForCombat")] public static class HookPatch_AfterCardGeneratedForCombat { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterCardPlayed")] public static class HookPatch_AfterCardPlayed { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterCombatEnd")] public static class HookPatch_AfterCombatEnd { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "AfterCombatVictory")] public static class HookPatch_AfterCombatVictory { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "AfterCreatureAddedToCombat")] public static class HookPatch_AfterCreatureAddedToCombat { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterCurrentHpChanged")] public static class HookPatch_AfterCurrentHpChanged { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "AfterDamageGiven")] public static class HookPatch_AfterDamageGiven { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterDamageReceived")] public static class HookPatch_AfterDamageReceived { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "AfterDeath")] public static class HookPatch_AfterDeath { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "AfterDiedToDoom")] public static class HookPatch_AfterDiedToDoom { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterEnergyReset")] public static class HookPatch_AfterEnergyReset { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterEnergySpent")] public static class HookPatch_AfterEnergySpent { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterFlush")] public static class HookPatch_AfterFlush { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterForge")] public static class HookPatch_AfterForge { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterGoldGained")] public static class HookPatch_AfterGoldGained { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "AfterHandEmptied")] public static class HookPatch_AfterHandEmptied { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterItemPurchased")] public static class HookPatch_AfterItemPurchased { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "AfterMapGenerated")] public static class HookPatch_AfterMapGenerated { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "AfterModifyingBlockAmount")] public static class HookPatch_AfterModifyingBlockAmount { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterModifyingCardPlayCount")] public static class HookPatch_AfterModifyingCardPlayCount { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterModifyingCardRewardOptions")] public static class HookPatch_AfterModifyingCardRewardOptions { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "AfterModifyingDamageAmount")] public static class HookPatch_AfterModifyingDamageAmount { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "AfterModifyingEnergyGain")] public static class HookPatch_AfterModifyingEnergyGain { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterModifyingGoldGained")] public static class HookPatch_AfterModifyingGoldGained { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "AfterModifyingHandDraw")] public static class HookPatch_AfterModifyingHandDraw { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterModifyingHpLostAfterOsty")] public static class HookPatch_AfterModifyingHpLostAfterOsty { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "AfterModifyingHpLostBeforeOsty")] public static class HookPatch_AfterModifyingHpLostBeforeOsty { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "AfterModifyingOrbPassiveTriggerCount")] public static class HookPatch_AfterModifyingOrbPassiveTriggerCount { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterModifyingPowerAmountGiven")] public static class HookPatch_AfterModifyingPowerAmountGiven { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterModifyingPowerAmountReceived")] public static class HookPatch_AfterModifyingPowerAmountReceived { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterModifyingRewards")] public static class HookPatch_AfterModifyingRewards { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "AfterOrbChanneled")] public static class HookPatch_AfterOrbChanneled { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterOrbEvoked")] public static class HookPatch_AfterOrbEvoked { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterOstyRevived")] public static class HookPatch_AfterOstyRevived { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterPlayerTurnStart")] public static class HookPatch_AfterPlayerTurnStart { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterPotionDiscarded")] public static class HookPatch_AfterPotionDiscarded { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "AfterPotionProcured")] public static class HookPatch_AfterPotionProcured { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "AfterPotionUsed")] public static class HookPatch_AfterPotionUsed { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "AfterPowerAmountChanged")] public static class HookPatch_AfterPowerAmountChanged { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterPreventingBlockClear")] public static class HookPatch_AfterPreventingBlockClear { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterPreventingDeath")] public static class HookPatch_AfterPreventingDeath { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "AfterPreventingDraw")] public static class HookPatch_AfterPreventingDraw { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterRestSiteHeal")] public static class HookPatch_AfterRestSiteHeal { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "AfterRestSiteSmith")] public static class HookPatch_AfterRestSiteSmith { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "AfterRewardTaken")] public static class HookPatch_AfterRewardTaken { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "AfterRoomEntered")] public static class HookPatch_AfterRoomEntered { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "AfterShuffle")] public static class HookPatch_AfterShuffle { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterSideTurnEnd")] public static class HookPatch_AfterSideTurnEnd { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterSideTurnStart")] public static class HookPatch_AfterSideTurnStart { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterStarsGained")] public static class HookPatch_AfterStarsGained { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterStarsSpent")] public static class HookPatch_AfterStarsSpent { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterSummon")] public static class HookPatch_AfterSummon { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "AfterTakingExtraTurn")] public static class HookPatch_AfterTakingExtraTurn { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "BeforeAttack")] public static class HookPatch_BeforeAttack { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "BeforeBlockGained")] public static class HookPatch_BeforeBlockGained { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "BeforeCardAutoPlayed")] public static class HookPatch_BeforeCardAutoPlayed { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "BeforeCardPlayed")] public static class HookPatch_BeforeCardPlayed { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "BeforeCardRemoved")] public static class HookPatch_BeforeCardRemoved { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "BeforeCombatRewardOffered")] public static class HookPatch_BeforeCombatRewardOffered { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "BeforeCombatStart")] public static class HookPatch_BeforeCombatStart { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "BeforeDamageReceived")] public static class HookPatch_BeforeDamageReceived { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "BeforeDeath")] public static class HookPatch_BeforeDeath { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "BeforeFlush")] public static class HookPatch_BeforeFlush { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "BeforeHandDraw")] public static class HookPatch_BeforeHandDraw { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "BeforePotionUsed")] public static class HookPatch_BeforePotionUsed { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "BeforePowerAmountChanged")] public static class HookPatch_BeforePowerAmountChanged { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "BeforeRoomEntered")] public static class HookPatch_BeforeRoomEntered { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "BeforeSideTurnEnd")] public static class HookPatch_BeforeSideTurnEnd { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "BeforeSideTurnStart")] public static class HookPatch_BeforeSideTurnStart { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ShouldAddToDeck")] public static class HookPatch_ShouldAddToDeck { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ShouldAfflict")] public static class HookPatch_ShouldAfflict { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ShouldAllowAncient")] public static class HookPatch_ShouldAllowAncient { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ShouldAllowFreeTravel")] public static class HookPatch_ShouldAllowFreeTravel { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ShouldAllowHitting")] public static class HookPatch_ShouldAllowHitting { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ShouldAllowMerchantCardRemoval")] public static class HookPatch_ShouldAllowMerchantCardRemoval { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ShouldAllowSelectingMoreCardRewards")] public static class HookPatch_ShouldAllowSelectingMoreCardRewards { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ShouldAllowTargeting")] public static class HookPatch_ShouldAllowTargeting { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ShouldClearBlock")] public static class HookPatch_ShouldClearBlock { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ShouldCreatureBeRemovedFromCombatAfterDeath")] public static class HookPatch_ShouldCreatureBeRemovedFromCombatAfterDeath { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ShouldDie")] public static class HookPatch_ShouldDie { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "ShouldDisableRemainingRestSiteOptions")] public static class HookPatch_ShouldDisableRemainingRestSiteOptions { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ShouldDraw")] public static class HookPatch_ShouldDraw { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ShouldEtherealTrigger")] public static class HookPatch_ShouldEtherealTrigger { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ShouldFlush")] public static class HookPatch_ShouldFlush { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ShouldForcePotionReward")] public static class HookPatch_ShouldForcePotionReward { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ShouldGainStars")] public static class HookPatch_ShouldGainStars { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ShouldGenerateTreasure")] public static class HookPatch_ShouldGenerateTreasure { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ShouldPayExcessEnergyCostWithStars")] public static class HookPatch_ShouldPayExcessEnergyCostWithStars { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ShouldPlay")] public static class HookPatch_ShouldPlay { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ShouldPlayerResetEnergy")] public static class HookPatch_ShouldPlayerResetEnergy { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ShouldProceedToNextMapPoint")] public static class HookPatch_ShouldProceedToNextMapPoint { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ShouldProcurePotion")] public static class HookPatch_ShouldProcurePotion { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "ShouldRefillMerchantEntry")] public static class HookPatch_ShouldRefillMerchantEntry { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ShouldStopCombatFromEnding")] public static class HookPatch_ShouldStopCombatFromEnding { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ShouldTakeExtraTurn")] public static class HookPatch_ShouldTakeExtraTurn { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "TryModifyCardRewardOptions")] public static class HookPatch_TryModifyCardRewardOptions { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "TryReplaceDefaultRoomRewards")] public static class HookPatch_TryReplaceDefaultRoomRewards { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ModifyCardRewardCreationOptions")] public static class HookPatch_ModifyCardRewardCreationOptions { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ModifyCardPlayResultLocation")] public static class HookPatch_ModifyCardPlayResultLocation { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ModifyCardBeingAddedToDeck")] public static class HookPatch_ModifyCardBeingAddedToDeck { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ModifyMerchantCardRarity")] public static class HookPatch_ModifyMerchantCardRarity { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ModifyUnblockedDamageTarget")] public static class HookPatch_ModifyUnblockedDamageTarget { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ModifyPreBlockDamageTarget")] public static class HookPatch_ModifyPreBlockDamageTarget { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ModifyAttackHitCount")] public static class HookPatch_ModifyAttackHitCount { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ModifyBlock")] public static class HookPatch_ModifyBlock { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ModifyCardRewardUpgradeOdds")] public static class HookPatch_ModifyCardRewardUpgradeOdds { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ModifyDamage")] public static class HookPatch_ModifyDamage { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "ModifyEnergyCostInCombat")] public static class HookPatch_ModifyEnergyCostInCombat { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ModifyEnergyGain")] public static class HookPatch_ModifyEnergyGain { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ModifyGoldGained")] public static class HookPatch_ModifyGoldGained { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "ModifyHandDraw")] public static class HookPatch_ModifyHandDraw { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ModifyHpLost")] public static class HookPatch_ModifyHpLost { public static void Postfix(IRunState runState, ICombatState? combatState) { CalcManager.Update(runState, combatState); } }
[HarmonyPatch(typeof(Hook), "ModifyMaxEnergy")] public static class HookPatch_ModifyMaxEnergy { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ModifyMerchantPrice")] public static class HookPatch_ModifyMerchantPrice { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ModifyOrbValue")] public static class HookPatch_ModifyOrbValue { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ModifyPowerAmountGiven")] public static class HookPatch_ModifyPowerAmountGiven { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ModifyPowerAmountReceived")] public static class HookPatch_ModifyPowerAmountReceived { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ModifyRestSiteHealAmount")] public static class HookPatch_ModifyRestSiteHealAmount { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ModifyStarCost")] public static class HookPatch_ModifyStarCost { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ModifySummonAmount")] public static class HookPatch_ModifySummonAmount { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ModifyNextEvent")] public static class HookPatch_ModifyNextEvent { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ModifyOddsIncreaseForUnrolledRoomType")] public static class HookPatch_ModifyOddsIncreaseForUnrolledRoomType { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ModifyCardRewardAlternatives")] public static class HookPatch_ModifyCardRewardAlternatives { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ModifyRestSiteHealRewards")] public static class HookPatch_ModifyRestSiteHealRewards { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ModifyRestSiteOptions")] public static class HookPatch_ModifyRestSiteOptions { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ModifyRewards")] public static class HookPatch_ModifyRewards { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ModifyMerchantCardPool")] public static class HookPatch_ModifyMerchantCardPool { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ModifyCardPlayCount")] public static class HookPatch_ModifyCardPlayCount { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ModifyOrbPassiveTriggerCount")] public static class HookPatch_ModifyOrbPassiveTriggerCount { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ModifyXValue")] public static class HookPatch_ModifyXValue { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ModifyExtraRestSiteHealText")] public static class HookPatch_ModifyExtraRestSiteHealText { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ModifyExtraRestSiteMendText")] public static class HookPatch_ModifyExtraRestSiteMendText { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ModifyUnknownMapPointRoomTypes")] public static class HookPatch_ModifyUnknownMapPointRoomTypes { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ModifyKeywordsInCombat")] public static class HookPatch_ModifyKeywordsInCombat { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }
[HarmonyPatch(typeof(Hook), "ModifyMerchantCardCreationResults")] public static class HookPatch_ModifyMerchantCardCreationResults { public static void Postfix(IRunState runState) { CalcManager.Update(runState); } }
[HarmonyPatch(typeof(Hook), "ModifyShuffleOrder")] public static class HookPatch_ModifyShuffleOrder { public static void Postfix(ICombatState? combatState) { CalcManager.Update(combatState); } }

