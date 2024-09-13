// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.SuspiciousEye
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;

#nullable disable
namespace Fargowiltas.Items.Summons
{
  public class SuspiciousEye : BaseSummon
  {
    public virtual string Texture => "Terraria/Images/Item_43";

    public override int NPCType => 4;

    public override bool ResetTimeWhenUsed => !NPC.downedBoss1;

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual bool CanUseItem(Player player) => !Main.dayTime;

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(43, 1).AddTile(18).Register();
    }
  }
}
