﻿// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Renewals.PurityRenewalSupreme
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Renewals
{
  public class PurityRenewalSupreme : BaseRenewalItem
  {
    public PurityRenewalSupreme()
      : base("Purity Renewal Supreme", "Cleanses the entire world", -1, true, ModContent.ItemType<PurityRenewal>())
    {
    }

    public virtual bool Shoot(
      Player player,
      EntitySource_ItemUse_WithAmmo source,
      Vector2 position,
      Vector2 velocity,
      int type,
      int damage,
      float knockback)
    {
      Projectile.NewProjectile(player.GetSource_ItemUse(((EntitySource_ItemUse) source).Item, (string) null), position, velocity, ModContent.ProjectileType<PurityNukeSupremeProj>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      return false;
    }
  }
}
