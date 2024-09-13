// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Tiles.SemistationSheet
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
  public class SemistationSheet : ModTile
  {
    public virtual Color color => new Color(221, 85, 125);

    public virtual void SetStaticDefaults()
    {
      Main.tileLighted[(int) ((ModBlockType) this).Type] = true;
      Main.tileFrameImportant[(int) ((ModBlockType) this).Type] = true;
      TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
      TileObjectData.newTile.Height = 3;
      TileObjectData.newTile.CoordinateHeights = new int[4]
      {
        16,
        16,
        16,
        16
      };
      TileObjectData.addTile((int) ((ModBlockType) this).Type);
      this.AddMapEntry(this.color, ((ModBlockType) this).CreateMapEntryName());
    }

    public virtual bool CanDrop(int i, int j) => false;

    public virtual void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
      r = 1f;
      g = 1f;
      b = 1f;
    }

    public virtual void NearbyEffects(int i, int j, bool closer)
    {
      if (!closer || !((Entity) Main.LocalPlayer).active || Main.LocalPlayer.dead || Main.LocalPlayer.ghost)
        return;
      Main.LocalPlayer.AddBuff(ModContent.BuffType<Fargowiltas.Content.Buffs.Semistation>(), 30, true, false);
    }
  }
}
