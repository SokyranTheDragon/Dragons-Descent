using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;
using VFECore.Abilities;
using Ability = VFECore.Abilities.Ability;
using LudeonTK;
using DD;


namespace DD
{
    public class Ability_DragonJump: Ability

{
        public override void Cast(LocalTargetInfo target)
{
            this.pawn.stances.SetStance(new DD_Stance_Stand(200, target, verb));
            base.Cast(target);
            LongEventHandler.QueueLongEvent(() =>
    {
        IntVec3 destination = target.Cell + ((this.pawn.Position - target.Cell).ToVector3().normalized * 2).ToIntVec3();
        Map map = this.pawn.Map;
        AbilityPawnFlyer flyer = (AbilityPawnFlyer)PawnFlyer.MakeFlyer(DD_ThingDefOf.DD_DragonJumpFlyer, this.pawn, destination, null, null);
        flyer.ability = this;
        flyer.DestinationCell = destination;
        GenSpawn.Spawn(flyer, target.Cell, map);
        //target.Thing.TakeDamage(new DamageInfo(DamageDefOf.Blunt, this.GetPowerForPawn(), float.MaxValue, instigator: this.pawn));
  
    }, "DragonJumpAbility", false, null);
}

public override bool ShowGizmoOnPawn() =>
   this.pawn != null && this.pawn.Faction == Faction.OfPlayer;

        
    }
}