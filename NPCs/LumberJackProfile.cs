﻿// Decompiled with JetBrains decompiler
// Type: Fargowiltas.NPCs.LumberJackProfile
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.NPCs
{
  public class LumberJackProfile : ITownNPCProfile
  {
    public int RollVariation() => 0;

    public string GetNameForVariant(NPC npc) => npc.getNewNPCName();

    public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
    {
      if (npc.IsABestiaryIconDummy && !npc.ForcePartyHatOn)
        return ModContent.Request<Texture2D>("Fargowiltas/NPCs/LumberJack", (AssetRequestMode) 2);
      if (npc.IsABestiaryIconDummy && npc.ForcePartyHatOn)
        return ModContent.Request<Texture2D>("Fargowiltas/NPCs/LumberJack_Party", (AssetRequestMode) 2);
      return npc.IsShimmerVariant ? (npc.altTexture == 1 ? ModContent.Request<Texture2D>("Fargowiltas/NPCs/Lumberjack_Shimmer_Party", (AssetRequestMode) 2) : ModContent.Request<Texture2D>("Fargowiltas/NPCs/Lumberjack_Shimmer", (AssetRequestMode) 2)) : (npc.altTexture == 1 ? ModContent.Request<Texture2D>("Fargowiltas/NPCs/LumberJack_Party", (AssetRequestMode) 2) : ModContent.Request<Texture2D>("Fargowiltas/NPCs/LumberJack", (AssetRequestMode) 2));
    }

    public int GetHeadTextureIndex(NPC npc)
    {
      return ModContent.GetModHeadSlot("Fargowiltas/NPCs/LumberJack_Head");
    }
  }
}