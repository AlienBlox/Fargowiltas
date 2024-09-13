// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Tiles.SiblingPylonTile
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.NPCs;
using Fargowiltas.TileEntities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Terraria.ObjectData;

#nullable disable
namespace Fargowiltas.Items.Tiles
{
  public class SiblingPylonTile : ModPylon
  {
    public const int CrystalVerticalFrameCount = 8;
    public Asset<Texture2D> crystalTexture;
    public Asset<Texture2D> crystalHighlightTexture;
    public Asset<Texture2D> mapIcon;

    public virtual void Load()
    {
      this.crystalTexture = ModContent.Request<Texture2D>(((ModTexturedType) this).Texture + "_Crystal", (AssetRequestMode) 2);
      this.crystalHighlightTexture = ModContent.Request<Texture2D>(((ModTexturedType) this).Texture + "_CrystalHighlight", (AssetRequestMode) 2);
      this.mapIcon = ModContent.Request<Texture2D>(((ModTexturedType) this).Texture + "_MapIcon", (AssetRequestMode) 2);
    }

    public virtual void SetStaticDefaults()
    {
      Main.tileLighted[(int) ((ModBlockType) this).Type] = true;
      Main.tileFrameImportant[(int) ((ModBlockType) this).Type] = true;
      TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
      TileObjectData.newTile.LavaDeath = false;
      TileObjectData.newTile.DrawYOffset = 2;
      TileObjectData.newTile.StyleHorizontal = true;
      TEModdedPylon instance = (TEModdedPylon) ModContent.GetInstance<SiblingPylonTileEntity>();
      TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(new Func<int, int, int, int, int, int, int>(instance.PlacementPreviewHook_CheckIfCanPlace), 1, 0, true);
      TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int, int>(((ModTileEntity) instance).Hook_AfterPlacement), -1, 0, false);
      TileObjectData.addTile((int) ((ModBlockType) this).Type);
      TileID.Sets.InteractibleByNPCs[(int) ((ModBlockType) this).Type] = true;
      TileID.Sets.PreventsSandfall[(int) ((ModBlockType) this).Type] = true;
      ((ModTile) this).AddToArray(ref TileID.Sets.CountsAsPylon);
      ((ModTile) this).AddMapEntry(Color.White, ((ModBlockType) this).CreateMapEntryName());
    }

    public virtual NPCShop.Entry GetNPCShopEntry() => (NPCShop.Entry) null;

    public virtual bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

    public virtual bool RightClick(int i, int j)
    {
      Main.mapFullscreen = true;
      SoundEngine.PlaySound(ref SoundID.MenuOpen, new Vector2?(), (SoundUpdateCallback) null);
      return true;
    }

    public virtual void MouseOver(int i, int j)
    {
      Main.LocalPlayer.cursorItemIconEnabled = true;
      Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<SiblingPylon>();
    }

    public virtual void KillMultiTile(int i, int j, int frameX, int frameY)
    {
      ModContent.GetInstance<SiblingPylonTileEntity>().Kill(i, j);
    }

    private bool NearNPC(Vector2 tilePos, int npcType)
    {
      return ((IEnumerable<NPC>) Main.npc).Any<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.type == npcType && (double) ((Entity) n).Distance(tilePos) < 1000.0));
    }

    private bool NearEnoughSiblings(Vector2 tilePos)
    {
      int num = 0;
      if (this.NearNPC(tilePos, ModContent.NPCType<Mutant>()))
        ++num;
      if (this.NearNPC(tilePos, ModContent.NPCType<Abominationn>()))
        ++num;
      if (this.NearNPC(tilePos, ModContent.NPCType<Deviantt>()))
        ++num;
      return num >= 2;
    }

    public virtual void ValidTeleportCheck_DestinationPostCheck(
      TeleportPylonInfo destinationPylonInfo,
      ref bool destinationPylonValid,
      ref string errorKey)
    {
      if (this.NearEnoughSiblings(Utils.ToWorldCoordinates(destinationPylonInfo.PositionInTiles, 8f, 8f)))
        return;
      destinationPylonValid = false;
      errorKey = "Mods.Fargowiltas.MessageInfo.SiblingPylonNotNearSiblings";
    }

    public virtual void ValidTeleportCheck_NearbyPostCheck(
      TeleportPylonInfo nearbyPylonInfo,
      ref bool destinationPylonValid,
      ref bool anyNearbyValidPylon,
      ref string errorKey)
    {
      if (this.NearEnoughSiblings(Utils.ToWorldCoordinates(nearbyPylonInfo.PositionInTiles, 8f, 8f)))
        return;
      destinationPylonValid = false;
      errorKey = "Mods.Fargowiltas.MessageInfo.NearbySiblingPylonNotNearSiblings";
    }

    public virtual void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
      r = 0.15f;
      g = 0.75f;
      b = 0.5617647f;
    }

    public virtual bool AutoSelect(int i, int j, Item item)
    {
      return ((ModTile) this).AutoSelect(i, j, item);
    }

    public virtual void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
    {
      this.DefaultDrawPylonCrystal(spriteBatch, i, j, this.crystalTexture, this.crystalHighlightTexture, Vector2.op_Addition(Vector2.op_Multiply(Vector2.UnitX, -1f), Vector2.op_Multiply(Vector2.UnitY, -12f)), Color.op_Multiply(Color.White, 0.1f), Color.White, 4, 8);
    }

    public virtual void DrawMapIcon(
      ref MapOverlayDrawContext context,
      ref string mouseOverText,
      TeleportPylonInfo pylonInfo,
      bool isNearPylon,
      Color drawColor,
      float deselectedScale,
      float selectedScale)
    {
      this.DefaultMapClickHandle(this.DefaultDrawMapIcon(ref context, this.mapIcon, Vector2.op_Addition(Utils.ToVector2(pylonInfo.PositionInTiles), new Vector2(1.5f, 2f)), drawColor, deselectedScale, selectedScale), pylonInfo, "Mods.Fargowiltas.Items.SiblingPylon.DisplayName", ref mouseOverText);
    }
  }
}
