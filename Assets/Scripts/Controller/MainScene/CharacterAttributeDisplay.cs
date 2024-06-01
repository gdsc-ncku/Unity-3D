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
        CharacterBaseData student = department.GetComponent<StudentDataManager>().studentData; 
        Health.text = (student.Health * student.HealthRate).ToString();
        Defense.text = (student.Defense * student.DefenseRate).ToString();
        AttackRate.text = (student.AttackDamage * student.AttackRate).ToString();
        WalkSpeed.text = (student.WalkSpeed * student.WalkSpeedRate).ToString();
        MainDamage.text = (student.Q_SkillDamage * student.Q_SkillDamageRate).ToString();
        SecondDamage.text = (student.E_SkillDamage * student.E_SkillDamageRate).ToString();

        Health.transform.parent.GetComponent<TextMeshProUGUI>().text = "��q:(" + ((student.HealthRate - 1) * 10).ToString() + ")";
        Defense.transform.parent.GetComponent<TextMeshProUGUI>().text = "���m:(" + ((student.HealthRate - 1) * 10).ToString() + ")";
        AttackRate.transform.parent.GetComponent<TextMeshProUGUI>().text = "�������v:(" + ((student.HealthRate - 1) * 10).ToString() + ")";
        WalkSpeed.transform.parent.GetComponent<TextMeshProUGUI>().text = "���ʳt��:(" + ((student.HealthRate - 1) * 10).ToString() + ")";
        MainDamage.transform.parent.GetComponent<TextMeshProUGUI>().text = "�D�ޯ�ˮ`:(" + ((student.HealthRate - 1) * 10).ToString() + ")";
        SecondDamage.transform.parent.GetComponent<TextMeshProUGUI>().text = "�Ƨޯ�ˮ`:(" + ((student.HealthRate - 1) * 10).ToString() + ")";

        attribute.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        attribute.SetActive(false);
    }
}
