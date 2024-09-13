// Decompiled with JetBrains decompiler
// Type: Fargowiltas.NPCs.SuperDummy
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.NPCs
{
  public class SuperDummy : ModNPC
  {
    public virtual void SetStaticDefaults()
    {
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers1;
      // ISSUE: explicit constructor call
      ((NPCID.Sets.NPCBestiaryDrawModifiers) ref bestiaryDrawModifiers1).\u002Ector();
      bestiaryDrawModifiers1.Hide = true;
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers2 = bestiaryDrawModifiers1;
      NPCID.Sets.NPCBestiaryDrawOffset.Add(this.Type, bestiaryDrawModifiers2);
    }

    public virtual void SetDefaults()
    {
      this.NPC.CloneDefaults(488);
      this.NPC.lifeMax = 1000000;
      this.NPC.aiStyle = -1;
      ((Entity) this.NPC).width = 28;
      ((Entity) this.NPC).height = 50;
      this.NPC.immortal = false;
      this.NPC.npcSlots = 0.0f;
      this.NPC.dontCountMe = true;
    }

    public virtual bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
    {
      return new bool?(false);
    }

    public virtual void OnSpawn(IEntitySource source) => this.NPC.life = this.NPC.lifeMax = 1000000;

    public virtual void AI()
    {
      this.NPC.life = this.NPC.lifeMax = 1000000;
      if (!FargoGlobalNPC.AnyBossAlive())
        return;
      this.NPC.life = 0;
      this.NPC.HitEffect(0, 10.0, new bool?());
      this.NPC.SimpleStrikeNPC(int.MaxValue, 0, false, 0.0f, (DamageClass) null, false, 0.0f, true);
    }

    public virtual bool CheckDead() => false;
  }
}
