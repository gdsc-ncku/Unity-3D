using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllScriptableObjects : MonoBehaviour
{
    [SerializeField] CharacterBaseData[] student;
    [SerializeField] PlayerBasicInformationScriptable playerBasicInformation;
    [SerializeField] PlayerBattleValueScriptable playerBattleValue;
    [SerializeField] EnemyScriptableObject[] enemy;
    [SerializeField] GameStatus gameStatus;
    [SerializeField] WeaponData[] weaponData;
}
