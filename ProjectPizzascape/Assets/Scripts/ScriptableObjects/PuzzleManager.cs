using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/PuzzleManager", order = 1)]
public class PuzzleManager : ScriptableObject
{
    [FormerlySerializedAs("PuzzleIds")] public List<int> Ids = new List<int>();
    [FormerlySerializedAs("PuzzlePrefabs")] public List<GameObject> Prefabs = new List<GameObject>();
    public List<string> MainName = new List<string>();
}
