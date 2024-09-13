// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Tiles.CrimsonAltarSheet
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

#nullable disable
namespace Fargowiltas.Items.Tiles
{
  public class CrimsonAltarSheet : ModTile
  {
    public virtual void SetStaticDefaults()
    {
      Main.tileLighted[(int) ((ModBlockType) this).Type] = true;
      Main.tileFrameImportant[(int) ((ModBlockType) this).Type] = true;
      TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
      Main.tileNoAttach[(int) ((ModBlockType) this).Type] = true;
      TileObjectData.newTile.CoordinateHeights = new int[2]
      {
        16,
        16
      };
      TileObjectData.addTile((int) ((ModBlockType) this).Type);
      this.AddMapEntry(new Color(200, 200, 200), ((ModBlockType) this).CreateMapEntryName());
      TileID.Sets.DisableSmartCursor[(int) ((ModBlockType) this).Type] = true;
      this.AdjTiles = new int[1]{ 26 };
    }

    public virtual void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
  }
}
