// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.FakeHeartDeviantt
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
  public class FakeHeartDeviantt : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 12;
      ((Entity) this.Projectile).height = 12;
      this.Projectile.timeLeft = 600;
      this.Projectile.friendly = true;
      this.Projectile.npcProj = true;
      this.Projectile.aiStyle = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
    }

    public virtual void AI()
    {
      float num1 = (float) ((double) Main.rand.Next(90, 111) * 0.0099999997764825821 * ((double) Main.essScale * 0.5));
      Lighting.AddLight(((Entity) this.Projectile).Center, 0.5f * num1, 0.1f * num1, 0.1f * num1);
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        this.Projectile.ai[0] = -1f;
      }
      if ((double) this.Projectile.ai[0] >= 0.0 && (double) this.Projectile.ai[0] < (double) Main.maxNPCs)
      {
        int index = (int) this.Projectile.ai[0];
        if (Main.npc[index].CanBeChasedBy((object) null, false))
        {
          double num2 = (double) Utils.ToRotation(Vector2.op_Subtraction(((Entity) Main.npc[index]).Center, ((Entity) this.Projectile).Center)) - (double) Utils.ToRotation(((Entity) this.Projectile).velocity);
          if (num2 > Math.PI)
            num2 -= 2.0 * Math.PI;
          if (num2 < -1.0 * Math.PI)
            num2 += 2.0 * Math.PI;
          ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, num2 * ((double) ((Entity) this.Projectile).Distance(((Entity) Main.npc[index]).Center) > 100.0 ? 0.40000000596046448 : 0.10000000149011612), new Vector2());
        }
        else
        {
          this.Projectile.ai[0] = -1f;
          this.Projectile.netUpdate = true;
        }
      }
      else if ((double) ++this.Projectile.localAI[1] > 6.0)
      {
        this.Projectile.localAI[1] = 0.0f;
        float num3 = 700f;
        int num4 = -1;
        for (int index = 0; index < Main.maxNPCs; ++index)
        {
          NPC npc = Main.npc[index];
          if (npc.CanBeChasedBy((object) null, false) && Collision.CanHitLine(((Entity) this.Projectile).Center, 0, 0, ((Entity) npc).Center, 0, 0))
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
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) - 1.57079637f;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(119, 600, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) ((Color) ref lightColor).G, (int) ((Color) ref lightColor).B, (int) ((Color) ref lightColor).A), this.Projectile.Opacity));
    }
  }
}
