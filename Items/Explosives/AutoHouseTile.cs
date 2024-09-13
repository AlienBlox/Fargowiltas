// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Explosives.AutoHouseTile
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Projectiles.Explosives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Explosives
{
  public class AutoHouseTile : ModTile
  {
    public virtual void SetStaticDefaults()
    {
      Main.tileSolid[(int) ((ModBlockType) this).Type] = true;
      Main.tileMergeDirt[(int) ((ModBlockType) this).Type] = true;
      Main.tileBlockLight[(int) ((ModBlockType) this).Type] = true;
      Main.tileLighted[(int) ((ModBlockType) this).Type] = true;
    }

    public virtual void KillTile(
      int i,
      int j,
      ref bool fail,
      ref bool effectOnly,
      ref bool noItem)
    {
      if (Main.netMode != 1)
      {
        int closest = (int) Player.FindClosest(new Vector2((float) (i * 16 + 8), (float) (j * 16 + 8)), 0, 0);
        if (closest != -1)
          Projectile.NewProjectile((IEntitySource) new EntitySource_TileBreak(i, j, (string) null), (float) (i * 16 + 8), (float) ((j + 2) * 16), 0.0f, 0.0f, ModContent.ProjectileType<AutoHouseProj>(), 0, 0.0f, closest, 0.0f, 0.0f, 0.0f);
      }
      noItem = true;
    }

    public virtual void NearbyEffects(int i, int j, bool closer)
    {
      WorldGen.KillTile(i, j, false, false, false);
      if (Main.netMode == 0)
        return;
      NetMessage.SendData(17, -1, -1, (NetworkText) null, 0, (float) i, (float) j, 0.0f, 0, 0, 0);
    }

    public virtual bool PreDraw(int i, int j, SpriteBatch spriteBatch) => false;
  }
}
