// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.VanillaCopy.LihzahrdPowerCell2
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;

#nullable disable
namespace Fargowiltas.Items.Summons.VanillaCopy
{
  public class LihzahrdPowerCell2 : BaseSummon
  {
    public override int NPCType => 245;

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual bool CanUseItem(Player player) => NPC.downedPlantBoss;

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(1293, 1).AddTile(18).Register();
    }
  }
}
