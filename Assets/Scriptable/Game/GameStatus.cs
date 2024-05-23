using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public enum gameStatus
{
    Active,
    Paused,
    Ended
}

public enum Character
{
    Law
}

[CreateAssetMenu(fileName = "GameStatus", menuName = "PlayerInformation/Game/GameStatus", order = 1)]
public class GameStatus : ScriptableObject
{
    public CharacterBaseData []Roles = {};
    public TMP_FontAsset commonFont;
    public gameStatus nowStatus = gameStatus.Active;
    #region Listen Event Setting
    public bool _catach = false;
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

    private AsyncOperationHandle<SceneInstance> _loadingSceneHandle;
    public UnityEvent<AsyncOperationHandle<SceneInstance>> loadingSceneHandleChange;
    public AsyncOperationHandle<SceneInstance> LoadingSceneHandle
    {
        get => _loadingSceneHandle;
        set
        {
            _loadingSceneHandle = value;
            loadingSceneHandleChange.Invoke(_loadingSceneHandle);
            LoadingSceneHandle.Completed += (Handle) =>
            {
                SceneManager.SetActiveScene(LoadingSceneHandle.Result.Scene);
            };
        }
    }
    #endregion

    public void ExitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}
