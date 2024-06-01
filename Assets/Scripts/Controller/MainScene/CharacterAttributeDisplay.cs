using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterAttributeDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject attribute, department;
    [SerializeField] TextMeshProUGUI Health, Defense, AttackRate, WalkSpeed, MainDamage, SecondDamage;
    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdateDisplay();

        attribute.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        attribute.SetActive(false);
    }

    public void UpdateDisplay()
    {
        CharacterBaseData student = department.GetComponent<StudentDataManager>().studentData;
        Health.text = (student.Health * student.HealthRate).ToString("0.#");
        Defense.text = (student.Defense * student.DefenseRate).ToString("0.#");
        AttackRate.text = (student.AttackDamage * student.AttackRate).ToString("0.#");
        WalkSpeed.text = (student.WalkSpeed * student.WalkSpeedRate).ToString("0.#");
        MainDamage.text = (student.Q_SkillDamage * student.Q_SkillDamageRate).ToString("0.#");
        SecondDamage.text = (student.E_SkillDamage * student.E_SkillDamageRate).ToString("0.#");

        Health.transform.parent.GetComponent<TextMeshProUGUI>().text = "血量:(" + ((student.HealthRate - 1) * 10).ToString("0") + ")";
        Defense.transform.parent.GetComponent<TextMeshProUGUI>().text = "防禦:(" + ((student.DefenseRate - 1) * 10).ToString("0") + ")";
        AttackRate.transform.parent.GetComponent<TextMeshProUGUI>().text = "攻擊倍率:(" + ((student.AttackRate - 1) * 10).ToString("0") + ")";
        WalkSpeed.transform.parent.GetComponent<TextMeshProUGUI>().text = "移動速度:(" + ((student.WalkSpeedRate - 1) * 10).ToString("0") + ")";
        MainDamage.transform.parent.GetComponent<TextMeshProUGUI>().text = "主技能傷害:(" + ((student.Q_SkillDamageRate - 1) * 10).ToString("0") + ")";
        SecondDamage.transform.parent.GetComponent<TextMeshProUGUI>().text = "副技能傷害:(" + ((student.E_SkillDamageRate - 1) * 10).ToString("0") + ")";
    }
}
