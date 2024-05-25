using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image uiFill;
    [SerializeField] private Text uiText;
    [SerializeField] private GameStatus gameStatus;

    private bool Pause = false;

    private void Start()
    {
        gameStatus.settingTimer.AddListener(() => Being());
    }

    private void Being()
    {
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while(gameStatus.RemainingDuration >= 0)
        {
            if (!Pause)
            {
                uiText.text = $"{gameStatus.RemainingDuration / 60:00}:{gameStatus.RemainingDuration % 60:00}";
                uiFill.fillAmount = Mathf.InverseLerp(0, gameStatus.Duration, gameStatus.RemainingDuration);
                gameStatus.RemainingDuration--;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
        OnEnd();
    }

    private void OnEnd()
    {
        //End Time , if want Do something
        print("End");
    }
}
