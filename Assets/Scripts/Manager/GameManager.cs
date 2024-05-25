using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int level = 0;
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
        if(Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.DeleteKey("IsFirstTime");
            Debug.Log("Reset");
        }
    }

    IEnumerator WaitForLoadingMainScene()
    {
        yield return new WaitUntil(() => gameStatus.CatchData);
        gameStatus.LoadingSceneHandle = Addressables.LoadSceneAsync(mainScene, LoadSceneMode.Additive);
    }

    public void GameStart()
    {
        level++;
    }

    public void GameReset()
    {
        level = 0;
    }

    public void level1()
    {

    }
}
