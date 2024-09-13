// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.FakeHeart2Deviantt
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Projectiles
{
  public class FakeHeart2Deviantt : ModProjectile
  {
    public virtual string Texture => "Fargowiltas/Projectiles/FakeHeartDeviantt";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 7;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 12;
      ((Entity) this.Projectile).height = 12;
      this.Projectile.timeLeft = 600;
      this.Projectile.friendly = true;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = 2;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.extraUpdates = 1;
    }

    public virtual void AI()
    {
      float num1 = (float) ((double) Main.rand.Next(90, 111) * 0.0099999997764825821 * ((double) Main.essScale * 0.5));
      Lighting.AddLight(((Entity) this.Projectile).Center, 0.5f * num1, 0.1f * num1, 0.1f * num1);
      if ((double) ++this.Projectile.localAI[0] == 30.0)
      {
        this.Projectile.localAI[1] = Utils.ToRotation(((Entity) this.Projectile).velocity);
        ((Entity) this.Projectile).velocity = Vector2.Zero;
      }
      if ((double) --this.Projectile.ai[1] == 0.0)
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.localAI[1]), -12.5f);
      else if ((double) this.Projectile.ai[1] < 0.0)
      {
        if ((double) this.Projectile.ai[0] >= 0.0 && (double) this.Projectile.ai[0] < 200.0)
        {
          int index = (int) this.Projectile.ai[0];
          if (Main.npc[index].CanBeChasedBy((object) null, false))
          {
            double num2 = (double) Utils.ToRotation(Vector2.op_Subtraction(((Entity) Main.npc[index]).Center, ((Entity) this.Projectile).Center)) - (double) Utils.ToRotation(((Entity) this.Projectile).velocity);
            if (num2 > Math.PI)
              num2 -= 2.0 * Math.PI;
            if (num2 < -1.0 * Math.PI)
              num2 += 2.0 * Math.PI;
            ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, num2 * ((double) ((Entity) this.Projectile).Distance(((Entity) Main.npc[index]).Center) > 100.0 ? 0.60000002384185791 : 0.20000000298023224), new Vector2());
          }
          else
          {
            this.Projectile.ai[0] = -1f;
            this.Projectile.netUpdate = true;
          }
        }
        else if ((double) ++this.Projectile.localAI[1] > 12.0)
        {
          this.Projectile.localAI[1] = 0.0f;
          float num3 = 700f;
          int num4 = -1;
          for (int index = 0; index < Main.maxNPCs; ++index)
          {
            NPC npc = Main.npc[index];
            if (npc.CanBeChasedBy((object) null, false))
            {
              float num5 = ((Entity) this.Projectile).Distance(((Entity) npc).Center);
              if ((double) num5 < (double) num3)
              {
                num3 = num5;
                num4 = index;
              }
            }
          }
          this.Projectile.ai[0] = (float) num4;
          this.Projectile.netUpdate = true;
        }
      }
      if (Vector2.op_Inequality(((Entity) this.Projectile).velocity, Vector2.Zero))
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
      this.Projectile.rotation -= 1.57079637f;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.immune[this.Projectile.owner] = 5;
      target.AddBuff(119, 600, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(new Color((int) byte.MaxValue, (int) ((Color) ref lightColor).G, (int) ((Color) ref lightColor).B, (int) ((Color) ref lightColor).A));
    }
  }
}
