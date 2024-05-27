using UnityEngine;
using UnityEngine.Events;

//Storage battle information
[CreateAssetMenu(fileName = "PlayerBattleInformation", menuName = "PlayerInformation/Player/PlayerBattleInformation", order = 2)]
public class PlayerBattleValueScriptable : ScriptableObject
{
    private void OnEnable()
    {
        MaxHealth = initM_Hp;
    }
    #region BasicBattleValue
    [Header("BasicBattleValue")]
    public GameObject Player;
    public float initM_Hp;
    private float m_Health;
    public float MaxHealth
    {
        get
        {
            return m_Health;
        }
        set
        {
            m_Health = value;
            CurrentHealth = value;
        }
    }
    private float CurrentHealth;
    public UnityEvent HealthChange;
    public float GetHealth()
    {
        return CurrentHealth;
    }

    public void ReduceHealth(float Damage)
    {
        Debug.Log("Player be attacked");
        CurrentHealth -= Damage;
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
            Debug.Log("Player Die");
        }
        HealthChange.Invoke();
    }

    public CharacterBaseData Role;
    private GameObject Weapon;
    public WeaponsDataFetch nowWeaponData;
    public GameObject nowWeapon
    {
        get
        {
            return Weapon;
        }
        set
        {
            Weapon = value;
            nowWeaponData = value.GetComponent<WeaponsDataFetch>();
        }
    }

    public void ChangeWeapon(GameObject weapon)
    {
        if (weapon.tag != "Weapon")
        {
            return;
        }

        Weapon = weapon;
    }
    #endregion

    #region Scroll
    //Storage scrolls
    #endregion

    #region Awakening
    //Storage awakening
    #endregion
}
