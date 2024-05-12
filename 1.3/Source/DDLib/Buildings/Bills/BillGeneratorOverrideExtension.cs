﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace DD
{
    public abstract class BillGeneratorOverrideExtension : DefModExtension
    {
        public abstract Bill NewBill(RecipeDef recipe);
    }
}
