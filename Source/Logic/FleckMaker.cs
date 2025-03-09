using UnityEngine;
using Verse;
using System;

namespace BloodAnimations
{
    public static class FleckMaker
    {
        public const float DamagePerBloodParticle = 5f;
        public const float DamageForMaxTravelDistance = 30f;

        //Bleed dripping
        public const float InitialHeightOffset = 0.15f;
        public const float DripVelocity = 4f;
        public const float DripVelocityLayingDown = 2.5f;
        public const float DripAirtime = 0.15f;
        public const float DripAirtimeLayingDown = 0.075f;

        //Fleck values
        public static Func<float, float> Angle360 = randValue => Range(randValue, -180f, 180f);
        public static Func<float, float> Angle140 = randValue => Range(randValue, -70f, 70f);
        public static Func<float, float> AngleDirectional = randValue => Range(randValue, -5f, 5f);
        public static Func<float, float> Scale = randValue => Range(randValue, 0.6f, 0.8f);
        public static Func<float, float> AirTimeDirectional = randValue => Range(randValue, 0f, 0.3f);
        public static Func<float, float> AirTime360Blood = randValue => Range(randValue, 0f, 0.15f);
        public static Func<float, float> AirTime360Parts = randValue => Range(randValue, 0f, 0.2f);
        public static Func<float, float> SolidTime = randValue => Range(randValue, 1f, Settings.bloodTime);
        public static Func<float, float> VelocityImpactFlyingPieces = randValue => Range(randValue, 2f, 5f);
        public static Func<float, float> VelocityGroundFlyingPieces = randValue => Range(randValue, 1f, 3f);

        public static float Range(float randValue, float min, float max)
        {
            if (max <= min)
            {
                return min;
            }
            return randValue * (max - min) + min;
        }

        public static void CreateFleckBlood140(Pawn target, FleckDef fleckDef, Color bloodColor, float angle, float totalAmountDealt)
        {
            float scaleRand = Rand.Value;
            float angleRand = Rand.Value;
            FleckCreationData dataStatic = default(FleckCreationData);
            dataStatic.def = fleckDef;
            dataStatic.ageTicksOverride = -1;
            dataStatic.instanceColor = bloodColor;
            dataStatic.spawnPosition = target.DrawPos;
            dataStatic.spawnPosition.x += Range(scaleRand, -0.1f, 0.1f);
            dataStatic.scale = Scale(scaleRand);
            dataStatic.rotation = Angle360(angleRand);
            dataStatic.velocityAngle = angle + Angle140(angleRand);
            dataStatic.velocitySpeed = 10f;
            dataStatic.airTimeLeft = AirTime360Blood(scaleRand) * Math.Min(totalAmountDealt / DamageForMaxTravelDistance, 1f);
            dataStatic.solidTimeOverride = SolidTime(scaleRand);
            dataStatic.link = new FleckAttachLink(new TargetInfo(target));
            target.MapHeld.flecks.CreateFleck(dataStatic);
        }

        public static void CreateFleckBloodDirectional(Pawn target, FleckDef fleckDef, Color bloodColor, float angle, float totalAmountDealt)
        {
            float scaleRand = Rand.Value;
            float angleRand = Rand.Value;
            FleckCreationData dataStatic = default(FleckCreationData);
            dataStatic.def = fleckDef;
            dataStatic.ageTicksOverride = -1;
            dataStatic.instanceColor = bloodColor;
            dataStatic.spawnPosition = target.DrawPos;
            dataStatic.spawnPosition.x += Range(scaleRand, -0.1f, 0.1f);
            dataStatic.scale = Scale(scaleRand);
            dataStatic.rotation = Angle360(angleRand);
            dataStatic.velocityAngle = angle + AngleDirectional(angleRand);
            dataStatic.velocitySpeed = 15f;
            dataStatic.airTimeLeft = AirTimeDirectional(scaleRand) * Math.Min(totalAmountDealt / DamageForMaxTravelDistance, 1f);
            dataStatic.solidTimeOverride = SolidTime(scaleRand);
            dataStatic.link = new FleckAttachLink(new TargetInfo(target));
            target.MapHeld.flecks.CreateFleck(dataStatic);
        }

