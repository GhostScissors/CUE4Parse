using CUE4Parse.UE4.Assets.Readers;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.ExtensionData;

public class FExtensionDataConstant
{
    public ExtensionData Data;

    public FExtensionDataConstant(FAssetArchive Ar)
    {
        Data = new ExtensionData(Ar);
    }
}