// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Misc.InstantResearch
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Misc
{
  public class InstantResearch : ModItem
  {
    public virtual string Texture => "Fargowiltas/Items/Placeholder";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 18;
      ((Entity) this.Item).height = 18;
      this.Item.maxStack = 1;
      this.Item.rare = 1;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 4;
    }

    public virtual bool? UseItem(Player player)
    {
      if (player.itemAnimation > 0 && player.itemTime == 0)
      {
        int num1 = 0;
        for (int index = 0; index < ContentSamples.ItemsByType.Count; ++index)
        {
          int num2;
          if (CreativeItemSacrificesCatalog.Instance.TryGetSacrificeCountCapToUnlockInfiniteItems(index, ref num2))
          {
            int num3 = num2 - player.creativeTracker.ItemSacrifices.GetSacrificeCount(index);
            if (num3 > 0)
            {
              player.creativeTracker.ItemSacrifices.RegisterItemSacrifice(index, num3);
              ++num1;
            }
          }
        }
        FargoUtils.PrintLocalization("Items.InstantResearch.ResearchText", (object) num1);
      }
      return new bool?(true);
    }
  }
}
