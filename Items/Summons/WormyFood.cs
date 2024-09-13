﻿// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.WormyFood
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

#nullable disable
namespace Fargowiltas.Items.Summons
{
  public class WormyFood : BaseSummon
  {
    public virtual string Texture => "Terraria/Images/Item_70";

    public override int NPCType => 13;

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual bool CanUseItem(Player player) => !NPC.AnyNPCs(this.NPCType);

    public virtual bool? UseItem(Player player)
    {
      FargoUtils.SpawnBossNetcoded(player, this.NPCType);
      return new bool?(true);
    }

    public override bool Shoot(
      Player player,
      EntitySource_ItemUse_WithAmmo source,
      Vector2 position,
      Vector2 velocity,
      int type,
      int damage,
      float knockback)
    {
      return false;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(70, 1).AddTile(18).Register();
    }
  }
}
