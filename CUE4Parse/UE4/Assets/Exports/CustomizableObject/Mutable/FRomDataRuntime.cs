using CUE4Parse.UE4.Readers;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable;

public class FRomDataRuntime
{
    public uint Size;
    public ERomDataType ResourceType;
    public bool IsHighRes;

    public FRomDataRuntime(FArchive Ar)
    {
        Size = Ar.Read<uint>();
        ResourceType = Ar.Read<ERomDataType>();
        IsHighRes = Ar.ReadBoolean();
    }
}

public enum ERomDataType : uint
{
    Image = 0,
    Mesh  = 1
}
