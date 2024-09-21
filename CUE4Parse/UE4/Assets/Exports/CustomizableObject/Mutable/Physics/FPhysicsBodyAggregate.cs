using CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Physics.Bodies;
using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Exceptions;
using CUE4Parse.UE4.Objects.Core.Math;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Physics;

public class FPhysicsBodyAggregate
{
    public FSphereBody[] Spheres;
    public FBoxBody[] Boxes;
    public FConvexBody[] Convex;
    public FSphylBody[] Sphyls;
    public FTaperedCapsuleBody[] TaperedCapsules;
    
    public FPhysicsBodyAggregate(FAssetArchive Ar)
    {
        var ver = Ar.Read<uint>();
        if (ver > 0)
            throw new ParserException($"Mutable FPhysicsBodyAggregate version {ver} is currently not supported.");

        Spheres = Ar.ReadArray(() => new FSphereBody(Ar));
        Boxes = Ar.ReadArray(() => new FBoxBody(Ar));
        Convex = Ar.ReadArray(() => new FConvexBody(Ar));
        Sphyls = Ar.ReadArray(() => new FSphylBody(Ar));
        TaperedCapsules = Ar.ReadArray(() => new FTaperedCapsuleBody(Ar));
    }
}