using RimWorld;
using Verse;

namespace BloodAnimations
{
    [DefOf]
	public static class ParticleDefOf
	{
		public static FleckDef Fuu_BloodParticle;
		public static FleckDef Fuu_BloodDripParticle;
		public static FleckDef Fuu_LongSparkThrown;
		public static FleckDef Fuu_MachineParticle;
		public static FleckDef Fuu_RubbleBuildingParticle;
		public static FleckDef Fuu_RubbleRockParticle;
		public static FleckDef Fuu_RubbleSandbagsParticle;
        public static ThingDef Fuu_FlyingPiece;

        public static ThingDef Filth_RubbleRock;
		public static ThingDef Filth_RubbleBuilding;
		public static ThingDef SandbagRubble;
        public static ThingDef Filth_FireFoam;
        public static ThingDef SlagRubble;

        [MayRequireAnomaly]
        public static ThingDef Filth_DarkBlood;
        [MayRequireAnomaly]
        public static ThingDef Filth_Fleshmass;
        [MayRequireAnomaly]
        public static ThingDef Fuu_Filth_Blood_Fleshbeast;

        [MayRequireAnomaly]
        public static EffecterDef FleshmassDestroyed;
        [MayRequireAnomaly]
        public static EffecterDef FleshmassHeartDestroyed;

        //Alpha Genes special blood
        public static FleckDef Fuu_PoolSoftParticle;
		public static FleckDef Fuu_ChemfuelParticle;
		public static FleckDef Fuu_FireFoamParticle;

        //Bullet Casings
        public static ThingDef Fuu_BulletCasingRifle;
        public static ThingDef Fuu_BulletCasingCharge;
        public static ThingDef Fuu_BulletCasingShotgun;
        public static ThingDef Filth_BulletCasingsRifle;
        public static ThingDef Filth_BulletCasingsShotgun;
        public static ThingDef Filth_BulletCasingsCharge;



        static ParticleDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(ParticleDefOf));
		}
	}
}
