using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Exceptions;
using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Readers;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Images;

using FImageSize = TIntVector2<ushort>;

public class Image : IMutablePtr
{
    public int Version { get; set; }
    public FImageDataStorage DataStorage;

    public void Deserialize(FAssetArchive Ar)
    {
        if (Version > 4)
            throw new ParserException($"Mutable Image version '{Version}' is currently not supported.");
        
        if (Version <= 3)
        {
            DataStorage = new FImageDataStorage
            {
                ImageSize = Ar.Read<FImageSize>(),
                NumLODs = Ar.Read<byte>(),
                ImageFormat = Ar.Read<EImageFormat>(),
                Buffers = Ar.ReadArray(Ar.ReadArray<byte>)
            };
        }
        else if (Version >= 4)
        {
            DataStorage = new FImageDataStorage(Ar);
        }

        var flags = Ar.Read<byte>();
    }
}