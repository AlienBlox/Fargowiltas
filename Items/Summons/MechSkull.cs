// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.MechSkull
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;

#nullable disable
namespace Fargowiltas.Items.Summons
{
  public class MechSkull : BaseSummon
  {
    public virtual string Texture => "Terraria/Images/Item_557";

    public override int NPCType => (int) sbyte.MaxValue;

    public override bool ResetTimeWhenUsed => !NPC.downedMechBoss3;

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual bool CanUseItem(Player player) => !Main.dayTime;

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(557, 1).AddTile(18).Register();
    }
  }
}
