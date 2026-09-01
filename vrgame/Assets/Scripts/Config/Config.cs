// Beat map data - maps beats to positions
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BeatMapData
{
    public List<float> beatPositions;  // x,y coordinates of key moments
    public float beatInterval = 1.0f; // seconds between beats
}

[System.Serializable]
public class GameConfig
{
    public BeatMapData beatMap;
    public int initialHearts = 3;
    public float swingCooldown = 0.5f;
}

public class Config : MonoBehaviour
{
    public BeatMapData beatMap;
    public int hearts = 3;
    
    void Start()
    {
        // Load beat map from audio analysis or manual entry
        // Example: { "beat1": (0.5, 0.5), "beat2": (1.0, 0.5), ... }
    }
}
