using HarmonyLib;
using UnityEngine;
using Verse;

namespace BloodAnimations
{
    //Death blood
    [HarmonyPatch(typeof(Pawn), "Kill")]
    internal static class Pawn_Kill
    {
        private const float corpseBloodSprayChance = 0.66f;

        [HarmonyPrefix]
        private static void Kill(ref Pawn __instance, DamageInfo? dinfo, Hediff exactCulprit = null)
        {
            if (__instance.Spawned && __instance.MapHeld != null && Settings.bloodMultiplier > 0f)
            {
                Pawn pawn = __instance;
                if (pawn?.RaceProps != null && pawn.RaceProps.IsFlesh && Rand.Chance(corpseBloodSprayChance))
                {
                    //Log.Message($"KILL for pawn={pawn}, isMutant={pawn.IsMutant}, isShambler={pawn.IsShambler}");
                    ThingDef bloodDef = BloodDefUtility.GetBloodDef(pawn);
                    if (bloodDef != null)
                    {
                        FleckDef fleckDef = BloodDefUtility.GetFleckDef(bloodDef);
                        Color bloodColor = BloodDefUtility.GetBloodColor(bloodDef);
                        float distance = 0f;
                        float angle = 0f;
                        //Log.Message($"Corpse:{__instance.InnerPawn} SpawnedOrAnyParentSpawned:{__instance.SpawnedOrAnyParentSpawned} Spawned:{__instance.Spawned} parentHolder:{__instance.ParentHolder} angle:{pawn.Drawer.renderer.BodyAngle()} facing:{pawn.Drawer.renderer.LayingFacing()}");

                        for (int i = 0; i < 10 * Settings.bloodMultiplier; i++)
                        {
                            distance += 0.02f / Settings.bloodMultiplier;
                            if (pawn.Drawer.renderer.LayingFacing() == Rot4.East)
                                angle += 12f / Settings.bloodMultiplier;
                            else if (pawn.Drawer.renderer.LayingFacing() == Rot4.West)
                                angle -= 12f / Settings.bloodMultiplier;
                            else if (pawn.Drawer.renderer.LayingFacing() == Rot4.South)
                            {
                                if ((int)pawn.Drawer.renderer.BodyAngle(PawnRenderFlags.None) % 2 == 0)
                                    angle += 4f / Settings.bloodMultiplier;
                                else
                                    angle -= 4f / Settings.bloodMultiplier;
                            }
                            FleckMaker.CreateFleckDeathBlood(__instance, fleckDef, bloodColor, pawn.Drawer.renderer.BodyAngle(PawnRenderFlags.None) + angle, distance);
                        }
                    }
                }
            }
        }
    }
}