        public static void CreateFleckDeathBlood(Thing target, FleckDef fleckDef, Color bloodColor, float angle, float distance)
        {
            float scaleRand = Rand.Value;
            float angleRand = Rand.Value;
            FleckCreationData dataStatic = default(FleckCreationData);
            dataStatic.def = fleckDef;
            dataStatic.ageTicksOverride = -1;
            dataStatic.instanceColor = bloodColor;
            dataStatic.spawnPosition = target.DrawPos;
            dataStatic.spawnPosition.x += Range(scaleRand, -0.1f, 0.1f);
            dataStatic.scale = Scale(scaleRand);
            dataStatic.rotation = Angle360(angleRand);
            dataStatic.velocityAngle = angle;
            dataStatic.velocitySpeed = 10f;
            dataStatic.airTimeLeft = distance;
            dataStatic.solidTimeOverride = SolidTime(scaleRand);
            dataStatic.link = new FleckAttachLink(new TargetInfo(target));
            target.MapHeld.flecks.CreateFleck(dataStatic);
        }

        public static void CreateFleckHeavy360(Thing target, FleckDef fleckDef, float totalAmountDealt, bool isFleshmass = false)
        {
            float scaleRand = Rand.Value;
            float angleRand = Rand.Value;
            FleckCreationData dataStatic = default(FleckCreationData);
            dataStatic.def = fleckDef;
            dataStatic.ageTicksOverride = -1;
            dataStatic.spawnPosition = target.DrawPos;
            dataStatic.scale = Scale(scaleRand);
            dataStatic.rotation = Angle360(angleRand);
            dataStatic.velocityAngle = Angle360(angleRand);
            dataStatic.velocitySpeed = 10f;
            dataStatic.airTimeLeft = AirTime360Parts(scaleRand) * Math.Min(totalAmountDealt / DamageForMaxTravelDistance, 1f);
            dataStatic.solidTimeOverride = SolidTime(scaleRand);
            dataStatic.link = new FleckAttachLink(new TargetInfo(target));
            if (isFleshmass)
            {
                dataStatic.instanceColor = ParticleDefOf.Fuu_Filth_Blood_Fleshbeast.graphicData.color;
                dataStatic.scale *= 1.5f;
            }
            target.MapHeld.flecks.CreateFleck(dataStatic);
        }

        public static void CreateFleckBleed(Pawn target, Color bloodColor, bool isLayingDown, bool isCarryingPawn)
        {
            float angleRand = Rand.Value;
            FleckCreationData dataStatic = default(FleckCreationData);
            dataStatic.def = ParticleDefOf.Fuu_BloodDripParticle;
            dataStatic.ageTicksOverride = -1;
            dataStatic.instanceColor = bloodColor;
            dataStatic.spawnPosition = target.DrawPos;
            dataStatic.spawnPosition.z += InitialHeightOffset * dripBodySizeMultiplier(target.BodySize);
            dataStatic.spawnPosition.x += Rand.Range(-0.2f, 0.2f) * dripBodySizeMultiplier(target.BodySize);
            if (isCarryingPawn)
                dataStatic.spawnPosition.x += 0.5f;
            dataStatic.scale = 0.2f;
            dataStatic.velocityAngle = 180f;
            dataStatic.airTimeLeft = DripAirtime * dripBodySizeMultiplier(target.BodySize);
            dataStatic.velocitySpeed = DripVelocity;
            if (isLayingDown)
            {
                dataStatic.spawnPosition.z += 0.1f;
                dataStatic.velocitySpeed = DripVelocityLayingDown;
            }
            else
                dataStatic.velocitySpeed = DripVelocity;
            target.MapHeld.flecks.CreateFleck(dataStatic);
        }

