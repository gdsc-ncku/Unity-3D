using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public enum gameStatus
{
    Active,
    Paused,
    Ended
}

[CreateAssetMenu(fileName = "GameStatus", menuName = "PlayerInformation/Game/GameStatus", order = 1)]
public class GameStatus : ScriptableObject
{
    public TMP_FontAsset commonFont;
    public gameStatus nowStatus = gameStatus.Active;
    #region Listen Event Setting
    private bool _catach = false;
    public UnityEvent<bool> catachDataSuccess;
    public bool CatchData
    {
        get => _catach;
        set
        {
            if (value)
            {
                _catach = value;
                catachDataSuccess.Invoke(_catach);
            }
        }
    }

    private AsyncOperationHandle _loadingSceneHandle;
    public UnityEvent<AsyncOperationHandle> loadingSceneHandleChange;
    public AsyncOperationHandle LoadingSceneHandle
    {
        get => _loadingSceneHandle;
        set
        {
            _loadingSceneHandle = value;
            loadingSceneHandleChange.Invoke(_loadingSceneHandle);
        }
    }
    #endregion

    public void ExitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}
