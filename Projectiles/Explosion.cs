// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.Explosion
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Projectiles
{
  public class Explosion : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 5;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 0;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 10;
      ((Entity) this.Projectile).height = 10;
      this.Projectile.aiStyle = 0;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 10;
      this.Projectile.tileCollide = false;
      this.Projectile.light = 0.75f;
      this.Projectile.ignoreWater = true;
      this.Projectile.extraUpdates = 1;
      this.AIType = 14;
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      ((Entity) this.Projectile).width = 100;
      ((Entity) this.Projectile).height = 100;
      ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
      for (int index1 = 0; index1 < 30; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
      }
      for (int index3 = 0; index3 < 2; ++index3)
      {
        float num = 0.4f;
        if (index3 == 1)
          num = 0.8f;
        int index4 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).position, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore1 = Main.gore[index4];
        gore1.velocity = Vector2.op_Multiply(gore1.velocity, num);
        ++Main.gore[index4].velocity.X;
        ++Main.gore[index4].velocity.Y;
        int index5 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).position, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore2 = Main.gore[index5];
        gore2.velocity = Vector2.op_Multiply(gore2.velocity, num);
        --Main.gore[index5].velocity.X;
        ++Main.gore[index5].velocity.Y;
        int index6 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).position, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore3 = Main.gore[index6];
        gore3.velocity = Vector2.op_Multiply(gore3.velocity, num);
        ++Main.gore[index6].velocity.X;
        --Main.gore[index6].velocity.Y;
        int index7 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).position, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore4 = Main.gore[index7];
        gore4.velocity = Vector2.op_Multiply(gore4.velocity, num);
        --Main.gore[index7].velocity.X;
        --Main.gore[index7].velocity.Y;
      }
    }
  }
}
