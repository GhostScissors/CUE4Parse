﻿using System.Collections.Generic;
using CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Layouts;
using CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Physics;
using CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Skeletons;
using CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Surfaces;
using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Exceptions;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Meshes;

public class Mesh : IMutablePtr
{
    public int Version { get; set; }
    public FMeshBufferSet IndexBuffers;
    public FMeshBufferSet VertexBuffers;
    public KeyValuePair<EMeshBufferType, FMeshBufferSet>[] AdditionalBuffers;
    public Layout[] Layouts;
    public uint[] SkeletonIDs;
    public Skeleton Skeleton; // TODO FINISH ALL VERSIONS
    public PhysicsBody PhysicBody;
    public EMeshFlags Flags;
    public FMeshSurface[] Surfaces;
    public string[] Tags;
    public ulong[] StreamedResources;
    public FBonePose[] BonePoses;
    public FBoneName[] BoneMap;
    public PhysicsBody[] AdditionalPhysicsBodies;
    public uint MeshIDPrefix;
    public uint ReferenceID;
    
    public void Deserialize(FAssetArchive Ar)
    {
        if (Version > 23)
            throw new ParserException($"Mutable Mesh version {Version} is currently not supported.");

        IndexBuffers = new FMeshBufferSet(Ar);
        VertexBuffers = new FMeshBufferSet(Ar);
        AdditionalBuffers = Ar.ReadArray(() => new KeyValuePair<EMeshBufferType, FMeshBufferSet>(Ar.Read<EMeshBufferType>(), Ar.Read<FMeshBufferSet>()));
        Layouts = Ptr<Layout>.ReadArray(Ar);
        SkeletonIDs = Ar.ReadArray<uint>();
        Skeleton = Ar.ReadMutable<Skeleton>();
        PhysicBody = Ar.ReadMutable<PhysicsBody>();
        Flags = Ar.Read<EMeshFlags>();
        Surfaces = Ar.ReadArray(() => new FMeshSurface(Ar));
        Tags = Ar.ReadArray(Ar.ReadMutableFString);
        StreamedResources = Ar.ReadArray<ulong>();
        BonePoses = Ar.ReadArray(() => new FBonePose(Ar));
        BoneMap = Ar.ReadArray(() => new FBoneName(Ar));
        AdditionalPhysicsBodies = Ptr<PhysicsBody>.ReadArray(Ar);
        MeshIDPrefix = Ar.Read<uint>();
        ReferenceID = Ar.Read<uint>();
    }
}

public enum EMeshBufferType
{
    None,
    SkeletonDeformBinding,
    PhysicsBodyDeformBinding,
    PhysicsBodyDeformSelection,
    PhysicsBodyDeformOffsets,
    MeshLaplacianData,
    MeshLaplacianOffsets,
    UniqueVertexMap
}

public enum EMeshFlags : uint
{
    None = 0,

    /** The mesh is formatted to be used for planar and cilyndrical projection */
    ProjectFormat = 1 << 0,

    /** The mesh is formatted to be used for wrapping projection */
    ProjectWrappingFormat = 1 << 1,

    /** The mesh is a reference to an external resource mesh. */
    IsResourceReference = 1 << 2,

    /** The mesh is a reference to an external resource mesh and must be loaded when first referenced. */
    IsResourceForceLoad = 1 << 3,
}