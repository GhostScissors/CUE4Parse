using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Objects.Core.Math;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Layouts;

public class Layout : IMutablePtr
{
    public TIntVector2<ushort> Size;
    public FLayoutBlock[] Blocks;
    public TIntVector2<ushort> MaxSize;
    public EPackStrategy Strategy;
    public EReductionMethod ReductionMethod;

    public int Version { get; set; }
    public void Deserialize(FAssetArchive Ar)
    {
        Size = Ar.Read<TIntVector2<ushort>>();
        Blocks = Ar.ReadArray(() => new FLayoutBlock(Ar));
        MaxSize = Ar.Read<TIntVector2<ushort>>();
        Strategy = Ar.Read<EPackStrategy>();
        ReductionMethod = Ar.Read<EReductionMethod>();
    }
}

public enum EPackStrategy : uint
{
    Resizeable,
    Fixed,
    Overlay
}

public enum EReductionMethod : uint
{
    Halve,	// Divide axis by 2
    Unitary	// Reduces 1 block the axis
}