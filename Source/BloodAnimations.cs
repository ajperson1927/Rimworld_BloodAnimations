using HarmonyLib;
using UnityEngine;
using Verse;

namespace BloodAnimations
{
    public class BloodAnimations : Mod
    {
        public BloodAnimations(ModContentPack content) : base(content)
        {
            Harmony harmony = new Harmony(content.PackageId);
            harmony.PatchAll();
            GetSettings<Settings>();
        }
        public override string SettingsCategory()
        {
            return "BloodAnimations_Title".Translate();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Settings.DoWindowContents(inRect);
        }
    }
}
