using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
    [SerializeField] MainSceneManager mainSceneManager;
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
        if (mainSceneManager.advenatureScene != null)
        {
            AsyncOperationHandle unLoad = mainSceneManager.gameStatus.LoadingSceneHandle;
            Addressables.UnloadSceneAsync(unLoad);
            mainSceneManager.gameStatus.LoadingSceneHandle = Addressables.LoadSceneAsync(mainSceneManager.advenatureScene, LoadSceneMode.Additive);
            mainSceneManager.gameStatus.LoadingSceneHandle.Completed += (Handle) => { mainSceneManager.gameStatus.Level = 1; };
        }
    }

    public void ExitGame()
    {
        mainSceneManager.gameStatus.ExitGame();
    }
}
