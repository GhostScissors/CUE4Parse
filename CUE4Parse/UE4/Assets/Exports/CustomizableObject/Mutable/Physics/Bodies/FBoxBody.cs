using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Exceptions;
using CUE4Parse.UE4.Objects.Core.Math;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Physics.Bodies;

public class FBoxBody : FBodyShape
{
    public FVector Position;
    public FQuat Orientation;
    public FVector Size;
    
    public FBoxBody(FAssetArchive Ar) : base(Ar)
    {
        var ver = Ar.Read<uint>();
        if (ver > 0)
            throw new ParserException($"Mutable FBoxBody version {ver} is currently not supported."); 
        
        Position = Ar.Read<FVector>();
        Orientation = Ar.Read<FQuat>();
        Size = Ar.Read<FVector>();
    }
}