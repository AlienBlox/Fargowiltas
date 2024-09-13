// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.Deviantt.RuneOrb
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Common.Systems.Recipes;
using Terraria;

#nullable disable
namespace Fargowiltas.Items.Summons.Deviantt
{
  public class RuneOrb : BaseSummon
  {
    public override int NPCType => 172;

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual bool CanUseItem(Player player)
    {
      return !Main.dayTime || player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight || player.ZoneUnderworldHeight;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(238, 1).AddRecipeGroup(RecipeGroups.AnyGemRobe, 1).AddIngredient(154, 10).AddTile(134).Register();
    }
  }
}
