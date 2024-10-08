﻿using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.ExtensionData;
using CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Images;
using CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Layouts;
using CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Meshes;
using CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Parameters;
using CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Physics;
using CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Skeletons;
using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.Engine.Curves;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable;

public class FProgram
{
    public uint[] OpAddress;
    public byte[] ByteCode;
    public FState[] States;
    public FRomData[] Roms;
    public Image[] ConstantImageLODs;
    public uint[] ConstantImageLODIndices;
    public FImageLODRange[] ConstantImages;
    public Mesh[] ConstantMeshes;
    public FExtensionDataConstant[] ConstantExtensionData;
    public string[] ConstantStrings;
    public Layout[] ConstantLayouts;
    public FProjector[] ConstantProjectors;
    public FMatrix[] ConstantMatrices;
    public FShape[] ConstantShapes;
    public FRichCurve[] ConstantCurves;
    public Skeleton[] ConstantSkeletons;
    public PhysicsBody[] ConstantPhysicsBodies;
    public FParameterDesc[] Parameters;
    public FRangeDesc[] Ranges;
    public ushort[][] ParameterLists;
    
    public FProgram(FAssetArchive Ar)
    {
        OpAddress = Ar.ReadArray<uint>();
        ByteCode = Ar.ReadArray<byte>();
        States = Ar.ReadArray(() => new FState(Ar));
        Roms = Ar.ReadArray(() => new FRomData(Ar));
        ConstantImageLODs = Ptr<Image>.ReadArray(Ar);
        ConstantImageLODIndices = Ar.ReadArray<uint>();
        ConstantImages = Ar.ReadArray(() => new FImageLODRange(Ar));
        ConstantMeshes = Ptr<Mesh>.ReadArray(Ar);
        ConstantExtensionData = Ar.ReadArray(() => new FExtensionDataConstant(Ar));
        ConstantStrings = Ar.ReadArray(Ar.ReadMutableFString);
        ConstantLayouts = Ptr<Layout>.ReadArray(Ar, true);
        ConstantProjectors = Ar.ReadArray(() => new FProjector(Ar));
        ConstantMatrices = Ar.ReadArray(() => new FMatrix(Ar, false));
        ConstantShapes = Ar.ReadArray(() => new FShape(Ar));
        ConstantCurves = Ar.ReadArray(() => new FRichCurve(Ar));
        ConstantSkeletons = Ptr<Skeleton>.ReadArray(Ar);
        ConstantPhysicsBodies = Ptr<PhysicsBody>.ReadArray(Ar);
        Parameters = Ar.ReadArray(() => new FParameterDesc(Ar));
        Ranges = Ar.ReadArray(() => new FRangeDesc(Ar));
        ParameterLists = Ar.ReadArray(Ar.ReadArray<ushort>);
    }
}