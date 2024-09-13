// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Tiles.ElementalAssemblerSheet
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
  public class ElementalAssemblerSheet : ModTile
  {
    public virtual void SetStaticDefaults()
    {
      Main.tileLighted[(int) ((ModBlockType) this).Type] = true;
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
      this.AdjTiles = new int[17]
      {
        77,
        17,
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
        13,
        26,
        85,
        622
      };
      TileID.Sets.CountsAsHoneySource[(int) ((ModBlockType) this).Type] = true;
      TileID.Sets.CountsAsLavaSource[(int) ((ModBlockType) this).Type] = true;
      this.AnimationFrameHeight = 54;
    }

    public virtual void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
      float num = Utils.NextFloat(Main.rand, 0.9f, 1f);
      r = g = b = num;
    }

    public virtual void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

    public virtual void AnimateTile(ref int frame, ref int frameCounter)
    {
      ++frameCounter;
      if (frameCounter >= 8)
      {
        frameCounter = 0;
        ++frame;
        frame %= 8;
      }
      Main.tileLighted[(int) ((ModBlockType) this).Type] = true;
    }

    public virtual void NearbyEffects(int i, int j, bool closer)
    {
      if ((double) ((Entity) Main.LocalPlayer).Distance(new Vector2((float) (i * 16 + 8), (float) (j * 16 + 8))) >= 80.0)
        return;
      Main.LocalPlayer.GetModPlayer<FargoPlayer>().ElementalAssemblerNearby = 6f;
    }
  }
}
