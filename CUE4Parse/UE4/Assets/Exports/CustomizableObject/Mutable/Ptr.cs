using System.Linq;
using CUE4Parse.UE4.Assets.Readers;


namespace CUE4Parse.UE4.Assets.Exports.CustomizableObject.Mutable;

public class Ptr<T> where T : IMutablePtr, new()
{
    public int Index;
    private bool IsBadPointer;
    private int Version;

    public T Object;

    public Ptr(FAssetArchive Ar, bool skipVersion = false)
    {
        if (!skipVersion)
        {
            Version = Ar.Read<int>();
            if (Version == -1)
            {
                IsBadPointer = true;
                return;
            }
        }
        
        Object = new T { Version = Version };
        Object.Deserialize(Ar);
    }

    public static T[] ReadArray(FAssetArchive Ar, bool skipVersion = false)
    {
        var count = Ar.Read<int>();
        var array = new Ptr<T>[count];
        var nextObjectIndex = 0;
        for (var objectIndex = 0; objectIndex < count; objectIndex++)
        {
            var index = Ar.Read<int>();
            if (index == -1)
            {
                objectIndex--;
                continue;
            }

            var ptr = new Ptr<T>(Ar, skipVersion) { Index = index };
            if (ptr.IsBadPointer) continue;

            array[nextObjectIndex] = ptr;
            nextObjectIndex++;
        }

        return array.Where(index => index != null).Select(index => index.Object).ToArray();
    }
}

public interface IMutablePtr
{
    public int Version { get; set; }
    public void Deserialize(FAssetArchive Ar);
}

public static class MutablePtrExtensions 
{
    public static T ReadMutable<T>(this FAssetArchive Ar) where T : IMutablePtr, new()
    {
        var obj = new Ptr<T>(Ar);
        return obj.Object;
    }
}