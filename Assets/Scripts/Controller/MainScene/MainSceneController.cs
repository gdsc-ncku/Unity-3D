using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
    [SerializeField] GameStatus gameStatus;
    [SerializeField] AssetReference advenatureScene, mainScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if (advenatureScene != null)
        {
            AsyncOperationHandle unLoad = gameStatus.LoadingSceneHandle;
            Addressables.UnloadSceneAsync(unLoad);
            gameStatus.LoadingSceneHandle = Addressables.LoadSceneAsync(advenatureScene, LoadSceneMode.Additive);
        }
    }

    public void ExitGame()
    {
       gameStatus.ExitGame();
    }
}
