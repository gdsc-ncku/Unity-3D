using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    [SerializeField] private Text uiText;
    [SerializeField] private GameStatus gameStatus;
    [SerializeField] GameObject WinUI, PlayerUI;

    private bool Pause = false;

    private void OnEnable()
    {
        gameStatus.settingTimer.AddListener(Being);
    }

    private void OnDisable()
    {
        gameStatus.settingTimer.RemoveListener(Being);
    }

    private void OnDestroy()
    {
        gameStatus.settingTimer.RemoveListener(Being);
    }

    private void Being()
    {
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (gameStatus.RemainingDuration >= 0)
        {
            if (!Pause)
            {
                uiText.text = $"{gameStatus.RemainingDuration / 60:00}:{gameStatus.RemainingDuration % 60:00}";
                gameStatus.RemainingDuration--;
                yield return new WaitForSeconds(1f);
            }
            else
            {
                yield return null;
            }
        }
        OnEnd();
    }

    public void WinGame()
    {
        PlayerUI.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        WinUI.SetActive(true);
        Time.timeScale = 0;
    }

    private void OnEnd()
    {
        //End Time , if want Do something
        WinGame();
    }
}
