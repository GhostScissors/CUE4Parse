using System;
using CUE4Parse.MappingsProvider.Usmap;
using CUE4Parse.UE4.Exceptions;
using CUE4Parse.UE4.Readers;

namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable;

public static class MutableArchiveExtension
{
    public static string ReadMutableFString(this FArchive Ar)
    {
        // > 0 for ANSICHAR, < 0 for UCS2CHAR serialization
        var length = Ar.Read<int>() * 2;

        if (length == int.MinValue)
            throw new ArgumentOutOfRangeException(nameof(length), "Archive is corrupted");

        if (Math.Abs(length) > Ar.Length - Ar.Position)
            throw new ParserException($"Invalid FString length '{length}'");

        // if (length is < -512000 or > 512000)
        //     throw new ParserException($"Invalid FString length '{length}'");

        if (length == 0)
            return string.Empty;

        // 1 byte/char is removed because of null terminator ('\0')
        if (length < 0) // LoadUCS2Char, Unicode, 16-bit, fixed-width
        {
            unsafe
            {
                length = -length;
                var ucs2Length = length * sizeof(ushort);
                var ucs2Bytes = ucs2Length <= 1024 ? stackalloc byte[ucs2Length] : new byte[ucs2Length];
                fixed (byte* ucs2BytesPtr = ucs2Bytes)
                {
                    Ar.Serialize(ucs2BytesPtr, ucs2Length);
#if !NO_STRING_NULL_TERMINATION_VALIDATION
                    if (ucs2Bytes[ucs2Length - 1] != 0 || ucs2Bytes[ucs2Length - 2] != 0)
                    {
                        throw new ParserException(Ar, "Serialized FString is not null terminated");
                    }
#endif
                    return new string((char*)ucs2BytesPtr, 0, length - 1).Replace("\0", string.Empty);
                }
            }
        }

        unsafe
        {
            var ansiBytes = length <= 1024 ? stackalloc byte[length] : new byte[length];
            fixed (byte* ansiBytesPtr = ansiBytes)
            {
                Ar.Serialize(ansiBytesPtr, length);
#if !NO_STRING_NULL_TERMINATION_VALIDATION
                if (ansiBytes[length - 1] != 0)
                {
                    throw new ParserException(Ar, "Serialized FString is not null terminated");
                }
#endif
                return new string((sbyte*)ansiBytesPtr, 0, length - 1).Replace("\0", string.Empty);;
            }
        }
    }
}