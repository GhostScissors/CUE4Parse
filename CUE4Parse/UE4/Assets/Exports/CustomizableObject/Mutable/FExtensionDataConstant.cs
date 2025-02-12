﻿using CUE4Parse.UE4.Readers;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable;

public class FExtensionDataConstant
{
    public FExtensionData Data;

    public FExtensionDataConstant(FMutableArchive Ar)
    {
        Data = Ar.ReadPtr(() => new FExtensionData(Ar));
    }
}
