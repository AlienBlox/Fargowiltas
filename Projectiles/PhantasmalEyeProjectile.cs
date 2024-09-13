// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.PhantasmalEyeProjectile
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Projectiles
{
  public class PhantasmalEyeProjectile : ModProjectile
  {
    private float HomingCooldown
    {
      get => this.Projectile.ai[0];
      set => this.Projectile.ai[0] = value;
    }

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 4;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 9;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.npcProj = true;
      this.Projectile.tileCollide = false;
      this.Projectile.penetrate = 50;
      this.Projectile.timeLeft = 600;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 10;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(69, 240, false);
      target.AddBuff(203, 240, false);
    }

    public virtual void AI()
    {
      ++this.HomingCooldown;
      if ((double) this.HomingCooldown > 10.0)
      {
        this.HomingCooldown = 10f;
        int index = this.HomeOnTarget();
        if (index != -1)
        {
          NPC npc = Main.npc[index];
          if ((double) ((Entity) this.Projectile).Distance(((Entity) npc).Center) > (double) Math.Max(((Entity) npc).width, ((Entity) npc).height))
          {
            ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(((Entity) this.Projectile).DirectionTo(((Entity) npc).Center), 60f), 0.05f);
            double num = (double) Utils.ToRotation(Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) this.Projectile).Center)) - (double) Utils.ToRotation(((Entity) this.Projectile).velocity);
            if (num > Math.PI)
              num -= 2.0 * Math.PI;
            if (num < -1.0 * Math.PI)
              num += 2.0 * Math.PI;
            ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, num * ((double) ((Entity) this.Projectile).Distance(((Entity) npc).Center) > 100.0 ? 0.40000000596046448 : 0.10000000149011612), new Vector2());
          }
        }
      }
      if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < 2.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.03f);
      }
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.570796f;
      this.Projectile.localAI[0] += 0.1f;
      if ((double) this.Projectile.localAI[0] > (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type])
        this.Projectile.localAI[0] = (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type];
      this.Projectile.localAI[1] += 0.25f;
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundStyle soundStyle = new SoundStyle("Terraria/Sounds/Zombie_103", (SoundType) 0);
      SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = 144;
      ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
      for (int index = 0; index < 2; ++index)
        Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 0, new Color(), 2.5f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 3f);
        int index3 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
        Main.dust[index3].noGravity = true;
      }
    }

    private int HomeOnTarget()
    {
      int index1 = -1;
      for (int index2 = 0; index2 < Main.maxNPCs; ++index2)
      {
        NPC npc = Main.npc[index2];
        if (npc.CanBeChasedBy((object) this.Projectile, false))
        {
          float num = ((Entity) this.Projectile).Distance(((Entity) npc).Center);
          if ((double) num <= 1000.0 && (index1 == -1 || (double) ((Entity) this.Projectile).Distance(((Entity) Main.npc[index1]).Center) > (double) num))
            index1 = index2;
        }
      }
      if (index1 == -1)
        index1 = NPC.FindFirstNPC(ModContent.NPCType<Mutant>());
      return index1;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = ModContent.Request<Texture2D>(this.Texture + "_Glow", (AssetRequestMode) 2).Value;
      int num1 = texture2D.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color color1 = Color.Lerp(new Color(31, 187, 192, 0), Color.Transparent, 0.74f);
      Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitX), 14f));
      for (int index = 0; index < 3; ++index)
      {
        Vector2 vector2_3 = Vector2.op_Subtraction(Vector2.op_Addition(vector2_2, Utils.RotatedBy(Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitX), 8f), 0.62831854820251465 - (double) index * 3.1415927410125732 / 5.0, new Vector2())), Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitX), 8f));
        float num3 = this.Projectile.scale + (float) Math.Sin((double) this.Projectile.localAI[1]) / 10f;
        Main.spriteBatch.Draw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_3, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color1, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2_1, num3, (SpriteEffects) 0, 0.0f);
      }
      for (float index = this.Projectile.localAI[0] - 1f; (double) index > 0.0; index -= this.Projectile.localAI[0] / 5f)
      {
        float num4 = 0.2f;
        if ((double) index > 5.0 && (double) index < 10.0)
          num4 = 0.4f;
        if ((double) index >= 10.0)
          num4 = 0.6f;
        Color color2 = Color.op_Multiply(Color.Lerp(color1, Color.Transparent, 0.1f + num4), (float) ((int) (((double) this.Projectile.localAI[0] - (double) index) / (double) this.Projectile.localAI[0]) ^ 2));
        float num5 = this.Projectile.scale * (this.Projectile.localAI[0] - index) / this.Projectile.localAI[0] + (float) Math.Sin((double) this.Projectile.localAI[1]) / 10f;
        Vector2 vector2_4 = Vector2.op_Subtraction(this.Projectile.oldPos[(int) index], Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitX), 14f));
        Main.spriteBatch.Draw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(vector2_4, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2_1, num5 * 0.8f, (SpriteEffects) 0, 0.0f);
      }
      return false;
    }

    public virtual void PostDraw(Color lightColor)
    {
      Texture2D texture2D = ModContent.Request<Texture2D>(this.Texture, (AssetRequestMode) 2).Value;
      int num1 = texture2D.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Main.spriteBatch.Draw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
    }
  }
}
