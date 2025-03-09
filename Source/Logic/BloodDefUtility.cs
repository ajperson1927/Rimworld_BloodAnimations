using RimWorld;
using UnityEngine;
using Verse;

namespace BloodAnimations
{
    public static class BloodDefUtility
    {
        public static ThingDef GetBloodDef(Pawn pawn)
        {
            if (ModLister.BiotechInstalled && pawn?.genes != null && ModSupport.VanillaExpandedFrameworkActive)
            {
                ThingDef bloodDef = ModSupport.GetVanillaGenesExpandedBloodDef(pawn);
                if (bloodDef != null)
                    return bloodDef;
            }

            //Log.Message($"raceprops={pawn.RaceProps}, bloodDef={(pawn.IsMutant ? (pawn.mutant.Def.bloodDef ?? pawn.RaceProps.BloodDef) : pawn.RaceProps.BloodDef)}");
            if (pawn.Dead && pawn.IsShambler)
                return MutantDefOf.Shambler.bloodDef; //For Shamblers, Pawn_MutantTracker is removed on death, thus the mutant bloodDef can't be accessed anymore.

            return (pawn.IsMutant ? (pawn.mutant.Def.bloodDef ?? pawn.RaceProps.BloodDef) : pawn.RaceProps.BloodDef);
        }

        public static Color GetBloodColor(ThingDef bloodDef)
        {
            Color bloodColor = ThingDefOf.Filth_Blood.graphicData.color;
            if (bloodDef?.graphicData?.color != null)
            {
                bloodColor = bloodDef.graphicData.color;
            }
            if (bloodDef != null && ModSupport.TheProfanedActive)
            {
                if (bloodDef == ModSupport.BotchJob_Filth_Ectoplasm)
                    bloodColor = new Color32(25, 255, 255, 85);
                else if (bloodDef == ModSupport.BotchJob_Filth_Undead)
                    bloodColor = new Color32(28, 0, 0, 180);
            }
            return bloodColor;
        }

        public static Color GetBleedColor(ThingDef bloodDef, Color bloodColor)
        {
            if (ModSupport.AlphaGenesActive)
            {
                if (bloodDef == ModSupport.AG_Filth_RubbleRock)
                    bloodColor = new Color32(134, 120, 113, 180);
                else if (bloodDef == ModSupport.AG_Filth_Fuel)
                    bloodColor = new Color32(129, 97, 60, 180);
            }
            return bloodColor;
        }

        public static FleckDef GetFleckDef(ThingDef bloodDef)
        {
            FleckDef fleckDef = ParticleDefOf.Fuu_BloodParticle;
            if (bloodDef != null && ModSupport.AlphaGenesActive)
            {
                if (bloodDef == ModSupport.AG_FilthMucus || bloodDef == ModSupport.AG_TarBlood)
                    fleckDef = ParticleDefOf.Fuu_PoolSoftParticle;
                else if (bloodDef == ModSupport.AG_Filth_Fuel)
                    fleckDef = ParticleDefOf.Fuu_ChemfuelParticle;
                else if (bloodDef == ModSupport.AG_Filth_RubbleRock)
                    fleckDef = ParticleDefOf.Fuu_RubbleRockParticle;
                else if (bloodDef == ParticleDefOf.Filth_FireFoam)
                    fleckDef = ParticleDefOf.Fuu_FireFoamParticle;
            }
            fleckDef.solidTime = Settings.bloodTime;
            return fleckDef;
        }

        public static Color GetFlyingPieceColor(ThingDef bloodDef)
        {
            Color color = new Color32(69, 57, 50, 120);
            if (bloodDef != null)
            {
                if (bloodDef == ThingDefOf.Filth_MachineBits)
                    color = new Color32(77, 77, 77, 125);
                else if (bloodDef == ThingDefOf.Filth_RubbleBuilding)
                    color = new Color32(69, 57, 50, 125);
                else if (bloodDef == ParticleDefOf.SandbagRubble)
                    color = new Color32(177, 152, 104, 125);
                else if (bloodDef == ParticleDefOf.SlagRubble || bloodDef == ThingDefOf.Filth_RubbleRock)
                    color = new Color32(138, 119, 112, 125);
                else if (bloodDef == ParticleDefOf.Filth_Fleshmass)
                    color = new Color32(189, 140, 140, 255);
            }
            return color;
        }
    }
}
