﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace DD;
    public class DD_DamageWorker_Flame : DamageWorker_AddInjury
    {
    public override DamageResult Apply(DamageInfo dinfo, Thing victim)
    {
        Pawn pawn = victim as Pawn;
        if (pawn != null && pawn.Faction == Faction.OfPlayer)
        {
            Find.TickManager.slower.SignalForceNormalSpeedShort();
        }
        Map map = victim.Map;
        DamageResult damageResult = base.Apply(dinfo, victim);
        if (map == null)
        {
            return damageResult;
        }
        if (!damageResult.deflected && !dinfo.InstantPermanentInjury && Rand.Chance(FireUtility.ChanceToAttachFireFromEvent(victim)))
        {
            if (pawn != null)
            {
                pawn.TryAttachFire(Rand.Range(0.15f, 0.25f), dinfo.Instigator);
            }
        }
        if (victim.Destroyed && pawn == null)
        {
            foreach (IntVec3 item in victim.OccupiedRect())
            {
                FilthMaker.TryMakeFilth(item, map, ThingDefOf.Filth_Ash);
            }
        }
        return damageResult;
    }

    //public override void ExplosionAffectCell(Explosion explosion, IntVec3 c, List<Thing> damagedThings, List<Thing> ignoredThings, bool canThrowMotes)
    //{
    //    base.ExplosionAffectCell(explosion, c, damagedThings, ignoredThings, canThrowMotes);
    //    if (def == DD_DamageDefOf.DraconicFlame && Rand.Chance(DD_FireUtility.ChanceToStartFireIn(c, explosion.Map)))
    //    {
    //        DD_FireUtility.TryStartFireIn(c, explosion.Map, Rand.Range(0.2f, 0.6f), explosion.instigator);
    //    }
    //}
    public override void ExplosionAffectCell(Explosion explosion, IntVec3 c, List<Thing> damagedThings, List<Thing> ignoredThings, bool canThrowMotes)
    {
        base.ExplosionAffectCell(explosion, c, damagedThings, ignoredThings, canThrowMotes);
        if (def == DamageDefOf.Flame && Rand.Chance(FireUtility.ChanceToStartFireIn(c, explosion.Map)))
        {
            FireUtility.TryStartFireIn(c, explosion.Map, Rand.Range(0.2f, 0.6f), explosion.instigator);
        }
    }

}
