// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.Explosives.InstaHouseVisual
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Items.Explosives;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Projectiles.Explosives
{
  public class InstaHouseVisual : ModProjectile
  {
    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 160;
      ((Entity) this.Projectile).height = 96;
      this.Projectile.timeLeft = 10;
      this.Projectile.tileCollide = false;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      Vector2 mouseWorld = Main.MouseWorld;
      if ((double) ((Entity) player).position.X > (double) mouseWorld.X)
      {
        ((Entity) this.Projectile).position.X = (float) ((double) mouseWorld.X - (double) ((Entity) this.Projectile).width + 4.0);
        ((Entity) this.Projectile).position.Y = (float) ((double) mouseWorld.Y - (double) ((Entity) this.Projectile).height + 8.0 + 16.0);
      }
      else
      {
        ((Entity) this.Projectile).position.X = mouseWorld.X - 4f;
        ((Entity) this.Projectile).position.Y = (float) ((double) mouseWorld.Y - (double) ((Entity) this.Projectile).height + 8.0 + 16.0);
      }
      ++this.Projectile.timeLeft;
      if (player.HeldItem.type != ModContent.ItemType<AutoHouse>())
        this.Projectile.Kill();
      this.Projectile.hide = this.Projectile.owner != Main.myPlayer;
    }
  }
}
