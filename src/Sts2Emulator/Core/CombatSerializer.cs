using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sts2Emulator.Core;

public static class CombatStateSerializer
{
    private static readonly JsonSerializerOptions Options = new()
    {
        IncludeFields = true,
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public static bool Compare(CombatState? State1, CombatState? State2)
    {
        if(State1 == null) return false;
        if(State2 == null) return false;
        var TimeStamp1 = State1.TimeStamp;
        var TimeStamp2 = State2.TimeStamp;
        State1.TimeStamp = "";
        State2.TimeStamp = "";
        bool identical = JsonSerializer.Serialize(State1, Options) == JsonSerializer.Serialize(State2, Options);
        State1.TimeStamp = TimeStamp1;
        State2.TimeStamp = TimeStamp2;
        return identical;
    }

    public static string Export(CombatState state)
    {
        return JsonSerializer.Serialize(state, Options);
    }

    public static CombatState Import(string json)
    {
        return JsonSerializer.Deserialize<CombatState>(json, Options)
            ?? throw new InvalidOperationException("Invalid combat state.");
    }

    public static void Save(CombatState state, string path)
    {
        File.WriteAllText(path, Export(state));
    }

    public static CombatState Load(string path)
    {
        return Import(File.ReadAllText(path));
    }
}
