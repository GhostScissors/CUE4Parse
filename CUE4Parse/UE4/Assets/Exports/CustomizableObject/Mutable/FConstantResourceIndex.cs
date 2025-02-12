using CUE4Parse.UE4.Readers;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable;

public class FConstantResourceIndex
{
    public uint Index;
    public bool Streamable;

    public FConstantResourceIndex(FArchive Ar)
    {
        Index = Ar.Read<uint>();
        Streamable = Ar.ReadBoolean();
    }
}
