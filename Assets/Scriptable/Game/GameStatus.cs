using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public enum Character
{
    Law
}

[CreateAssetMenu(fileName = "GameStatus", menuName = "PlayerInformation/Game/GameStatus", order = 1)]
public class GameStatus : ScriptableObject
{
    public TMP_FontAsset commonFont;
    public UnityEvent settingTimer, settingLevel;
    public AssetReference mainScene, adventureScene;
    public PlayerBattleValueScriptable playerBattle;
    public float GenerateSpeed;
    public int enemyNum = 0;
    public int remainingDuration = 0;
    private int level = 0, duration = 0;
    public int Level
    {
        get 
        { 
            return level; 
        }

        set 
        { 
            level = value;
            settingLevel.Invoke();
        }
    }

    public int Duration
    {
        get
        { 
            return duration; 
        }

        set
        {
            duration = value;
            RemainingDuration = value;
            settingTimer.Invoke();
        }
    }
    public int RemainingDuration
    {
        get
        { 
            return remainingDuration; 
        }

        set 
        {
            remainingDuration = value;
        }
    }
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
                SceneManager.SetActiveScene(_loadingSceneHandle.Result.Scene);
            };
        }
    }
    #endregion

    public void LoadOtherScene(bool doUnload, AssetReference scene)
    {
        if (doUnload)
        {
            AsyncOperationHandle unLoad = LoadingSceneHandle;
            Addressables.UnloadSceneAsync(unLoad);
        }
        LoadingSceneHandle = Addressables.LoadSceneAsync(scene, LoadSceneMode.Additive);
    }

    public void StartGame()
    {
        ResetGame();
    }

    //Reset game and level
    public void ResetGame()
    {
        AsyncOperationHandle unLoad = LoadingSceneHandle;
        Addressables.UnloadSceneAsync(unLoad);
        LoadingSceneHandle = Addressables.LoadSceneAsync(adventureScene, LoadSceneMode.Additive);
        LoadingSceneHandle.Completed += (Handle) => 
        {
            enemyNum = 1;
            Level = 1;
            playerBattle.MaxHealth = playerBattle.initM_Hp;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        };
    }

    //Reset game and go next level
    public void ResetGame(int nextLevel)
    {
        AsyncOperationHandle unLoad = LoadingSceneHandle;
        Addressables.UnloadSceneAsync(unLoad);
        LoadingSceneHandle = Addressables.LoadSceneAsync(adventureScene, LoadSceneMode.Additive);
        LoadingSceneHandle.Completed += (Handle) =>
        {
            enemyNum = 1;
            Level = nextLevel;
            playerBattle.MaxHealth = playerBattle.initM_Hp;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        };
    }

    public void ExitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}
