using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Exceptions;
using CUE4Parse.UE4.Objects.Core.Math;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Physics.Bodies;

public class FSphylBody : FBodyShape
{
    public FVector Position;
    public FQuat Orientation;
    public float Radius;
    public float Length;
    
    public FSphylBody(FAssetArchive Ar) : base(Ar)
    {
        var ver = Ar.Read<uint>();
        if (ver > 0)
            throw new ParserException($"Mutable FSphereBody version {ver} is currently not supported."); 
        
        Position = Ar.Read<FVector>();
        Orientation = Ar.Read<FQuat>();
        Radius = Ar.Read<float>();
        Length = Ar.Read<float>();
    }
}