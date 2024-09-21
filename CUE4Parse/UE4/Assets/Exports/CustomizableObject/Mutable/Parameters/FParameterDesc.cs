using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Exceptions;
using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.Core.Misc;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable.Parameters;

public class FParameterDesc
{
    public string Name;
    public FGuid Uid;
    public PARAMETER_TYPE Type;
    public object DefaultValue;
    public uint[] Ranges;
    public FIntValueDesc[] PossibleValues;
    
    public FParameterDesc(FAssetArchive Ar)
    {
        var ver = Ar.Read<int>();
        if (ver > 10)
            throw new ParserException($"Mutable FParameterDesc version '{ver}' is currently not supported.");

        Name = Ar.ReadMutableFString();
        Uid = Ar.Read<FGuid>();
        Type = Ar.Read<PARAMETER_TYPE>();
        
        Ar.ReadByte(); // TVariantIndex
        DefaultValue = Type switch
        {
            PARAMETER_TYPE.T_BOOL => Ar.ReadBoolean(),
            PARAMETER_TYPE.T_INT => Ar.Read<int>(),
            PARAMETER_TYPE.T_FLOAT => Ar.Read<float>(),
            PARAMETER_TYPE.T_COLOUR => Ar.Read<FVector4>(),
            PARAMETER_TYPE.T_PROJECTOR => new FProjector(Ar),
            PARAMETER_TYPE.T_IMAGE => new FName(),
            PARAMETER_TYPE.T_STRING => Ar.ReadMutableFString(),
            _ => throw new ParserException($"Unknown PARAMETER_TYPE value {Type}")
        };
        
        Ranges = Ar.ReadArray<uint>();
        PossibleValues = Ar.ReadArray(() => new FIntValueDesc(Ar));
    }
}

public enum PARAMETER_TYPE : uint
{
    //! Undefined parameter type.
    T_NONE,

    //! Boolean parameter type (true or false)
    T_BOOL,

    //! Integer parameter type. It usually has a limited range of possible values that can be
    //! queried in the Parameters object.
    T_INT,

    //! Floating point value in the range of 0.0 to 1.0
    T_FLOAT,

    //! Floating point RGBA colour, with each channel ranging from 0.0 to 1.0
    T_COLOUR,

    //! 3D Projector type, defining a position, scale and orientation. Basically used for
    //! projected decals.
    T_PROJECTOR,

    //! An externally provided image.
    T_IMAGE,

    //! A text string.
    T_STRING,

    //! Utility enumeration value, not really a parameter type.
    T_COUNT
}