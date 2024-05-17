using UnityEngine;

//Storage battle information
[CreateAssetMenu(fileName = "PlayerBattleInformation", menuName = "PlayerInformation/Player/PlayerBattleInformation", order = 2)]
public class PlayerBattleValueScriptable : ScriptableObject
{
    #region BasicBattleValue
    [Header("BasicBattleValue")]
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
        if(weapon.tag != "Weapon")
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
