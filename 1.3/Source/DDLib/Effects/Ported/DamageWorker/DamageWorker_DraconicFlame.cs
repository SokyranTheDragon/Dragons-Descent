﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace DD
{
   public class DamageWorker_DraconicFlame : DamageWorker_AbstractEffect
   {
       protected override ThingDef EffectDef => DD_EffectDefOf.DraconicFire;

       protected override bool HasEffect(IntVec3 cell, Map map) => cell.GetThingList(map).OfType<DraconicFire>().Any();
   }
}
