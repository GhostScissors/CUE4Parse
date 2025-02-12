﻿using CUE4Parse.UE4.Readers;
using FImageSize = CUE4Parse.UE4.Objects.Core.Math.TIntVector2<ushort>;
using FImageArray = byte[];

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Image;

public class FImageDataStorage
{
    public FImageArray[] Buffers;
    public FImageSize ImageSize;
    public EImageFormat ImageFormat;
    public byte NumLODs;
    public ushort[] CompactedTailOffsets;

    private const int NumLODsInCompactedTail = 7;

    public FImageDataStorage(FArchive Ar)
    {
        ImageSize = Ar.Read<FImageSize>();
        ImageFormat = Ar.Read<EImageFormat>();
        NumLODs = Ar.Read<byte>();

        var buffersNum = Ar.Read<int>();
        Buffers = new FImageArray[buffersNum];
        for (int i = 0; i < buffersNum; i++)
        {
            Buffers[i] = Ar.ReadArray<byte>();
        }

        var compactedTailOffsetsNum = Ar.Read<int>();
        CompactedTailOffsets = Ar.ReadArray<ushort>(NumLODsInCompactedTail);
    }
}

public enum EImageFormat : byte
{
    None,
    RGB_UByte,
    RGBA_UByte,
    L_UByte,

    //! Deprecated formats
    _DEPRECATED_1,
    _DEPRECATED_2,
    _DEPRECATED_3,
    _DEPRECATED_4,

    L_UByteRLE,
    RGB_UByteRLE,
    RGBA_UByteRLE,
    L_UBitRLE,

    //! Common S3TC formats
    BC1,
    BC2,
    BC3,
    BC4,
    BC5,

    //! Not really supported yet
    BC6,
    BC7,

    //! Swizzled versions, engineers be damned.
    BGRA_UByte,

    //! The new standard
    ASTC_4x4_RGB_LDR,
    ASTC_4x4_RGBA_LDR,
    ASTC_4x4_RG_LDR,

    ASTC_8x8_RGB_LDR,
    ASTC_8x8_RGBA_LDR,
    ASTC_8x8_RG_LDR,
    ASTC_12x12_RGB_LDR,
    ASTC_12x12_RGBA_LDR,
    ASTC_12x12_RG_LDR,
    ASTC_6x6_RGB_LDR,
    ASTC_6x6_RGBA_LDR,
    ASTC_6x6_RG_LDR,
    ASTC_10x10_RGB_LDR,
    ASTC_10x10_RGBA_LDR,
    ASTC_10x10_RG_LDR,

    Count
}
