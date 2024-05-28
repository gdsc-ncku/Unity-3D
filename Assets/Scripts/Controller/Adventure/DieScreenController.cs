using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DieScreenController : MonoBehaviour
{
    [SerializeField] GameStatus gameStatus;
    public void MainScene()
    {
        gameStatus.LoadOtherScene(true, gameStatus.mainScene);
    }

    public void Retry()
    {
        gameStatus.ResetGame();
    }
}
