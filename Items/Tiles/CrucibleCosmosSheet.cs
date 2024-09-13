// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Tiles.CrucibleCosmosSheet
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
  public class CrucibleCosmosSheet : ModTile
  {
    public virtual void SetStaticDefaults()
    {
      Main.tileFrameImportant[(int) ((ModBlockType) this).Type] = true;
      TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
      TileObjectData.newTile.Width = 4;
      Main.tileNoAttach[(int) ((ModBlockType) this).Type] = true;
      TileObjectData.newTile.CoordinateHeights = new int[3]
      {
        16,
        16,
        16
      };
      TileObjectData.addTile((int) ((ModBlockType) this).Type);
      this.AddMapEntry(new Color(200, 200, 200), ((ModBlockType) this).CreateMapEntryName());
      TileID.Sets.DisableSmartCursor[(int) ((ModBlockType) this).Type] = true;
      this.AdjTiles = new int[39]
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
        94,
        77,
        355,
        114,
        243,
        228,
        304,
        302,
        306,
        308,
        305,
        220,
        300,
        134,
        133,
        26,
        101,
        125,
        247,
        412,
        499,
        301,
        303,
        307,
        217,
        218,
        85,
        ModContent.TileType<GoldenDippingVatSheet>()
      };
      TileID.Sets.CountsAsHoneySource[(int) ((ModBlockType) this).Type] = true;
      TileID.Sets.CountsAsLavaSource[(int) ((ModBlockType) this).Type] = true;
      TileID.Sets.CountsAsWaterSource[(int) ((ModBlockType) this).Type] = true;
      this.AnimationFrameHeight = 54;
    }

    public virtual void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

    public virtual void AnimateTile(ref int frame, ref int frameCounter)
    {
      ++frameCounter;
      if (frameCounter < 5)
        return;
      frameCounter = 0;
      ++frame;
      frame %= 8;
    }

    public virtual void NearbyEffects(int i, int j, bool closer)
    {
      if ((double) ((Entity) Main.LocalPlayer).Distance(new Vector2((float) (i * 16 + 8), (float) (j * 16 + 8))) >= 80.0)
        return;
      Main.LocalPlayer.GetModPlayer<FargoPlayer>().ElementalAssemblerNearby = 6f;
    }
  }
}
