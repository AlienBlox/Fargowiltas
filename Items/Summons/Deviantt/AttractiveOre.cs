// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.Deviantt.AttractiveOre
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;

#nullable disable
namespace Fargowiltas.Items.Summons.Deviantt
{
  public class AttractiveOre : BaseSummon
  {
    public override int NPCType => 44;

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual bool CanUseItem(Player player)
    {
      return !Main.dayTime || player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight || player.ZoneUnderworldHeight;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(3988, 1).AddIngredient(88, 1).AddIngredient(296, 1).AddTile(283).Register();
    }
  }
}
