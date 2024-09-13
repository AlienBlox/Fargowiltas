// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Content.Biomes.SkyBiome
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;
using Terraria.GameContent.Personalities;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Content.Biomes
{
  public class SkyBiome : IShoppingBiome, ILoadable
  {
    public string NameKey => "Mods.Fargowiltas.Biome.Sky";

    public bool IsInBiome(Player player) => player.ZoneSkyHeight;

    void ILoadable.Load(Mod mod)
    {
    }

    void ILoadable.Unload()
    {
    }
  }
}
