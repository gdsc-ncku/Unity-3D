using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
    [SerializeField] MainSceneManager mainSceneManager;

    public void StartGame()
    {
        mainSceneManager.gameStatus.StartGame();
    }

    public void ExitGame()
    {
        mainSceneManager.gameStatus.ExitGame();
    }
}
