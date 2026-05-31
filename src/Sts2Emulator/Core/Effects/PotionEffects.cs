namespace Sts2Emulator.Core.Effects;

public static class PotionEffects
{
    public static void Apply(int potionId, CombatState state)
    {
        // Populated incrementally as potions are reverse-engineered from sts2.dll.
        switch (potionId)
        {
            case 21: // Duplicator: this turn, play the next card an extra time.
                BuffSystem.Apply(state.PlayerBuffs, BuffId.OneTwoPunch, 1);
                break;
            default:
                break;
        }
    }
}
