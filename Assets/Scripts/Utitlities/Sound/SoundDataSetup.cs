using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sounds
{
    [CreateAssetMenu(fileName = "New SoundConfig", menuName = "Sounds/SoundConfig")]
    public class SoundDataSetup : ScriptableObject
    {
        [field:SerializeField] public List<SoundData> SoundDataList { get; set; }
    }
    
    [Serializable]
    public class SoundData
    {
        [field: SerializeField] public string Type { get; private set; }
        
        [field: SerializeField] public List<AudioClip> Sound { get; private set; }
    }
}