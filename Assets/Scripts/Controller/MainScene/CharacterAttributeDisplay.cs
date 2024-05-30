using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterAttributeDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject attribute, department;
    [SerializeField] TextMeshProUGUI Health, Defense, AttackRate, WalkSpeed;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Health.text = department.GetComponent<StudentDataManager>().studentData.Health.ToString();
        Defense.text = department.GetComponent<StudentDataManager>().studentData.Defense.ToString();
        AttackRate.text = department.GetComponent<StudentDataManager>().studentData.AttackDamage.ToString();
        WalkSpeed.text = department.GetComponent<StudentDataManager>().studentData.WalkSpeed.ToString();
        attribute.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        attribute.SetActive(false);
    }
}
