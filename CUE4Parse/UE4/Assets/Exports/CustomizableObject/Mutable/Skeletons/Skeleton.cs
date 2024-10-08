﻿using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Exceptions;
using CUE4Parse.UE4.Objects.Core.Math;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Skeletons;

public class Skeleton : IMutablePtr
{
    public int Version { get; set; }
    public FBoneName[] BoneIds;
    public short[] BoneParents;
    
    public void Deserialize(FAssetArchive Ar)
    {
        if (Version > 7)
            throw new ParserException($"Mutable Skeleton version '{Version}' is currently not supported.");
        
        if (Version >= 7)
        {
            BoneIds = Ar.ReadArray(() => new FBoneName(Ar));
        }
        else if (Version == 6)
        {
            var boneIds_DEPRECATED = Ar.ReadArray<ushort>();

            var numBones = boneIds_DEPRECATED.Length;
            BoneIds = new FBoneName[numBones];
            for (var i = 0; i < numBones; ++i)
            {
                BoneIds[i] = new FBoneName(boneIds_DEPRECATED[i]);
            }
        }
        else
        {
            var oldBoneNames = Ar.ReadArray(Ar.ReadFString);
            var numBones = oldBoneNames.Length;

            BoneIds = new FBoneName[numBones];
            for (uint i = 0; i < numBones; ++i)
            {
                BoneIds[i].Id = i;
            }
        }

        if (Version == 3)
        {
            var boneTransforms_DEPRECATED = Ar.ReadArray<FTransform>();
        }
        
        BoneParents = Ar.ReadArray<short>();

        if (Version < 6) // before BoneIds
        {
            short parentIndex = -1;
            for (int i = 0; i < BoneParents.Length; ++i)
            {
                BoneParents[i] = parentIndex;
                parentIndex++;
            }
        }
        
        if (Version <= 4)
        {
            var boneIds_DEPRECATED = Ar.ReadArray<int>();
        }

        if (Version == 3)
        {
            var bBoneTransformModified = Ar.ReadBoolean();
        }
    }
}