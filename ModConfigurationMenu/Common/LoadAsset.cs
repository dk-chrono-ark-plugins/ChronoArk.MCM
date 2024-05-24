using System.IO;
using ChronoArkMod.ModData;

namespace Mcm.Common;

public static class LoadAsset
{
    public static Texture2D LoadTexture2D(this ModInfo modInfo, string filename, bool absens = false)
    {
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

        return McmMod.ModInfo!.LoadTexture2D("absens.png", true);
    }
}