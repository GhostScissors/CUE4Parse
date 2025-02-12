﻿using CUE4Parse.UE4.Readers;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Mesh;

public class FMeshSurface
{
    public FSurfaceSubMesh[] SubMeshes;
    public uint BoneMapIndex;
    public uint BoneMapCount;
    public uint Id;

    public FMeshSurface(FArchive Ar)
    {
        SubMeshes = Ar.ReadArray(() => new FSurfaceSubMesh(Ar));
        BoneMapIndex = Ar.Read<uint>();
        BoneMapCount = Ar.Read<uint>();
        Id = Ar.Read<uint>();
    }
}
