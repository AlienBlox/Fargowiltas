// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.Deviantt.SuspiciousLookingChest
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;
using Terraria.ID;

#nullable disable
namespace Fargowiltas.Items.Summons.Deviantt
{
  public class SuspiciousLookingChest : BaseSummon
  {
    public override int NPCType => !Main.LocalPlayer.ZoneSnow ? 85 : 629;

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddRecipeGroup(RecipeGroupID.IronBar, 10).AddIngredient(2350, 5).AddIngredient(48, 1).AddTile(26).Register();
    }
  }
}
