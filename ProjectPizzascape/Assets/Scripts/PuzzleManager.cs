using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/PuzzleManager", order = 1)]
public class PuzzleManager : ScriptableObject
{
    public List<int> PuzzleIds = new List<int>();
    public List<GameObject> PuzzlePrefabs = new List<GameObject>();
}
