using HarmonyLib;
using UnityEngine;
using Verse;

namespace BloodAnimations
{
    //Impact blood
    [HarmonyPatch(typeof(DamageWorker_AddInjury), "ApplyToPawn")]
    internal static class DamageWorker_AddInjury_ApplyToPawn
    {
        [HarmonyPostfix]
        private static void ApplyToPawn(DamageInfo dinfo, Pawn pawn, ref DamageWorker.DamageResult __result)
        {
            //Log.Message($"pawn:{pawn}, anyParentSpawned:{pawn.SpawnedOrAnyParentSpawned}, damageInfo:{dinfo}, damageResult:{__result}, mapHeld:{pawn.MapHeld}. drawPos:{pawn.DrawPos}");
            if (pawn != null && pawn.SpawnedOrAnyParentSpawned && pawn.MapHeld != null && pawn.DrawPos != null && __result != null && __result.wounded && Settings.bloodMultiplier > 0f)
            {
                float totalAmountDealt = __result.totalDamageDealt;
                if (__result.headshot)
                    totalAmountDealt *= 2f;

                if (totalAmountDealt > 100f)
                    totalAmountDealt = 100f;

                if (totalAmountDealt <= FleckMaker.DamagePerBloodParticle)
                    return;

                //Log.Message($"pawn:{pawn} totalAmountDealt:{totalAmountDealt} loopTimes:{totalAmountDealt / DamagePerBloodParticle} wounded:{wounded} angle:{angle}");

                if (pawn.def?.race != null)
                {
                    ThingDef bloodDef = BloodDefUtility.GetBloodDef(pawn);
                    //Log.Message($"bloodDef:{bloodDef}, fleckDef:{fleckDef}, bloodColor{bloodColor}, pawn:{pawn}");
                    totalAmountDealt *= Settings.bloodMultiplier;

                    if (pawn.def.race.IsFlesh && bloodDef != null) //Blood for biological pawns
                    {
                        Color bloodColor = BloodDefUtility.GetBloodColor(bloodDef);
                        FleckDef fleckDef = BloodDefUtility.GetFleckDef(bloodDef);
                        for (int i = 0; i < totalAmountDealt / FleckMaker.DamagePerBloodParticle; i++)
                        {
                            FleckMaker.CreateFleckBloodDirectional(pawn, fleckDef, bloodColor, dinfo.Angle, totalAmountDealt);
                            if (i % 2 == 0)
                            {
                                FleckMaker.CreateFleckBlood140(pawn, fleckDef, bloodColor, dinfo.Angle, totalAmountDealt);
                            }
                        }
                    }
                    else if (!pawn.def.race.IsFlesh) //Machine parts for non-biological pawns
                    {
                        Color flyingPieceColor = BloodDefUtility.GetFlyingPieceColor(bloodDef);
                        for (int i = 0; i < totalAmountDealt / FleckMaker.DamagePerBloodParticle / 2f; i++)
                        {
                            if (i % 4 == 0)
                                FleckMaker.CreateFleckHeavy360(pawn, ParticleDefOf.Fuu_MachineParticle, totalAmountDealt);
                            FleckMaker.CreateMoteImpactFlyingPiece(pawn, ParticleDefOf.Fuu_FlyingPiece, flyingPieceColor);
                        }
                    }

                    if (dinfo.Def != null && dinfo.Def.isRanged && !dinfo.Def.isExplosive) //Ricochets
                    {
                        if (__result.deflected || __result.diminished)
                        {
                            float ricochetMagnitude = totalAmountDealt / FleckMaker.DamageForMaxTravelDistance;
                            FleckMaker.CreateFleckRicochet(pawn, ricochetMagnitude);
                        }
                        else if (!pawn.def.race.IsFlesh)
                        {
                            float ricochetMagnitude = totalAmountDealt / FleckMaker.DamageForMaxTravelDistance;
                            if (Rand.Chance(ricochetMagnitude))
                                FleckMaker.CreateFleckRicochet(pawn, ricochetMagnitude);
                        }
                    }
                }
            }
        }
    }
}