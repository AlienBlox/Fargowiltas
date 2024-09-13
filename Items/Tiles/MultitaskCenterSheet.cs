// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Tiles.MultitaskCenterSheet
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
  public class MultitaskCenterSheet : ModTile
  {
    public virtual void SetStaticDefaults()
    {
      Main.tileLighted[(int) ((ModBlockType) this).Type] = true;
      Main.tileSolidTop[(int) ((ModBlockType) this).Type] = true;
      Main.tileTable[(int) ((ModBlockType) this).Type] = true;
      Main.tileFrameImportant[(int) ((ModBlockType) this).Type] = true;
      TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
      TileObjectData.newTile.Width = 4;
      Main.tileNoAttach[(int) ((ModBlockType) this).Type] = false;
      TileObjectData.newTile.CoordinateHeights = new int[3]
      {
        16,
        16,
        16
      };
      TileObjectData.addTile((int) ((ModBlockType) this).Type);
      this.AddMapEntry(new Color(200, 200, 200), ((ModBlockType) this).CreateMapEntryName());
      TileID.Sets.DisableSmartCursor[(int) ((ModBlockType) this).Type] = true;
      this.AdjTiles = new int[12]
      {
        18,
        283,
        17,
        16,
        13,
        106,
        86,
        14,
        15,
        96,
        172,
        94
      };
      TileID.Sets.CountsAsWaterSource[(int) ((ModBlockType) this).Type] = true;
      this.AnimationFrameHeight = 54;
    }

    public virtual void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
      r = 0.93f;
      g = 0.11f;
      b = 0.12f;
    }

    public virtual void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

    public virtual void AnimateTile(ref int frame, ref int frameCounter)
    {
      ++frameCounter;
      if (frameCounter < 10)
        return;
      frameCounter = 0;
      ++frame;
      frame %= 7;
    }
  }
}
