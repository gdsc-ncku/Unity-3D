using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameStatus gameStatus;
    [SerializeField] AssetReference mainScene, advenatureScene;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForLoadingMainScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitForLoadingMainScene()
    {
        yield return new WaitUntil(() => gameStatus.CatchData);
        gameStatus.LoadingSceneHandle = Addressables.LoadSceneAsync(mainScene, LoadSceneMode.Additive);
        gameStatus.LoadingSceneHandle.Completed += (Handle) =>
        {
            Addressables.Release(gameStatus.LoadingSceneHandle);
        };
    }
}
