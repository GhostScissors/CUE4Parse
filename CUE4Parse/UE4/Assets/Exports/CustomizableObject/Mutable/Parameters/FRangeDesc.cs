using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Exceptions;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Parameters;

public class FRangeDesc
{
    public string Name;
    public string Uid;
    public int DimensionParameter;
    
    public FRangeDesc(FAssetArchive Ar)
    {
        var ver = Ar.Read<int>();
        if (ver > 3)
            throw new ParserException($"Mutable FRangeDesc version '{ver}' is currently not supported.");
        
        Name = Ar.ReadMutableFString();
        Uid = Ar.ReadMutableFString();
        DimensionParameter = Ar.Read<int>();
    }
}