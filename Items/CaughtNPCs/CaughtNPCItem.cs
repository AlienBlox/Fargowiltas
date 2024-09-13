// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.CaughtNPCs.CaughtNPCItem
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.NPCs;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.CaughtNPCs
{
  public class CaughtNPCItem : ModItem
  {
    internal static Dictionary<int, int> CaughtTownies = new Dictionary<int, int>();
    public string _name;
    public int AssociatedNpcId;
    public string NpcQuote;

    public virtual string Name => this._name;

    public CaughtNPCItem()
    {
      this._name = ((ModType) this).Name;
      this.AssociatedNpcId = 0;
      this.NpcQuote = "";
    }

    public CaughtNPCItem(string internalName, int associatedNpcId, string npcQuote = "")
    {
      this._name = internalName;
      this.AssociatedNpcId = associatedNpcId;
      this.NpcQuote = npcQuote;
    }

    public virtual bool IsLoadingEnabled(Mod mod) => this.AssociatedNpcId != 0;

    protected virtual bool CloneNewInstances => true;

    public virtual ModItem Clone(Item item)
    {
      CaughtNPCItem caughtNpcItem = ((ModType<Item, ModItem>) this).Clone(item) as CaughtNPCItem;
      caughtNpcItem._name = this._name;
      caughtNpcItem.AssociatedNpcId = this.AssociatedNpcId;
      caughtNpcItem.NpcQuote = this.NpcQuote;
      return (ModItem) caughtNpcItem;
    }

    public virtual bool IsCloneable => true;

    public virtual void Unload() => CaughtNPCItem.CaughtTownies.Clear();

    public virtual string Texture
    {
      get
      {
        if (this.AssociatedNpcId >= (int) NPCID.Count)
          return NPCLoader.GetNPC(this.AssociatedNpcId).Texture;
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(20, 1);
        interpolatedStringHandler.AppendLiteral("Terraria/Images/NPC_");
        interpolatedStringHandler.AppendFormatted<int>(this.AssociatedNpcId);
        return interpolatedStringHandler.ToStringAndClear();
      }
    }

    public virtual void SetStaticDefaults()
    {
      Main.RegisterItemAnimation(this.Type, (DrawAnimation) new DrawAnimationVertical(6, Main.npcFrameCount[this.AssociatedNpcId], false));
      ItemID.Sets.AnimatesAsSoul[this.Type] = true;
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 5;
    }

    public virtual void SetDefaults()
    {
      this.Item.DefaultToCapturedCritter(this.AssociatedNpcId);
      this.Item.rare = 1;
      this.Item.UseSound = new SoundStyle?(SoundID.Item44);
      if (this.AssociatedNpcId != 369)
        return;
      this.Item.bait = 15;
    }

    public virtual void PostUpdate()
    {
      if (this.AssociatedNpcId != 22 || !((Entity) this.Item).lavaWet || NPC.AnyNPCs(113))
        return;
      NPC.SpawnWOF(((Entity) this.Item).position);
      this.Item.TurnToAir(false);
    }

    public virtual bool CanUseItem(Player player)
    {
      return (double) ((Entity) player).Distance(Main.MouseWorld) < 64.0 && !Collision.SolidCollision(Vector2.op_Subtraction(Main.MouseWorld, Vector2.op_Division(player.DefaultSize, 2f)), (int) player.DefaultSize.X, (int) player.DefaultSize.Y) && NPC.CountNPCS(this.AssociatedNpcId) < 5;
    }

    public virtual bool? UseItem(Player player) => new bool?(true);

    public static void RegisterItems()
    {
      CaughtNPCItem.CaughtTownies = new Dictionary<int, int>();
      CaughtNPCItem.Add("Abominationn", ModContent.NPCType<Abominationn>(), "'I sure wish I was a boss.'");
      CaughtNPCItem.Add("Angler", 369, "'You'd be a great helper minion!'");
      CaughtNPCItem.Add("ArmsDealer", 19, "'Keep your hands off my gun, buddy!'");
      CaughtNPCItem.Add("Clothier", 54, "'Thanks again for freeing me from my curse.'");
      CaughtNPCItem.Add("Cyborg", 209, "'My expedition efficiency was critically reduced when a projectile impacted my locomotive actuator.'");
      CaughtNPCItem.Add("Demolitionist", 38, "'It's a good day to die!'");
      CaughtNPCItem.Add("Deviantt", ModContent.NPCType<Deviantt>(), "'Embrace suffering... and while you're at it, embrace another purchase!'");
      CaughtNPCItem.Add("Dryad", 20, "'Be safe; Terraria needs you!'");
      CaughtNPCItem.Add("DyeTrader", 207, "'My dear, what you're wearing is much too drab.'");
      CaughtNPCItem.Add("GoblinTinkerer", 107, "'Looking for a gadgets expert? I'm your goblin!'");
      CaughtNPCItem.Add("Golfer", 588, "'An early bird catches the worm, but an early hole catches the birdie.'");
      CaughtNPCItem.Add("Guide", 22, "'They say there is a person who will tell you how to survive in this land.'");
      CaughtNPCItem.Add("LumberJack", ModContent.NPCType<LumberJack>(), "'I eat a bowl of woodchips for breakfast... without any milk.'");
      CaughtNPCItem.Add("Mechanic", 124, "'Always buy more wire than you need!'");
      CaughtNPCItem.Add("Merchant", 17, "'Did you say gold? I'll take that off of ya.'");
      CaughtNPCItem.Add("Mutant", ModContent.NPCType<Mutant>(), "'You're lucky I'm on your side.'");
      CaughtNPCItem.Add("Nurse", 18, "'Show me where it hurts.'");
      CaughtNPCItem.Add("Painter", 227, "'I know the difference between turquoise and blue-green. But I won't tell you.'");
      CaughtNPCItem.Add("PartyGirl", 208, "'We have to talk. It's... it's about parties.'");
      CaughtNPCItem.Add("Pirate", 229, "'Stay off me booty, ya scallywag!'");
      CaughtNPCItem.Add("SantaClaus", 142, "'What? You thought I wasn't real?'");
      CaughtNPCItem.Add("SkeletonMerchant", 453, "'You would not believe some of the things people throw at me... Wanna buy some of it?'");
      CaughtNPCItem.Add("Squirrel", ModContent.NPCType<Squirrel>(), "*squeak*");
      CaughtNPCItem.Add("Steampunker", 178, "'Show me some gears!'");
      CaughtNPCItem.Add("Stylist", 353, "'Did you even try to brush your hair today?'");
      CaughtNPCItem.Add("Tavernkeep", 550, "'What am I doing here...'");
      CaughtNPCItem.Add("TaxCollector", 441, "'You again? Suppose you want more money!?'");
      CaughtNPCItem.Add("TravellingMerchant", 368, "'I sell wares from places that might not even exist!'");
      CaughtNPCItem.Add("Truffle", 160, "'Everyone in this town feels a bit off.'");
      CaughtNPCItem.Add("WitchDoctor", 228, "'Which doctor am I? The Witch Doctor am I.'");
      CaughtNPCItem.Add("Wizard", 108, "'Want me to pull a coin from behind your ear? No? Ok.'");
      CaughtNPCItem.Add("Zoologist", 633, "'I love animals, like, a lot!'");
      CaughtNPCItem.Add("Princess", 663, "'Pink is the best color anyone could ask for!'");
      CaughtNPCItem.Add("TownDog", 638, "'Woof!'");
      CaughtNPCItem.Add("TownCat", 637, "'Meow!'");
      CaughtNPCItem.Add("TownBunny", 656, "'*Bunny noises*'");
    }

    public static void Add(string internalName, int id, string quote)
    {
      CaughtNPCItem caughtNpcItem = new CaughtNPCItem(internalName, id, quote);
      Fargowiltas.Fargowiltas.Instance.AddContent((ILoadable) caughtNpcItem);
      CaughtNPCItem.CaughtTownies.Add(id, caughtNpcItem.Type);
    }
  }
}
