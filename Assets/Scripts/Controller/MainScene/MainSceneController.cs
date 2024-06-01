using System.Collections.Generic;
using UnityEngine;

enum ContentsName { TalentContent, HeroContent, SettingContent };
public class MainSceneController : MonoBehaviour
{
    [SerializeField] MainSceneManager mainSceneManager;
    private GameObject NowContent;
    public List<GameObject> Contents;
    ContentsName contentsName;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            NowContent.SetActive(false);
            PlayerPrefs.Save();
        }
    }

    public void StartGame()
    {
        mainSceneManager.gameStatus.StartGame();
    }

    public void OpenSettingUI()
    {
        contentsName = ContentsName.SettingContent;
        OpenContent();
    }

    public void OpenHeroUI()
    {
        contentsName = ContentsName.HeroContent;
        OpenContent();
    }

    public void ExitGame()
    {
        mainSceneManager.gameStatus.ExitGame();
    }

    public void OpenContent()
    {
        NowContent = Contents[(int)contentsName];
        NowContent.SetActive(true);
    }
}
