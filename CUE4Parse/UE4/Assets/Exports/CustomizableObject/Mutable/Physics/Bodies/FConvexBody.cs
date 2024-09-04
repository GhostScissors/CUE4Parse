using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Exceptions;
using CUE4Parse.UE4.Objects.Core.Math;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Physics.Bodies;

public class FConvexBody : FBodyShape
{
    public FVector[] Vertices;
    public int[] Indices;
    public FTransform Transform;
    
    public FConvexBody(FAssetArchive Ar) : base(Ar)
    {
        var ver = Ar.Read<uint>();
        if (ver > 0)
            throw new ParserException($"Mutable FConvexBody version {ver} is currently not supported."); 
        
        Vertices = Ar.ReadArray<FVector>();
        Indices = Ar.ReadArray<int>();
        Transform = Ar.Read<FTransform>();
    }
}