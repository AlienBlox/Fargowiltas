// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.DeathScythe
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
  public class DeathScythe : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_274";

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 42;
      ((Entity) this.Projectile).height = 42;
      this.Projectile.friendly = true;
      this.Projectile.npcProj = true;
      this.Projectile.penetrate = 50;
      this.Projectile.scale = 1f;
      this.Projectile.timeLeft = 180;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 10;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        this.Projectile.ai[0] = -1f;
        SoundEngine.PlaySound(ref SoundID.Item71, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      ++this.Projectile.rotation;
      ++this.Projectile.ai[1];
      if ((double) this.Projectile.ai[1] <= 30.0)
        return;
      this.Projectile.ai[1] = 30f;
      this.Projectile.ai[0] = (float) this.HomeOnTarget();
      if ((double) this.Projectile.ai[0] > -1.0 && (double) this.Projectile.ai[0] < 200.0)
      {
        NPC npc = Main.npc[(int) this.Projectile.ai[0]];
        if (!((Entity) npc).active || !npc.CanBeChasedBy((object) null, false))
          return;
        ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(((Entity) this.Projectile).DirectionTo(((Entity) npc).Center), 70f), 0.1f);
      }
      else
        this.Projectile.ai[0] = -1f;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(153, 600, false);
    }

    private int HomeOnTarget()
    {
      int index1 = -1;
      for (int index2 = 0; index2 < Main.maxNPCs; ++index2)
      {
        NPC npc = Main.npc[index2];
        if (npc.CanBeChasedBy((object) this.Projectile, false))
        {
          int num1 = ((Entity) npc).wet ? 1 : 0;
          float num2 = ((Entity) this.Projectile).Distance(((Entity) npc).Center);
          if ((double) num2 <= 1000.0 && (index1 == -1 || (double) ((Entity) this.Projectile).Distance(((Entity) Main.npc[index1]).Center) > (double) num2))
            index1 = index2;
        }
      }
      return index1;
    }
  }
}
