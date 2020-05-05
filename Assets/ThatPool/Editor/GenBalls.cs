using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GenBalls : MonoBehaviour
{
    [MenuItem("UdonPool/Generate Balls")]
    static void Balls()
    {
        var radius = 0.0285f;
        var parent = GameObject.Find("Balls").transform;
        GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/ThatPool/PoolBall.prefab", typeof(GameObject));
        Debug.Log($"prefab {prefab}");
        for (int i = 0; i < 5; ++i)
        {
            var z = i * (Mathf.Sin(60 * Mathf.Deg2Rad) * radius * 2) + 0.2f;
            var width = (i + 1) * radius * 2;
            for (int j = 0; j <= i; ++j)
            {
                var x = ((j * 2 + 1) * radius) - width / 2;
                GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(prefab, parent);
                go.name = $"ball-{i}-{j}";
                go.transform.localPosition = new Vector3(x, 1 + radius, z);
            }
        }
    }
}
