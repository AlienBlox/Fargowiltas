// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Tiles.Omnistation2
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Tiles
{
  public class Omnistation2 : BaseOmnistation
  {
    public Omnistation2()
      : base(1198)
    {
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.createTile = ModContent.TileType<OmnistationSheet2>();
    }
  }
}
