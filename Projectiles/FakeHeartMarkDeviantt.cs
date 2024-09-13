// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.FakeHeartMarkDeviantt
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Projectiles
{
  public class FakeHeartMarkDeviantt : ModProjectile
  {
    public virtual string Texture => "Fargowiltas/Projectiles/FakeHeartDeviantt";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 12;
      ((Entity) this.Projectile).height = 12;
      this.Projectile.timeLeft = 2;
      this.Projectile.aiStyle = -1;
      this.Projectile.hide = true;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
    }

    public virtual void AI()
    {
      if (Main.netMode != 1)
      {
        for (int index = -3; index < 3; ++index)
          Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_UnaryNegation(Utils.RotatedBy(((Entity) this.Projectile).velocity, Math.PI / 7.0 * (double) index, new Vector2())), ModContent.ProjectileType<FakeHeart2Deviantt>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, -1f, (float) (120 + 20 * index), 0.0f);
      }
      this.Projectile.Kill();
    }

    public virtual bool? CanDamage() => new bool?(false);
  }
}
