using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace BloodAnimations
{
    [HarmonyPatch(typeof(Bullet), "Impact")]
    internal static class Bullet_Impact
    {
        public static Color dirtColor = new Color32(142, 114, 63, 180);
        public static Color waterColor = new Color(0.53f, 0.69f, 0.74f, 0.5f);

        [HarmonyPostfix]
        private static void Impact(ref Bullet __instance, Thing hitThing, bool blockedByShield = false)
        {
            if (Settings.groundImpactToggle && __instance.Launcher?.Map != null && hitThing == null && !blockedByShield && __instance.Position.InBounds(__instance.Launcher.Map))
            {
                if (__instance.Position.GetTerrain(__instance.Launcher.Map).takeSplashes)
                {
                    //FleckMaker.WaterSplash(ExactPosition, map, Mathf.Sqrt(DamageAmount) * 1f, 4f);
                    for (int i = 0; i < __instance.DamageAmount / FleckMaker.DamagePerBloodParticle; i++)
                    {
                        FleckMaker.CreateMoteGroundImpactFlyingPiece(__instance.Launcher.Map, __instance.ExactPosition, ParticleDefOf.Fuu_FlyingPiece, waterColor);
                    }
                }
                else
                {
                    //FleckMaker.Static(ExactPosition, map, FleckDefOf.ShotHit_Dirt);
                    for (int i = 0; i < __instance.DamageAmount / FleckMaker.DamagePerBloodParticle; i++)
                    {
                        FleckMaker.CreateMoteGroundImpactFlyingPiece(__instance.Launcher.Map, __instance.ExactPosition, ParticleDefOf.Fuu_FlyingPiece, dirtColor);
                    }
                }
            }
        }
    }
}
