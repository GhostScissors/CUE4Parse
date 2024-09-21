using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Exceptions;
using CUE4Parse.UE4.Objects.Core.Math;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Physics.Bodies;

public class FSphereBody : FBodyShape
{
    public FVector Position;
    public float Radius;
    
    public FSphereBody(FAssetArchive Ar) : base(Ar)
    {
        var ver = Ar.Read<uint>();
        if (ver > 0)
            throw new ParserException($"Mutable FSphereBody version {ver} is currently not supported."); 
        
        Position = Ar.Read<FVector>();
        Radius = Ar.Read<float>();
    }
}