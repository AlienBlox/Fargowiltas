// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.WorldTokenProj
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Projectiles
{
  public class WorldTokenProj : ModProjectile
  {
    public virtual string Texture => "Fargowiltas/Items/Misc/ModeToggle_0";

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
      ((Entity) this.Projectile).width = 32;
      ((Entity) this.Projectile).height = 32;
      this.Projectile.friendly = false;
      this.Projectile.penetrate = -1;
      this.Projectile.scale = 1f;
      this.Projectile.timeLeft = 60;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
    }

    public virtual void AI()
    {
      Player player;
      if (this.Projectile.TryGetOwner(ref player))
        ((Entity) this.Projectile).Center = Vector2.op_Subtraction(((Entity) player).Center, Vector2.op_Multiply(Vector2.UnitY, 100f));
      this.Projectile.scale += 0.0333333351f;
      this.Projectile.Opacity -= 0.0166666675f;
      if ((double) this.Projectile.Opacity > 0.0099999997764825821)
        return;
      this.Projectile.Kill();
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 1);
      interpolatedStringHandler.AppendLiteral("Fargowiltas/Items/Misc/ModeToggle_");
      interpolatedStringHandler.AppendFormatted<int>(Main.GameMode);
      Texture2D texture2D = Asset<Texture2D>.op_Explicit(ModContent.Request<Texture2D>(interpolatedStringHandler.ToStringAndClear(), (AssetRequestMode) 2));
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, 0, texture2D.Width, texture2D.Height);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Main.spriteBatch.Draw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
