// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.Explosives.SemiBridgifierProj
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Items.Tiles;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Projectiles.Explosives
{
  public class SemiBridgifierProj : OmniBridgifierProj
  {
    protected override int TileHeight => 3;

    protected override int Placeable => ModContent.TileType<SemistationSheet>();

    protected override bool Replaceable(int TileType) => TileType == this.Placeable;

    public override void SetStaticDefaults()
    {
    }
  }
}
