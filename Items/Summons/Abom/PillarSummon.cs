// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.Abom.PillarSummon
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Summons.Abom
{
  public class PillarSummon : ModItem
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.maxStack = 20;
      this.Item.value = Item.sellPrice(0, 0, 2, 0);
      this.Item.rare = 9;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 5;
      this.Item.consumable = true;
      this.Item.shoot = ModContent.ProjectileType<SpawnProj>();
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
      int[] numArray = new int[4]{ 507, 517, 493, 422 };
      if (!NPC.AnyNPCs(517))
        NPC.ShieldStrengthTowerSolar = 0;
      if (!NPC.AnyNPCs(422))
        NPC.ShieldStrengthTowerVortex = 0;
      if (!NPC.AnyNPCs(507))
        NPC.ShieldStrengthTowerNebula = 0;
      if (!NPC.AnyNPCs(493))
        NPC.ShieldStrengthTowerStardust = 0;
      for (int index = 0; index < numArray.Length; ++index)
      {
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector((float) ((int) ((Entity) player).position.X + 400 * index - 600), (float) ((int) ((Entity) player).position.Y - 200));
        Projectile.NewProjectile(player.GetSource_ItemUse(((EntitySource_ItemUse) source).Item, (string) null), vector2, Vector2.Zero, type, 0, 0.0f, Main.myPlayer, (float) numArray[index], 0.0f, 0.0f);
      }
      FargoUtils.PrintLocalization("MessageInfo.StartLunarEvent", new Color(175, 75, (int) byte.MaxValue));
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      return false;
    }
  }
}
