using HarmonyLib;
using RedPill.Scripts;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RedPill.Patches
{
    [HarmonyPatch(typeof(RoundManager))]
    internal class RoundManagerPatch
    {
        [HarmonyPatch(nameof(RoundManager.FinishGeneratingLevel))]
        [HarmonyPostfix]
        static void AddRedPillToEnemiesList()
        {
            EnemyType redPillEnemyType = null;
            AnimationCurve probabilityCurve = null;
            
            foreach (EnemyType e in Resources.FindObjectsOfTypeAll(typeof(EnemyType)))
            {
                if (e.name == "Flowerman")
                {
                    ModDebug.LogInfo($"Found {e.name}! Stealing probability curve...");
                    probabilityCurve = e.probabilityCurve;
                    string curveDetails = "curve: ";
                    foreach (Keyframe keyf in probabilityCurve.keys)
                    {
                        curveDetails += $"[{keyf.time}:{keyf.value}]";
                    }
                    ModDebug.LogInfo(curveDetails);
                }
                if (e.name == "RedPillEnemyType")
                {
                    ModDebug.LogInfo($"Found {e.name}!");
                    redPillEnemyType = e;
                }
                ModDebug.LogInfo($"{e.name}, max: {e.MaxCount}, curve: {e.probabilityCurve.length}, spawningDisabled: {e.spawningDisabled}, power: {e.PowerLevel}");
            }

            SpawnableEnemyWithRarity redPill = new SpawnableEnemyWithRarity();
            if (redPillEnemyType != null)
            {
                redPill.enemyType = redPillEnemyType;
                redPill.enemyType.probabilityCurve = probabilityCurve;
                redPill.enemyType.PowerLevel = 2;
                redPill.enemyType.MaxCount = ConfigController.spawnCount.Value;
                redPill.rarity = ConfigController.spawnRarityGeneral.Value;

                if (ConfigController.spawnLocation.Value != ConfigController.SpawnLocation.Outside)
                {
                    RoundManager.Instance.currentLevel.Enemies.Add(redPill);
                    ModDebug.LogInfo("Added RedPill to inside enemies");
                }
                if (ConfigController.spawnLocation.Value != ConfigController.SpawnLocation.Inside)
                {
                    RoundManager.Instance.currentLevel.OutsideEnemies.Add(redPill);
                    ModDebug.LogInfo("Added RedPill to outside enemies");
                }
            }

            #region list all enemies
            ModDebug.LogInfo("List of enemies on this level");
            foreach (SpawnableEnemyWithRarity e in RoundManager.Instance.currentLevel.Enemies)
            {
                ModDebug.LogInfo($"(Rarity: {e.rarity}) {e.enemyType.name}");
            }
            #endregion
        }
    }
}
