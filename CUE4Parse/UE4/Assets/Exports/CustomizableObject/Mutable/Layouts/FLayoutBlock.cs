using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Objects.Core.Math;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Layouts;

public class FLayoutBlock
{
    public TIntVector2<ushort> Min;
    public TIntVector2<ushort> Size;
    public ulong Id;
    public int Priority;
    public bool bReduceBothAxes;
    public bool bReduceByTwo;
    public uint UnusedPadding;
    
    public FLayoutBlock(FAssetArchive Ar)
    {
        Min = Ar.Read<TIntVector2<ushort>>();
        Size = Ar.Read<TIntVector2<ushort>>();
        Id = Ar.Read<ulong>();
        Priority = Ar.Read<int>();
        var flags = Ar.Read<uint>();
        bReduceBothAxes = (flags & 1) == 1;
        bReduceByTwo = (flags & 2) == 2;
    }
}