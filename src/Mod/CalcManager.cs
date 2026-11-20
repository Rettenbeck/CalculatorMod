using Godot;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Rooms;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Data;

namespace CalculatorMod;


public static class CalcManager 
{

    public static IRunState? CurrentGameRunState = null;
    public static ICombatState? CurrentGameCombatState = null;
    public static Sts2Emulator.Core.CombatState? CurrentCombatState = null;

    public static Random rng = new Random(10);
    public static Random? aiRng = new Random(11);

    public static void Update(IRunState? GameRunState = null)
    {
        if(GameRunState == null) return;
        CurrentGameRunState = GameRunState;
    }

    public static void Update(ICombatState? GameCombatState = null)
    {
        if(GameCombatState == null) return;
        CurrentGameCombatState = GameCombatState;
        Update(GameCombatState.RunState);
    }

    public static void Update(IRunState? GameRunState, ICombatState? GameCombatState)
    {
        Update(GameRunState);
        Update(GameCombatState);
    }

    public static void UpdateInternalCombatState()
    {
        var TempState = Convert(CurrentGameCombatState);
        if(TempState == null) return;

        bool identical = Sts2Emulator.Core.CombatStateSerializer.Compare(TempState, CurrentCombatState);
        if(identical) return;

        CurrentCombatState = TempState;

        DateTime dateTime = DateTime.Now;
        string ts = dateTime.ToString("HH:mm:ss");

        CurrentCombatState.TimeStamp = ts;
        Sts2Emulator.Core.CombatStateSerializer.Save(CurrentCombatState, CalculatorMod.GetModPath() + "combat.json");
    }

    public static Sts2Emulator.Core.CombatState? Convert(ICombatState? GameCombatState = null)
    {
        if(GameCombatState == null) return null;
        Sts2Emulator.Core.CombatState s = new();

        Player player = GetPlayer(GameCombatState);
        if(player == null) return null;

        CharacterModel characterModel = player.Character;
        Creature creature = player.Creature;
        PlayerCombatState? playerCombatState = player.PlayerCombatState;

        var Deck = player.Deck;

        s.PlayerHp = creature.CurrentHp;
        s.PlayerMaxHp = creature.MaxHp;
        s.PlayerBlock = creature.Block;
        s.Energy = -1; if(playerCombatState != null) s.Energy = playerCombatState.Energy;
        s.MaxEnergy = player.MaxEnergy;
        s.Stars = 0; if(playerCombatState != null) s.Stars = playerCombatState.Stars;

        // s.PlayerGold = CardPile.Get(PileType.Hand, player).Cards.Count;
        s.PlayerGold = player.Gold;

        s.Hand = PileToCardList(CardPile.Get(PileType.Hand, player));
        s.DrawPile = PileToCardList(CardPile.Get(PileType.Draw, player));
        s.DiscardPile = PileToCardList(CardPile.Get(PileType.Discard, player));
        s.ExhaustPile = PileToCardList(CardPile.Get(PileType.Exhaust, player));

        s.Turn = 0; if(playerCombatState != null) s.Turn = playerCombatState.TurnNumber;
        
        s.Enemies = GetEnemies(GameCombatState, s.Turn);

        return s;

    }

    public static Player? GetPlayer(ICombatState? combatState = null)
    {
        var combatStateTmp = combatState;
        if(combatStateTmp == null)
        {
            combatStateTmp = CurrentGameCombatState;
            if(combatStateTmp == null) return null;
        }

        IReadOnlyList<Player> players = combatStateTmp.Players;
        Player player = players[0];
        return player;
    }

    public static List<Sts2Emulator.Core.CardInstance> PileToCardList(CardPile cardPile)
    {
        var lst = new List<Sts2Emulator.Core.CardInstance>();
        foreach(var cardModel in cardPile.Cards)
        {
            lst.Add(Sts2Emulator.Core.CardConverter.Get(cardModel));
        }
        return lst;
    }

