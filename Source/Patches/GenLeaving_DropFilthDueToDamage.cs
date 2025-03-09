using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace BloodAnimations
{
    //Structure particles
    [HarmonyPatch(typeof(GenLeaving), "DropFilthDueToDamage")]
    internal static class GenLeaving_DropFilthDueToDamage
    {
        [HarmonyPostfix]
        private static void DropFilthDueToDamage(Thing t, float damageDealt)
        {
            if (t?.def != null && t.Spawned && t.MapHeld != null && t.def.useHitPoints && t.def.filthLeaving != null && damageDealt > FleckMaker.DamagePerBloodParticle)
            {
                if (damageDealt > 50f)
                    damageDealt = 50f;

                damageDealt *= Settings.bloodMultiplier;

                //Log.Message($"throw structure particles:{target}");
                FleckDef fleckDef = ParticleDefOf.Fuu_RubbleBuildingParticle;
                if (t.def.filthLeaving == ParticleDefOf.SandbagRubble)
                    fleckDef = ParticleDefOf.Fuu_RubbleSandbagsParticle;
                else if (t.def.filthLeaving == ParticleDefOf.Filth_RubbleRock)
                    fleckDef = ParticleDefOf.Fuu_RubbleRockParticle;
                else if (t.def.filthLeaving == ParticleDefOf.Filth_Fleshmass)
                    fleckDef = ParticleDefOf.Fuu_BloodParticle;

                fleckDef.solidTime = Settings.bloodTime;
                Color flyingPieceColor = BloodDefUtility.GetFlyingPieceColor(t.def.filthLeaving);

                if (fleckDef != ParticleDefOf.Fuu_BloodParticle) //Buildings
                {
                    for (int i = 0; i < damageDealt / FleckMaker.DamagePerBloodParticle / 2f; i++)
                    {
                        if (i % 4 == 0)
                            FleckMaker.CreateFleckHeavy360(t, fleckDef, damageDealt);
                        FleckMaker.CreateMoteImpactFlyingPiece(t, ParticleDefOf.Fuu_FlyingPiece, flyingPieceColor);
                    }
                }
                else //Fleshmass
                {
                    for (int i = 0; i < damageDealt / FleckMaker.DamagePerBloodParticle / 2f; i++)
                    {
                        if (i % 4 == 0)
                            FleckMaker.CreateFleckHeavy360(t, fleckDef, damageDealt, true);
                        FleckMaker.CreateMoteImpactFlyingPiece(t, ParticleDefOf.Fuu_FlyingPiece, flyingPieceColor);
                    }
                }
            }
        }
    }
}