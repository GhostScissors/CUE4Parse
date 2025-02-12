using CUE4Parse.UE4.Readers;
using Newtonsoft.Json;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable;

[JsonConverter(typeof(FModelConverter))]
public class FModel
{
    public int Version;
    public FProgram Program;

    public FModel(FArchive Ar)
    {
        Version = Ar.Read<int>();
        Program = new FProgram(Ar);
    }
}
