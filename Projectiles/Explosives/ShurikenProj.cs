// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.Explosives.ShurikenProj
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Projectiles.Explosives
{
  public class ShurikenProj : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 11;
      ((Entity) this.Projectile).height = 11;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Default;
      this.Projectile.penetrate = 5;
      this.Projectile.aiStyle = 2;
      this.Projectile.timeLeft = 600;
      this.AIType = 48;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      this.Projectile.timeLeft = 0;
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      this.Projectile.timeLeft = 0;
      return false;
    }

    public virtual void OnKill(int timeLeft)
    {
      if (this.Projectile.owner == Main.myPlayer)
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center.X, ((Entity) this.Projectile).Center.Y, 0.0f, 0.0f, ModContent.ProjectileType<Explosion>(), 0, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      Vector2 center = ((Entity) this.Projectile).Center;
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(center), (SoundUpdateCallback) null);
      int num1 = 16;
      Player player = Main.player[this.Projectile.owner];
      Item bestPickaxe = player.GetBestPickaxe();
      for (int index1 = -num1; index1 <= num1; ++index1)
      {
        for (int index2 = -num1; index2 <= num1; ++index2)
        {
          int num2 = (int) ((double) index1 + (double) center.X / 16.0);
          int num3 = (int) ((double) index2 + (double) center.Y / 16.0);
          if (num2 >= 0 && num2 < Main.maxTilesX && num3 >= 0 && num3 < Main.maxTilesY)
          {
            Tile tile = ((Tilemap) ref Main.tile)[num2, num3];
            if (index1 * index1 + index2 * index2 <= num1)
            {
              if (this.Projectile.owner == Main.myPlayer)
              {
                for (int index3 = 0; index3 < 6 && !((Tile) ref tile).IsActuated && !FargoGlobalProjectile.TileIsLiterallyAir(tile) && !FargoGlobalProjectile.TileBelongsToMagicStorage(tile); ++index3)
                  player.PickTile(num2, num3, bestPickaxe != null ? bestPickaxe.pick : 35);
              }
              Dust.NewDust(center, 22, 22, 31, 0.0f, 0.0f, 120, new Color(), 1f);
            }
          }
        }
      }
    }
  }
}