    public static PileType FindCard(CardModel? cardModel, Player? player = null)
    {
        {
            if(CurrentCombatState == null) return PileType.None;
            if(cardModel == null) return PileType.None;

            var playerTmp = player;
            if(playerTmp == null)
            {
                playerTmp = GetPlayer();
                if(playerTmp == null) return PileType.None;
            }
            
            var PileTypes = new List<PileType>{
                                PileType.Draw,
                                PileType.Hand,
                                PileType.Discard,
                                PileType.Exhaust,
                                PileType.Play,
                                PileType.Deck
                            };
            
            foreach(var pileType in PileTypes)
            {
                if(CardPile.Get(pileType, playerTmp).Cards.Contains(cardModel)) return pileType;
            }

            return PileType.None;
        }
    }

    public static List<Sts2Emulator.Core.EnemyState> GetEnemies(ICombatState? combatState, int turn)
    {
        var lst = new List<Sts2Emulator.Core.EnemyState>();

        var combatStateTmp = combatState;
        if(combatStateTmp == null)
        {
            combatStateTmp = CurrentGameCombatState;
            if(combatStateTmp == null) return lst;
        }

        return Sts2Emulator.Core.EnemyConverter.Get(combatStateTmp.Enemies, turn, rng, aiRng);
    }

}

    // // Cards
    // public List<CardInstance> ReturnToHandBeforeDraw = [];
    // public List<CardInstance> AutoPlayQueue = [];

    // // Defect-style orb queue.
    // public List<OrbState> Orbs = [];
    // public int OrbCapacity = 3;

    // // Necrobinder pet state.
    // public int OstyHp;
    // public int OstyMaxHp;

    // // Potions: slot index → potion def ID, 0 = empty
    // public int[] PotionSlots = new int[3];
    // public int MaxPotionSlots = 3;

    // // Relics
    // public List<RelicInstance> Relics = [];

    // // Buffs/debuffs on the player
    // public List<BuffState> PlayerBuffs = [];

    // // Enemies
    // public int EncounterId;
    // public bool IsEliteCombat;

    // // Shuffle RNG (RunRngSet.shuffle subsystem) — used for mid-combat discard reshuffles.
    // // Null falls back to the combat RNG (only valid when no pre-shuffle was done).
    // // CountingRandom tracks total Next() calls so RunEngine can sync its shuffle RNG.
    // public CountingRandom? ShuffleRng;

    // // AI RNG (RunRngSet.monster_ai subsystem) — used for enemy intent selection.
    // // Null falls back to the combat RNG (used in single-combat tests).
    // public Random? AiRng;

    // // Niche HP RNG — used ONLY for SetUniqueMonsterHpValue (CreateEnemy HP calls).
    // // When non-null, CreateEnemy uses this instead of the main combat RNG for HP.
    // // CountingRandom.CallCount tracks how many HP values were drawn (= enemy count).
    // public CountingRandom? NicheHpRng;

    // // Turn tracking
    // public bool PlayerTurn = true;
    // public bool SkillPlayedWhileSmoggy;
    // public int AttackCardsPlayedThisTurn;
    // public int AttackOrSkillCardsPlayedThisTurn;
    // public int CardPlaysThisTurn;
    // public int CardsPlayedThisCombat;
    // public int DrawnCardsSinceAutomationProc;
    // public int CardsPlayedSincePanacheProc;
    // public int BlockGainsThisTurn;
    // public int PlayerHpLostThisTurn;
    // public int CardsExhaustedThisTurn;
    // public int LightningOrbsChanneledThisCombat;
    // public int EtherealExhaustCount; // number of cards exhausted by Ethereal this turn (Dark Embrace)
    // public int UnblockedDamageHitCount; // times player took unblocked damage this combat (TearAsunder)
    // public int TargetEnemyIndex = -1; // -1 = auto (first living enemy), >=0 = specific index
