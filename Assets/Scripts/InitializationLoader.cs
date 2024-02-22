using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class InitializationLoader : MonoBehaviour
{
    [SerializeField] AssetReferenceT<SceneAsset> InitializationScene, PersistenceScene, LoadingScene;
    [SerializeField] GameStatus gameStatus;
    // Start is called before the first frame update
    void Start()
    {
        Addressables.LoadSceneAsync(LoadingScene, LoadSceneMode.Additive).Completed += LoadPersistence;
    }

    void LoadPersistence(AsyncOperationHandle<SceneInstance> asyncOperationHandle)
    {
        if(asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            //Unload Scene which follow the build setting order
            //Because InitializationScene is the only one Scene in build thus use order 0;
            Addressables.LoadSceneAsync(PersistenceScene, LoadSceneMode.Additive).Completed += (Handle) => 
            {
                SceneManager.UnloadSceneAsync(0); 
            };
        }
        else
        {
            Debug.LogError("Fail to load the PersistenceScene");
        }
    }
}
