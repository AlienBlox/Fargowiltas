// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.LumberJaxe
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Projectiles
{
  public class LumberJaxe : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 42;
      ((Entity) this.Projectile).height = 40;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.penetrate = 1;
      this.Projectile.aiStyle = 0;
      this.Projectile.timeLeft = 150;
      this.AIType = 89;
    }

    public virtual void AI() => this.Projectile.rotation += 0.3f;

    public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
      if (target.type == 325 || target.type == 344 || target.type == 326)
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Multiply(local, 10f);
      }
      base.ModifyHitNPC(target, ref modifiers);
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      this.Projectile.Kill();
    }

    public virtual void OnKill(int timeLeft)
    {
      Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center.X, ((Entity) this.Projectile).Center.Y, ((Entity) this.Projectile).velocity.X * 0.0f, ((Entity) this.Projectile).velocity.Y * 0.0f, ModContent.ProjectileType<Explosion>(), (int) ((double) this.Projectile.damage * 1.0), this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
    }
  }
}
