using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Slider HealthBar;
    public Text BulletLeftNum;
    public Text BulletMaxNum;
    public Image WeaponIcon;
    [SerializeField] PlayerBattleValueScriptable PlayerInfo;
    // Start is called before the first frame update
    void Start()
    {
        HealthBar.maxValue = PlayerInfo.MaxHealth;
        PlayerInfo.HealthChange.AddListener(ChangeHealthBar);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ChangeHealthBar()
    {
        HealthBar.value = PlayerInfo.GetHealth();
    }

    public void ChangeWeaponInfo()
    {
        BulletMaxNum.text = "/ " + PlayerInfo.nowWeaponData.bulletsMax.ToString();
        BulletLeftNum.text = PlayerInfo.nowWeaponData.bulletsLeft.ToString();
        WeaponIcon.sprite = PlayerInfo.nowWeaponData.ThisWeapon.Icon;
    }

    public void BulletLeftNumUpdate()
    {
        BulletLeftNum.text = PlayerInfo.nowWeaponData.bulletsLeft.ToString();
    }
}
