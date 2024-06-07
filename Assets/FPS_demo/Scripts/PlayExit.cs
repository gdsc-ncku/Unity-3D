using 
/* 取消合併專案 'Assembly-CSharp.Player' 的變更
之前:
using UnityEngine.UI;
之後:
using UnityEngine.EventSystems;
*/
UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayExit : MonoBehaviour
{
    public Button PlayBtn;
    public Button ExitBtn;
    public string sceneName;
    // Start is called before the first frame update
    private void Awake()
    {
    }
    void Start()
    {
        PlayBtn.onClick.AddListener(LoadScene);
        ExitBtn.onClick.AddListener(ExitGame);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    void ExitGame()
    {
        Application.Quit();
    }
}
