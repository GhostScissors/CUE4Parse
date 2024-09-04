using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Exceptions;
using CUE4Parse.UE4.Objects.Core.Math;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Images;

using FImageArray = byte[];
using FImageSize = TIntVector2<ushort>;

public class FImageDataStorage
{
    public int Version;
    public FImageSize ImageSize;
    public EImageFormat ImageFormat;
    public byte NumLODs;
    public FImageArray[] Buffers;
    public ushort[] CompactedTailOffsets;

    private int NumLODsInCompactedTail = 7;

    public FImageDataStorage() { }
    public FImageDataStorage(FAssetArchive Ar)
    {
        Version = Ar.Read<int>();

        if (Version > 0)
            throw new ParserException($"Mutable FImageDataStorage version '{Version}' is currently not supported");
        
        ImageSize = Ar.Read<FImageSize>();
        ImageFormat = Ar.Read<EImageFormat>();
        NumLODs = Ar.Read<byte>();

        Ar.Position += 3; // epic games woooohoooooo 
        
        var buffersNum = Ar.Read<int>();
        Buffers = new FImageArray[buffersNum];
        for (var i = 0; i < buffersNum; i++)
        {
            Buffers[i] = Ar.ReadArray<byte>();
        }
        
        var numTailOffsets = Ar.Read<int>();
        if (numTailOffsets != NumLODsInCompactedTail)
            throw new ParserException("NumTailOffsets != NumLODsInCompactedTail");

        CompactedTailOffsets = Ar.ReadArray<ushort>(numTailOffsets);
    }
}

public enum EImageFormat : byte
{
    IF_NONE,
    IF_RGB_UBYTE,
    IF_RGBA_UBYTE,
    IF_L_UBYTE,

    //! Deprecated formats
    IF_PVRTC2_DEPRECATED,
    IF_PVRTC4_DEPRECATED,
    IF_ETC1_DEPRECATED,
    IF_ETC2_DEPRECATED,

    IF_L_UBYTE_RLE,
    IF_RGB_UBYTE_RLE,
    IF_RGBA_UBYTE_RLE,
    IF_L_UBIT_RLE,

    //! Common S3TC formats
    IF_BC1,
    IF_BC2,
    IF_BC3,
    IF_BC4,
    IF_BC5,

    //! Not really supported yet
    IF_BC6,
    IF_BC7,

    //! Swizzled versions, engineers be damned.
    IF_BGRA_UBYTE,

    //! The new standard
    IF_ASTC_4x4_RGB_LDR,
    IF_ASTC_4x4_RGBA_LDR,
    IF_ASTC_4x4_RG_LDR,

    IF_ASTC_8x8_RGB_LDR,
    IF_ASTC_8x8_RGBA_LDR,
    IF_ASTC_8x8_RG_LDR,
    IF_ASTC_12x12_RGB_LDR,
    IF_ASTC_12x12_RGBA_LDR,
    IF_ASTC_12x12_RG_LDR,
    IF_ASTC_6x6_RGB_LDR,
    IF_ASTC_6x6_RGBA_LDR,
    IF_ASTC_6x6_RG_LDR,
    IF_ASTC_10x10_RGB_LDR,
    IF_ASTC_10x10_RGBA_LDR,
    IF_ASTC_10x10_RG_LDR,

    IF_COUNT
}