using System;
using UnityEngine;
using Verse;

namespace BloodAnimations
{
    internal partial class Settings
    {
        //private static Vector2 scrollPosition;
        public static void DoWindowContents(Rect inRect)
        {
            //30f for top page description and bottom close button
            Rect viewRect = new Rect(0f, 30f, inRect.width, inRect.height - 30f);
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.ColumnWidth = viewRect.width;
            listingStandard.Begin(viewRect);
            listingStandard.Gap(5f);

            listingStandard.Label("BloodAnimations_BloodMultiplier".Translate() + ":\n" + MakeHumanReadable(bloodMultiplier) + "%");
            bloodMultiplier = listingStandard.Slider(bloodMultiplier, 0f, 5f);

            listingStandard.Gap(15f);
            listingStandard.Label("BloodAnimations_BleedRate".Translate() + ":\n" + MakeHumanReadable(bleedMultiplier) + "%");
            bleedMultiplier = listingStandard.Slider(bleedMultiplier, 0.0001f, 3f);
            ticksUntilBleed = (int)(30f / bleedMultiplier);

            listingStandard.Gap(15f);
            listingStandard.Label("BloodAnimations_BloodTime".Translate() + ":\n" + (int)bloodTime + " seconds");
            bloodTime = (int)listingStandard.Slider(bloodTime, 0, 300);

            listingStandard.Gap(15f);
            listingStandard.Label("BloodAnimations_FilthMultiplier".Translate() + ":\n" + MakeHumanReadable(filthMultiplier) + "%");
            filthMultiplier = listingStandard.Slider(filthMultiplier, 0f, 1f);

            listingStandard.Gap(30f);
            listingStandard.CheckboxLabeled("BloodAnimations_BulletCasingToggle".Translate() + ":", ref bulletCasingToggle);
            if (bulletCasingToggle == true)
            {
                listingStandard.Gap(5f);
                listingStandard.CheckboxLabeled("   " + "BloodAnimations_BulletCasingFilthToggle".Translate() + ":", ref bulletCasingFilthToggle);
            }

            listingStandard.Gap(15f);
            listingStandard.CheckboxLabeled("BloodAnimations_GroundImpactToggle".Translate() + ":", ref groundImpactToggle);

            listingStandard.Gap(40f);
            listingStandard.Label("BloodAnimations_Warning".Translate());

            listingStandard.Gap(5f);
            if (listingStandard.ButtonText("BloodAnimations_ResetAll".Translate()))
            {
                Initialize();
            }
            listingStandard.End();
        }

        public static float MakeHumanReadable(float drynessFloat)
        {
            return (float)Math.Round(drynessFloat * 100f, MidpointRounding.AwayFromZero);
        }
    }
}
