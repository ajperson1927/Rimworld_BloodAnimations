using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace BloodAnimations
{
    //Bleed particles
    [HarmonyPatch(typeof(Pawn_HealthTracker), "HealthTick")]
    internal static class Pawn_HealthTracker_HealthTick
    {
        [HarmonyPostfix]
        private static void HealthTick(ref Pawn_HealthTracker __instance, ref Pawn ___pawn)
        {
            if (___pawn.IsHashIntervalTick(Settings.ticksUntilBleed) && ___pawn.MapHeld != null && ___pawn.RaceProps.IsFlesh && !__instance.Dead && __instance.capacities.GetLevel(PawnCapacityDefOf.Consciousness) > 0f)
            {
                //Log.Message($"Pawn:{___pawn} SpawnedOrAnyParentSpawned:{___pawn.SpawnedOrAnyParentSpawned} Spawned:{___pawn.Spawned} parentHolder:{___pawn.ParentHolder}");
                if (___pawn.Spawned && Rand.Value < __instance.hediffSet.BleedRateTotal / 4f) //Max drip at 400% pawn bleed rate
                {
                    ThingDef bloodDef = BloodDefUtility.GetBloodDef(___pawn);
                    if (bloodDef != null)
                    {
                        Color bloodColor = BloodDefUtility.GetBloodColor(bloodDef);
                        Color bleedColor = BloodDefUtility.GetBleedColor(bloodDef, bloodColor);
                        FleckDef fleckDef = BloodDefUtility.GetFleckDef(bloodDef);

                        if (___pawn.GetPosture() == PawnPosture.Standing)
                        {
                            FleckMaker.CreateFleckBleed(___pawn, bleedColor, false, false);
                            FleckMaker.CreateFleckBleedPuddle(___pawn, fleckDef, bloodColor, false, false);
                        }
                        else
                        {
                            FleckMaker.CreateFleckBleed(___pawn, bleedColor, true, false);
                            FleckMaker.CreateFleckBleedPuddle(___pawn, fleckDef, bloodColor, true, false);
                        }
                    }
                }
                else if (___pawn.SpawnedOrAnyParentSpawned && ___pawn.ParentHolder is Pawn_CarryTracker) //Downed pawn is being carried by someone else
                {
                    Pawn carrier = ((Pawn_CarryTracker)___pawn.ParentHolder).pawn;
                    //Log.Message($"pawn:{___pawn} carrier:{carrier}");

                    ThingDef bloodDef = BloodDefUtility.GetBloodDef(___pawn);
                    if (bloodDef != null)
                    {
                        Color bloodColor = BloodDefUtility.GetBloodColor(bloodDef);
                        Color bleedColor = BloodDefUtility.GetBleedColor(bloodDef, bloodColor);
                        FleckDef fleckDef = BloodDefUtility.GetFleckDef(bloodDef);

                        if (Rand.Value < ___pawn.health.hediffSet.BleedRateTotal / 4f)
                        {
                            FleckMaker.CreateFleckBleed(carrier, bleedColor, false, true);
                            FleckMaker.CreateFleckBleedPuddle(carrier, fleckDef, bloodColor, false, true);
                            //Log.Message($"carriedPawn:{carriedPawn} pos:{carriedPawn.DrawPos}");
                        }
                    }
                }
            }
        }
    }
}
