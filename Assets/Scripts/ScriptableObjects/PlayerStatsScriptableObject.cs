using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStatsScriptableObject : ScriptableObject
{
    public float speedPlayer;
    public int restartedScore;
    public int coinScore;
    public bool isRestarted;
    
    public void SetDefaultValues()
    {
        int defaultValue = 0;

        speedPlayer = defaultValue;
        restartedScore = defaultValue;
        coinScore = defaultValue;
    }
}
