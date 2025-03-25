using HarmonyLib;
using RedPill.Scripts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;

namespace RedPill.Patches
{
    [HarmonyPatch(typeof(TestEnemy))]
    internal class TestEnemyPatch
    {
        [HarmonyPatch(nameof(TestEnemy.Start))]
        [HarmonyPostfix]
        static void ChangeInstanceVars(ref TestEnemy __instance)
        {
            __instance.detectionRadius = ConfigController.playerDetectionRadius.Value;

            Vector3 pos = __instance.transform.position;
            // must've spawned outside if nearest vent is far away
            __instance.isOutside = RoundManager.Instance.allEnemyVents.Min<EnemyVent>(vent => (vent.transform.position - pos).sqrMagnitude) > 5;

            ModDebug.LogInfo($"isOutside = {__instance.isOutside}");
        }

        [HarmonyPatch(nameof(TestEnemy.DoAIInterval))]
        [HarmonyPostfix]
        static void UpdateSpeed(ref TestEnemy __instance)
        {
            if (!ConfigController.useOriginalAI.Value) { return; }

            // slow down when close to player
            float proximityEffect = Mathf.InverseLerp(15f, 3f, __instance.closestPlayerDist) * ConfigController.agentSpeedSlowDownAmount.Value;
            __instance.agent.speed = ConfigController.agentSpeedBase.Value;
            __instance.agent.speed /= Mathf.Clamp(proximityEffect, 1f, 999f);

            // slower if outside
            if (__instance.isOutside)
            {
                __instance.agent.speed *= 0.5f;
            }
        }
    }
}
