using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Slider HealthBar;
    public Text BulletLeftNum;
    public Text BulletMaxNum;
    public Image WeaponIcon;
    public TextMeshProUGUI HealthValueDisplay;
    [SerializeField] PlayerBattleValueScriptable PlayerBattleInfo;
    [SerializeField] PlayerBasicInformationScriptable PlayerBasicInfo;
    [SerializeField] GameStatus gameStatus;
    [SerializeField] GameObject playerDieUI, pauseUI, winU, playerUI;
    // Start is called before the first frame update
    void Start()
    {
        PlayerBattleInfo.PlayerDieUI = playerDieUI;
        HealthBar.maxValue = PlayerBattleInfo.Role.GetComponent<StudentDataManager>().studentData.Health;
        HealthBar.value = HealthBar.maxValue;
        HealthValueDisplay.text = HealthBar.value.ToString() + "/" + HealthBar.maxValue.ToString();
        PlayerBattleInfo.HealthChange.AddListener(ChangeHealthBar);
    }

    private void OnEnable()
    {
        PlayerBasicInfo.playerControl.Player.Pause.performed += Pause;
    }

    private void OnDisable()
    {
        PlayerBasicInfo.playerControl.Player.Pause.performed -= Pause;
    }

    private void OnDestroy()
    {
        PlayerBasicInfo.playerControl.Player.Pause.performed -= Pause;
    }

    private void ChangeHealthBar()
    {
        if(PlayerBattleInfo != null)
        {
            HealthBar.value = PlayerBattleInfo.GetHealth();
            HealthValueDisplay.text = HealthBar.value.ToString() + "/" + HealthBar.maxValue.ToString();
        }
        
    }

    public void ChangeWeaponInfo()
    {
        BulletMaxNum.text = "/ " + PlayerBattleInfo.nowWeaponData.bulletsMax.ToString();
        BulletLeftNum.text = PlayerBattleInfo.nowWeaponData.bulletsLeft.ToString();
        WeaponIcon.sprite = PlayerBattleInfo.nowWeaponData.ThisWeapon.Icon;
    }

    public void BulletLeftNumUpdate()
    {
        BulletLeftNum.text = PlayerBattleInfo.nowWeaponData.bulletsLeft.ToString();
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (pauseUI.activeSelf)
        {
            playerUI.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }
        else
        {
            playerUI.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }
    }

    public void Pause()
    {
        if (pauseUI.activeSelf)
        {
            PlayerBasicInfo.playerControl.Player.Disable();
            playerUI.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }
        else
        {
            PlayerBasicInfo.playerControl.Player.Enable();
            playerUI.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }
    }

    public void Menu()
    {
        PlayerBasicInfo.playerControl.Player.Enable();
        Time.timeScale = 1;
        gameStatus.LoadOtherScene(true, gameStatus.mainScene);
    }

    public void NextLevel()
    {
        PlayerBasicInfo.playerControl.Player.Enable();
        int level = gameStatus.Level;
        gameStatus.ResetGame();
        gameStatus.Level = level + 1;
    }

    public void Exit()
    {
        Time.timeScale = 1;
        gameStatus.ExitGame();
    }
}
