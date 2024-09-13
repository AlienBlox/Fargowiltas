// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Tiles.MutantToiletSheet
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

#nullable disable
namespace Fargowiltas.Items.Tiles
{
  public class MutantToiletSheet : ModTile
  {
    public const int NextStyleHeight = 40;

    public virtual void SetStaticDefaults()
    {
      Main.tileFrameImportant[(int) ((ModBlockType) this).Type] = true;
      Main.tileNoAttach[(int) ((ModBlockType) this).Type] = true;
      Main.tileLavaDeath[(int) ((ModBlockType) this).Type] = true;
      TileID.Sets.HasOutlines[(int) ((ModBlockType) this).Type] = true;
      TileID.Sets.CanBeSatOnForNPCs[(int) ((ModBlockType) this).Type] = true;
      TileID.Sets.CanBeSatOnForPlayers[(int) ((ModBlockType) this).Type] = true;
      TileID.Sets.DisableSmartCursor[(int) ((ModBlockType) this).Type] = true;
      this.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);
      ((ModBlockType) this).DustType = 265;
      this.AdjTiles = new int[2]{ 15, 497 };
      this.AddMapEntry(new Color(200, 200, 200), Language.GetText("MapObject.Toilet"));
      TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
      TileObjectData.newTile.CoordinateHeights = new int[2]
      {
        16,
        18
      };
      TileObjectData.newTile.CoordinatePaddingFix = new Point16(0, 2);
      TileObjectData.newTile.Direction = (TileObjectDirection) 1;
      TileObjectData.newTile.StyleWrapLimit = 2;
      TileObjectData.newTile.StyleMultiplier = 2;
      TileObjectData.newTile.StyleHorizontal = true;
      TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
      TileObjectData.newAlternate.Direction = (TileObjectDirection) 2;
      TileObjectData.addAlternate(1);
      TileObjectData.addTile((int) ((ModBlockType) this).Type);
    }

    public virtual void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

    public virtual void KillMultiTile(int i, int j, int frameX, int frameY)
    {
      Item.NewItem((IEntitySource) new EntitySource_TileBreak(i, j, (string) null), i * 16, j * 16, 16, 32, ModContent.ItemType<MutantToilet>(), 1, false, 0, false, false);
    }

    public virtual bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
    {
      return settings.player.IsWithinSnappngRangeToTile(i, j, 40);
    }

    public virtual void ModifySittingTargetInfo(int i, int j, ref TileRestingInfo info)
    {
      Tile tileSafely = Framing.GetTileSafely(i, j);
      info.TargetDirection = -1;
      if (((Tile) ref tileSafely).TileFrameX != (short) 0)
        info.TargetDirection = 1;
      info.AnchorTilePosition.X = i;
      info.AnchorTilePosition.Y = j;
      if ((int) ((Tile) ref tileSafely).TileFrameY % 40 == 0)
        ++info.AnchorTilePosition.Y;
      ModBuff modBuff;
      if (!(info.RestingEntity is Player restingEntity) || !Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"] || !ModContent.TryFind<ModBuff>("FargowiltasSouls/MutantPresenceBuff", ref modBuff))
        return;
      restingEntity.AddBuff(modBuff.Type, 2, true, false);
    }

    public virtual bool RightClick(int i, int j)
    {
      Player localPlayer = Main.LocalPlayer;
      if (localPlayer.IsWithinSnappngRangeToTile(i, j, 40))
      {
        localPlayer.GamepadEnableGrappleCooldown();
        ((PlayerSittingHelper) ref localPlayer.sitting).SitDown(localPlayer, i, j);
      }
      return true;
    }

    public virtual void MouseOver(int i, int j)
    {
      Player localPlayer = Main.LocalPlayer;
      if (!localPlayer.IsWithinSnappngRangeToTile(i, j, 40))
        return;
      localPlayer.noThrow = 2;
      localPlayer.cursorItemIconEnabled = true;
      localPlayer.cursorItemIconID = ModContent.ItemType<MutantToilet>();
      Tile tile = ((Tilemap) ref Main.tile)[i, j];
      if ((int) ((Tile) ref tile).TileFrameX / 18 >= 1)
        return;
      localPlayer.cursorItemIconReversed = true;
    }

    public virtual void HitWire(int i, int j)
    {
      Tile tile = ((Tilemap) ref Main.tile)[i, j];
      int num1 = i;
      int num2 = j - (int) ((Tile) ref tile).TileFrameY % 40 / 18;
      Wiring.SkipWire(num1, num2);
      Wiring.SkipWire(num1, num2 + 1);
      if (!Wiring.CheckMech(num1, num2, 60))
        return;
      Projectile.NewProjectile(Wiring.GetProjectileSource(num1, num2), (float) (num1 * 16 + 8), (float) (num2 * 16 + 12), 0.0f, 0.0f, 733, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }
  }
}
