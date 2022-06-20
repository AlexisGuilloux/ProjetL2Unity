using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/AudioBank", order = 1)]
public class AudioBank : ScriptableObject
{
    public List<AudioClip> audioClips = new List<AudioClip>();
}
