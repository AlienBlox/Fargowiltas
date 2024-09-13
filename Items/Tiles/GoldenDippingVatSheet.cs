// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Tiles.GoldenDippingVatSheet
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

#nullable disable
namespace Fargowiltas.Items.Tiles
{
  public class GoldenDippingVatSheet : ModTile
  {
    public virtual void SetStaticDefaults()
    {
      Main.tileFrameImportant[(int) ((ModBlockType) this).Type] = true;
      Main.tileObsidianKill[(int) ((ModBlockType) this).Type] = true;
      Main.tileNoAttach[(int) ((ModBlockType) this).Type] = true;
      TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
      TileObjectData.newTile.LavaDeath = true;
      TileObjectData.addTile((int) ((ModBlockType) this).Type);
      this.AddMapEntry(new Color((int) byte.MaxValue, 215, 0), ((ModBlockType) this).CreateMapEntryName());
      this.AnimationFrameHeight = 54;
    }

    public virtual void AnimateTile(ref int frame, ref int frameCounter)
    {
      ++frameCounter;
      if (frameCounter < 10)
        return;
      frameCounter = 0;
      ++frame;
      frame %= 12;
    }
  }
}
