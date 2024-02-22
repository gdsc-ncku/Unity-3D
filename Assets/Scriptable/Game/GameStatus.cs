using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public enum gameStatus
{
    Active,
    Loading,
    Paused,
    Ended
}

[CreateAssetMenu(fileName = "GameStatus", menuName = "PlayerInformation/Game/GameStatus", order = 1)]
public class GameStatus : ScriptableObject
{
    public gameStatus nowStatus = gameStatus.Active;
    public bool catchData = false;
}
