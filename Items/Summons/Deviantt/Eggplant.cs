// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.Deviantt.Eggplant
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Summons.Deviantt
{
  public class Eggplant : BaseSummon
  {
    public override int NPCType => 52;

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual bool CanUseItem(Player player)
    {
      return !Main.dayTime || player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight || player.ZoneUnderworldHeight;
    }

    public virtual void AddRecipes()
    {
      ModItem modItem;
      if (!ModContent.TryFind<ModItem>("Fargowiltas/Deviantt", ref modItem))
        return;
      Recipe(4292);
      Recipe(4294);

      void Recipe(int fruit)
      {
        this.CreateRecipe(1).AddIngredient(fruit, 1).AddIngredient(331, 4).AddIngredient(210, 2).AddIngredient(195, 2).AddIngredient(modItem.Type, 1).AddTile(16).Register();
      }
    }
  }
}
