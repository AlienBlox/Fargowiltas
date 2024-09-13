// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.Mutant.PrismaticPrimrose
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;

#nullable disable
namespace Fargowiltas.Items.Summons.Mutant
{
  public class PrismaticPrimrose : BaseSummon
  {
    public override int NPCType => 636;

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(4961, 1).AddTile(18).Register();
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
      if (!Main.dayTime && !NPC.downedEmpressOfLight)
      {
        Main.dayTime = false;
        Main.time = 0.0;
        if (Main.netMode == 2)
          NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      return base.Shoot(player, source, position, velocity, type, damage, knockback);
    }
  }
}