        public static void CreateFleckBleedPuddle(Pawn target, FleckDef fleckDef, Color bloodColor, bool isLayingDown, bool isCarryingPawn)
        {
            float angleRand = Rand.Value;
            FleckCreationData dataStatic = default(FleckCreationData);
            dataStatic.def = fleckDef;
            dataStatic.ageTicksOverride = -1;
            dataStatic.instanceColor = bloodColor;
            dataStatic.spawnPosition = target.DrawPos;
            dataStatic.spawnPosition.z += InitialHeightOffset * dripBodySizeMultiplier(target.BodySize);
            if (isCarryingPawn)
                dataStatic.spawnPosition.x += 0.5f;
            if (isLayingDown)
                dataStatic.spawnPosition.z -= DripVelocityLayingDown * DripAirtime * dripBodySizeMultiplier(target.BodySize);
            else
                dataStatic.spawnPosition.z -= DripVelocity * DripAirtime * dripBodySizeMultiplier(target.BodySize);
            dataStatic.scale = 0.6f;
            dataStatic.rotation = Angle360(angleRand);
            dataStatic.velocityAngle = 0f;
            dataStatic.velocitySpeed = 0f;
            dataStatic.airTimeLeft = 0.00001f;
            dataStatic.solidTimeOverride = SolidTime(angleRand);
            target.MapHeld.flecks.CreateFleck(dataStatic);
        }

        public static void CreateFleckRicochet(Thing target, float ricochetMagnitude)
        {
            float scaleRand = Rand.Value;
            float angleRand = Rand.Value;
            FleckCreationData dataStatic = default(FleckCreationData);
            dataStatic.def = ParticleDefOf.Fuu_LongSparkThrown;
            dataStatic.ageTicksOverride = -1;
            dataStatic.spawnPosition = target.DrawPos;
            dataStatic.scale = 0.6f;
            dataStatic.velocityAngle = Angle360(angleRand);
            dataStatic.velocitySpeed = 22f;
            dataStatic.airTimeLeft = 1f;
            dataStatic.solidTimeOverride = 0.15f * ricochetMagnitude;
            target.MapHeld.flecks.CreateFleck(dataStatic);
        }
        public static void CreateMoteImpactFlyingPiece(Thing target, ThingDef moteDef, Color flyingPieceColor)
        {
            float scaleRand = Rand.Value;
            float angleRand = Rand.Value;
            Mote dataStatic = (Mote)ThingMaker.MakeThing(moteDef);
            dataStatic.instanceColor = flyingPieceColor;
            dataStatic.exactPosition = target.DrawPos;
            dataStatic.exactPosition.x += Range(scaleRand, -0.2f, 0.2f);
            dataStatic.Scale = Scale(scaleRand);
            dataStatic.exactRotation = Angle360(angleRand);
            dataStatic.curvedScale = moteDef.mote.scalers?.ScaleAtTime(0f) ?? Vector3.one;
            if (dataStatic is MoteThrown moteThrown)
            {
                moteThrown.airTimeLeft = 0.87f;
                moteThrown.SetVelocity(Angle360(angleRand), VelocityImpactFlyingPieces(scaleRand));
            }
            GenSpawn.Spawn(dataStatic, target.DrawPos.ToIntVec3(), target.MapHeld);
            dataStatic.Maintain();
        }
        public static void CreateMoteGroundImpactFlyingPiece(Map map, Vector3 position, ThingDef moteDef, Color flyingPieceColor)
        {
            float scaleRand = Rand.Value;
            float angleRand = Rand.Value;
            Mote dataStatic = (Mote)ThingMaker.MakeThing(moteDef);
            dataStatic.instanceColor = flyingPieceColor;
            dataStatic.exactPosition = position;
            dataStatic.Scale = Scale(scaleRand);
            dataStatic.exactRotation = Angle360(angleRand);
            dataStatic.curvedScale = moteDef.mote.scalers?.ScaleAtTime(0f) ?? Vector3.one;
            if (dataStatic is MoteThrown moteThrown)
            {
                moteThrown.airTimeLeft = 0.87f;
                moteThrown.SetVelocity(Angle360(angleRand), VelocityGroundFlyingPieces(scaleRand));
            }
            GenSpawn.Spawn(dataStatic, position.ToIntVec3(), map);
            dataStatic.Maintain();
        }

        private static float dripBodySizeMultiplier(float bodySize)
        {
            return Mathf.Clamp(bodySize, 0f, 1.5f);
        }
    }
}
