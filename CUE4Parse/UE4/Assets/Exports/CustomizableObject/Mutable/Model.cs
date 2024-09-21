using CUE4Parse.UE4.Assets.Readers;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable;

public class Model
{
    public int Version;
    public FProgram Program;

    public Model(FAssetArchive Ar)
    {
        Version = Ar.Read<int>();
        Program = new FProgram(Ar);
    }
}
