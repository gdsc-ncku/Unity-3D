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
        //This method will directly return AsyncOperationHandle<T> to LoadPersistance thus LoadPersistence need
        //declare the var which type is AsyncOperationHandle<SceneInstance>
        //But if we use the other method such as line 28~33, the return var will be transformed from AsyncOperationHandle<SceneInstance> to AsyncOperationHandle
        Addressables.LoadSceneAsync(LoadingScene, LoadSceneMode.Additive).Completed += LoadPersistence;
    }

    void LoadPersistence(AsyncOperationHandle<SceneInstance> asyncOperationHandle)
    {
        if(asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            //Unload Scene which follow the build setting order
            //Because InitializationScene is the only one Scene in build thus use order 0;
            gameStatus.LoadingSceneHandle = Addressables.LoadSceneAsync(PersistenceScene, LoadSceneMode.Additive);
            gameStatus.LoadingSceneHandle.Completed += (Handle) => 
            {
                SceneManager.UnloadSceneAsync(0);
                //Release handle to avoid memory leak 
                Addressables.Release(gameStatus.LoadingSceneHandle);
            };
        }
        else
        {
            Debug.LogError("Fail to load the PersistenceScene");
        }

        //Release handle to avoid memory leak 
        Addressables.Release(asyncOperationHandle);
    }
}
