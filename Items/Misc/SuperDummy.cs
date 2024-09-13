// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Misc.SuperDummy
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Projectiles;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Misc
{
  public class SuperDummy : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 30;
      this.Item.useTime = 15;
      this.Item.useAnimation = 15;
      this.Item.useStyle = 1;
      this.Item.useTurn = true;
      this.Item.rare = 1;
    }

    public virtual bool AltFunctionUse(Player player) => true;

    public virtual bool? UseItem(Player player)
    {
      if (player.altFunctionUse == 2)
      {
        if (((Entity) player).whoAmI == Main.myPlayer)
        {
          switch (Main.netMode)
          {
            case 0:
              for (int index = 0; index < Main.maxNPCs; ++index)
              {
                if (((Entity) Main.npc[index]).active && Main.npc[index].type == ModContent.NPCType<Fargowiltas.NPCs.SuperDummy>())
                {
                  NPC npc = Main.npc[index];
                  npc.life = 0;
                  npc.HitEffect(0, 10.0, new bool?());
                  Main.npc[index].SimpleStrikeNPC(int.MaxValue, 0, false, 0.0f, (DamageClass) null, false, 0.0f, true);
                }
              }
              break;
            case 1:
              ModPacket packet = ((ModType) this).Mod.GetPacket(256);
              ((BinaryWriter) packet).Write((byte) 5);
              packet.Send(-1, -1);
              break;
          }
        }
      }
      else if (NPC.CountNPCS(ModContent.NPCType<Fargowiltas.NPCs.SuperDummy>()) < 50)
      {
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector((float) ((int) Main.MouseWorld.X - 9), (float) ((int) Main.MouseWorld.Y - 20));
        Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), vector2, Vector2.Zero, ModContent.ProjectileType<SpawnProj>(), 0, 0.0f, ((Entity) player).whoAmI, (float) ModContent.NPCType<Fargowiltas.NPCs.SuperDummy>(), 0.0f, 0.0f);
      }
      return new bool?(true);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(3202, 1).AddIngredient(75, 1).AddTile(96).Register();
    }
  }
}
