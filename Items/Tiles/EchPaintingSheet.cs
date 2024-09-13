// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Tiles.EchPaintingSheet
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

#nullable disable
namespace Fargowiltas.Items.Tiles
{
  public class EchPaintingSheet : ModTile
  {
    public virtual void SetStaticDefaults()
    {
      Main.tileFrameImportant[(int) ((ModBlockType) this).Type] = true;
      Main.tileLavaDeath[(int) ((ModBlockType) this).Type] = true;
      Main.tileSpelunker[(int) ((ModBlockType) this).Type] = true;
      TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
      TileObjectData.newTile.StyleHorizontal = true;
      TileObjectData.newTile.StyleWrapLimit = 36;
      TileObjectData.addTile((int) ((ModBlockType) this).Type);
      ((ModBlockType) this).DustType = 7;
      TileID.Sets.DisableSmartCursor[(int) ((ModBlockType) this).Type] = true;
    }
  }
}
