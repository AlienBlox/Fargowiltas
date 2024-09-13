// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.MechEyeProjectile
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
  public class MechEyeProjectile : ModProjectile
  {
    private float HomingCooldown
    {
      get => this.Projectile.ai[0];
      set => this.Projectile.ai[0] = value;
    }

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 8;
      ((Entity) this.Projectile).height = 12;
      this.Projectile.aiStyle = 1;
      this.Projectile.friendly = true;
      this.Projectile.npcProj = true;
      this.Projectile.penetrate = 5;
      this.Projectile.timeLeft = 600;
      this.AIType = 14;
    }

    public virtual void AI()
    {
      ++this.HomingCooldown;
      if ((double) this.HomingCooldown <= 10.0)
        return;
      this.HomingCooldown = 10f;
      int index = this.HomeOnTarget();
      if (index == -1)
        return;
      NPC npc = Main.npc[index];
      if ((double) ((Entity) this.Projectile).Distance(((Entity) npc).Center) <= (double) Math.Max(((Entity) npc).width, ((Entity) npc).height))
        return;
      ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(((Entity) this.Projectile).DirectionTo(((Entity) npc).Center), 60f), 0.05f);
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(new Vector2(((Entity) this.Projectile).Center.X, ((Entity) this.Projectile).Center.Y), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 11, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 2f);
        int index3 = Dust.NewDust(new Vector2(((Entity) this.Projectile).Center.X, ((Entity) this.Projectile).Center.Y), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 11, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 0.75f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
      }
    }

    private int HomeOnTarget()
    {
      int index1 = -1;
      for (int index2 = 0; index2 < Main.maxNPCs; ++index2)
      {
        NPC npc = Main.npc[index2];
        if (npc.CanBeChasedBy((object) this.Projectile, false) && Collision.CanHitLine(((Entity) this.Projectile).Center, 0, 0, ((Entity) npc).Center, 0, 0))
        {
          float num = ((Entity) this.Projectile).Distance(((Entity) npc).Center);
          if ((double) num <= 500.0 && (index1 == -1 || (double) ((Entity) this.Projectile).Distance(((Entity) Main.npc[index1]).Center) > (double) num))
            index1 = index2;
        }
      }
      return index1;
    }
  }
}
