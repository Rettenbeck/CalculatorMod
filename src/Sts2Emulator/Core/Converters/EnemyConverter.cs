using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Entities;
using MegaCrit.Sts2.Core.Entities.Creatures;

namespace Sts2Emulator.Core;


public static class EnemyConverter
{
    public static ConversionMap MapEnemies = CreateMappingEnemies();
    public static ConversionMap MapMoves = CreateMappingMoves();

    public static List<EnemyState> Get(
        IReadOnlyList<Creature> creatures,
        int turn,
        Random rng,
        Random? aiRng = null
    )
    {
        var enemies = new List<EnemyState>();

        foreach(var creature in creatures)
        {
            int DefId = MapEnemies.Find(creature.ModelId.ToString());
            CalculatorMod.MLog.Info("Looking for:" + creature.ModelId.ToString());
            int Hp = creature.CurrentHp;
            int MaxHp = creature.MaxHp;
            int Block = creature.Block;
            List<BuffState> Buffs = [];
            int LastMove = -1;
            int StolenGold = 0;
            int HeistGold = 0;

            var moveId = "";
            var monster = creature.Monster;

            if(monster != null) moveId = monster.NextMove.Id;
            int MoveIndex = MapMoves.Find(moveId);

            var e = new EnemyState();
            e.DefId = DefId;
            e.Hp = Hp;
            e.MaxHp = MaxHp;
            e.Block = Block;
            e.Buffs = Buffs;
            e.MoveIndex = MoveIndex;
            e.LastMove = LastMove;
            e.StolenGold = StolenGold;
            e.HeistGold = HeistGold;

            enemies.Add(e);
        }

        EnemyAI.ChooseIntents(enemies, turn, rng, aiRng);

        return enemies;
    }

    public static ConversionMap CreateMappingEnemies()
    {
        var map = new ConversionMap();

        map.Add(KE.ShrinkerBeetle           , "MONSTER.SHRINKER_BEETLE"         );

        return map;
    }
    
    public static ConversionMap CreateMappingMoves()
    {
        var map = new ConversionMap();

        map.Add(0 , "SHRINKER_MOVE"         );
        map.Add(1 , "CHOMP_MOVE"            );
        map.Add(2 , "STOMP_MOVE"            );

        return map;
    }
    
}
