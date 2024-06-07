using UnityEngine;

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
