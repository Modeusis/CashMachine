using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sounds
{
    [CreateAssetMenu(menuName = "Setups/Sound Config")]
    public class SoundDataSetup : ScriptableObject
    {
        [field:SerializeField] public List<SoundData> SoundDataList { get; set; }
    }
    
    [Serializable]
    public class SoundData
    {
        [field: SerializeField] public SoundType Type { get; private set; }
        [field: SerializeField] public List<AudioClip> Sounds { get; private set; }
    }
}