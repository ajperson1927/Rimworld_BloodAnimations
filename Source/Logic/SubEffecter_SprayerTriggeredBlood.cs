using RimWorld;
using UnityEngine;
using Verse;

namespace BloodAnimations
{
    public class SubEffecter_SprayerTriggeredBlood : SubEffecter_Sprayer
    {
        public const float defaultBurstCount = 7f; //Needs to be hardcoded to make this scalable with bloodMultiplier
        public SubEffecter_SprayerTriggeredBlood(SubEffecterDef def, Effecter parent)
    : base(def, parent)
        {
        }
        public override void SubTrigger(TargetInfo A, TargetInfo B, int overrideSpawnTick = -1, bool force = false)
        {
            if (def?.fleckDef != null && parent?.def != null)
            {
                //Log.Message($"parentDef:{parent.def}, def:{def}, fleckDef:{def.fleckDef}, solidTime:{def.fleckDef.solidTime}, burstCountMin:{Mathf.RoundToInt(def.burstCount.min)}, burstCountMax:{Mathf.RoundToInt(def.burstCount.max)}");
                if (parent.def == EffecterDefOf.MeatExplosion || parent.def == EffecterDefOf.MeatExplosionExtraLarge || parent.def == EffecterDefOf.MeatExplosionLarge || parent.def == EffecterDefOf.MeatExplosionSmall)
                {
                    def.burstCount.min = Mathf.RoundToInt(defaultBurstCount * Settings.bloodMultiplier);
                    def.burstCount.max = def.burstCount.min;

                    if (Settings.filthMultiplier > 0f && A != null && A.IsValid && A.Cell != null && A.Map != null)
                    {
                        IntVec3 worldPos = A.Cell;
                        if (worldPos.InBounds(A.Map))
                        {
                            if (FilthMaker.TerrainAcceptsFilth(worldPos.GetTerrain(A.Map), ThingDefOf.Filth_Blood, FilthSourceFlags.None))
                            {
                                FilthMaker.TryMakeFilth(worldPos, A.Map, ParticleDefOf.Fuu_Filth_Blood_Fleshbeast, Mathf.RoundToInt(def.burstCount.max * Settings.filthMultiplier), FilthSourceFlags.None);
                            }
                        }
                    }
                }
                else if (parent.def == ParticleDefOf.FleshmassDestroyed || parent.def == ParticleDefOf.FleshmassHeartDestroyed)
                {
                    def.burstCount.min = Mathf.RoundToInt(1f * Settings.bloodMultiplier);
                    def.burstCount.max = def.burstCount.min;
                }
                def.fleckDef.solidTime = Settings.bloodTime;
            }
            MakeMote(A, B, overrideSpawnTick);
        }
    }
}