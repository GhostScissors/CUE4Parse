using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Exceptions;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Physics.Bodies;

public class FBodyShape
{
    public string Name;
    public uint Flags;
    
    public FBodyShape(FAssetArchive Ar)
    {
        var ver = Ar.Read<uint>();
        if (ver > 1)
            throw new ParserException($"Mutable FBodyShape version {ver} is currently not supported.");

        Name = Ar.ReadMutableFString();
        Flags = Ar.Read<uint>();
    }
}