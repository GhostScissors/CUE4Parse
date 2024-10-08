using System;
using System.IO;
using System.Threading.Tasks;
using CUE4Parse.Compression;
using CUE4Parse.Encryption.Aes;
using CUE4Parse.FileProvider;
using CUE4Parse.MappingsProvider;
using CUE4Parse.UE4.Assets.Exports.CustomizableObject;
using CUE4Parse.UE4.Objects.Core.Misc;
using CUE4Parse.UE4.Versions;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace CUE4Parse.Example
{
    public static class Program
    {
        private const string GameDirectory = @"D:\Fortnite\FortniteGame\Content\Paks";
        private const string AesKey = "0xE4417665D53F2340E22F7DE2DF23F4CC43D16413A256AD0393F821B633DC54E4";

        private const string MappingsFile = @"D:\Leaking Tools\FModel\Output\.data\5.5.0-35935822FortniteRelease-31.10-FortniteGame.usmap";

        public static async Task Main()
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console(theme: SystemConsoleTheme.Literate).CreateLogger();

            await InitOodle().ConfigureAwait(false);
            
            var provider = new DefaultFileProvider(new DirectoryInfo(GameDirectory), SearchOption.TopDirectoryOnly, true, new VersionContainer(EGame.GAME_UE5_5));
            provider.MappingsContainer = new FileUsmapTypeMappingsProvider(MappingsFile);
            await provider.SubmitKeyAsync(new FGuid(), new FAesKey(AesKey));

            provider.Initialize();
            await provider.MountAsync().ConfigureAwait(false);

            var customizableObject = await provider.LoadObjectAsync<UCustomizableObject>("FortniteGame/Plugins/GameFeatures/Juno/FigureCosmetics/Content/Figure/_Figure_Core/Mutable/CustomizableObject/CO_Figure_v2.CO_Figure_v2");
            Console.ReadKey();
        }

        private static async Task InitOodle()
        {
            var oodlePath = Path.Combine(Environment.CurrentDirectory, OodleHelper.OODLE_DLL_NAME);
            if (!File.Exists(oodlePath)) await OodleHelper.DownloadOodleDllAsync(oodlePath).ConfigureAwait(false);
            OodleHelper.Initialize(oodlePath);
        }
    }
}
