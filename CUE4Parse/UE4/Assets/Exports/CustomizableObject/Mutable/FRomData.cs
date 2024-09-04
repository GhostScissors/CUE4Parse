using CUE4Parse.UE4.Assets.Readers;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable;

public class FRomData
{
    public uint Id;
    public uint Size;
    public uint ResourceIndex;
    public ushort ResourceType;
    public ERomFlags Flags;
    
    public FRomData(FAssetArchive Ar)
    {
        Id = Ar.Read<uint>();
        Size = Ar.Read<uint>();
        ResourceIndex = Ar.Read<uint>();
        ResourceType = Ar.Read<ushort>();
        Flags = Ar.Read<ERomFlags>();
    }
}

public enum ERomFlags : ushort
{
    /** Standard data. */
    None = 0,

    /** Bigger mips and mesh lods that are optional in some devices of a platform. */
    HighRes = 1 << 0,
}