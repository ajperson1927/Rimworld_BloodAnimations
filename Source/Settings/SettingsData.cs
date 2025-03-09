using Verse;

namespace BloodAnimations
{
    internal partial class Settings : ModSettings
    {
        public static float bloodMultiplier;
        public static int bloodTime;

        public static float filthMultiplier;

        public static float bleedMultiplier;
        public static int ticksUntilBleed;

        public static bool bulletCasingToggle;
        public static bool bulletCasingFilthToggle;

        public static bool groundImpactToggle;


        public Settings()
        {
            Initialize();
        }

        private static void Initialize()
        {
            bloodMultiplier = 1f;
            bloodTime = 10;

            filthMultiplier = 0.15f;

            bleedMultiplier = 1f;
            ticksUntilBleed = 30;

            bulletCasingToggle = true;
            bulletCasingFilthToggle = true;

            groundImpactToggle = true;
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref bloodMultiplier, "bloodMultiplier", 1f);
            Scribe_Values.Look(ref bloodTime, "bloodTime", 10);

            Scribe_Values.Look(ref filthMultiplier, "filthMultiplier", 0.15f);

            Scribe_Values.Look(ref bleedMultiplier, "bleedMultiplier", 1f);
            Scribe_Values.Look(ref ticksUntilBleed, "ticksUntilBleed", 30);

            Scribe_Values.Look(ref bulletCasingToggle, "bulletCasingToggle", true);
            Scribe_Values.Look(ref bulletCasingFilthToggle, "bulletCasingFilthToggle", true);

            Scribe_Values.Look(ref groundImpactToggle, "groundImpactToggle", true);
        }
    }
}
