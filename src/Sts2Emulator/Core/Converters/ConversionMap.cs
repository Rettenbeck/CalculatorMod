namespace Sts2Emulator.Core;

public class ConversionMap
{
    public List<ConversionMapEntry> Content = new List<ConversionMapEntry>();
    
    public int Find(string ModelId)
    {
        foreach(var entry in Content)
        {
            if(entry.ModelId == ModelId) return entry.DefId;
        }
        return -1;
    }

    public void Add(int DefId, string ModelId)
    {
        Content.Add(new ConversionMapEntry(DefId, ModelId));
    }
}

public struct ConversionMapEntry
{
    public int DefId;
    public string ModelId;

    public ConversionMapEntry(int DefId_, string ModelId_)
    {
        DefId = DefId_;
        ModelId = ModelId_;
    }
}