﻿using CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Skeleton;
using CUE4Parse.UE4.Readers;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Physics;

public class FPhysicsBody
{
    public int CustomId;
    public FPhysicsBodyAggregate[] Bodies;
    public FBoneName[] BoneIds;
    public int[] BodiesCustomIds;
    public bool bBodiesModified;

    public FPhysicsBody(FMutableArchive Ar)
    {
        CustomId = Ar.Read<int>();
        Bodies = Ar.ReadArray(() => new FPhysicsBodyAggregate(Ar));
        BoneIds = Ar.ReadArray(() => new FBoneName(Ar));
        BodiesCustomIds = Ar.ReadArray<int>();
        bBodiesModified = Ar.ReadFlag();
    }
}
