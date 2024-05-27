using System.IO;
using ChronoArkMod.ModData;

namespace Mcm.Common;

public static class LoadAsset
{
    public static Texture2D LoadTexture2D(this ModInfo modInfo, string filename, bool absens = false)
    {
        while (true) {
            var file = Path.Combine(modInfo.assetInfo.AssetDirectory, filename);
            if (File.Exists(file)) {
                var raw = File.ReadAllBytes(file);
                var texture = new Texture2D(2, 2);
                if (texture.LoadImage(raw)) {
                    return texture;
                }
            }

            if (absens) {
                throw new FileNotFoundException("can't find texture");
            }

            modInfo = McmMod.ModInfo!;
            filename = "absens.png";
            absens = true;
        }
    }
}