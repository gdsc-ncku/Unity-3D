using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [SerializeField] GameObject LawUI, MechanicUI, ChemistryUI;
    [SerializeField] PlayerBattleValueScriptable playerBattle;
    [SerializeField] PlayerBasicInformationScriptable playerBasicInformation;
    private void OnEnable()
    {
        LawUI.transform.GetChild(0).gameObject.SetActive(false);
        MechanicUI.transform.GetChild(0).gameObject.SetActive(false);
        ChemistryUI.transform.GetChild(0).gameObject.SetActive(false);

        if(playerBattle.Role == playerBasicInformation.Law)
        {
            LawUI.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if(playerBattle.Role == playerBasicInformation.Mechanic)
        {
            MechanicUI.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if(playerBattle.Role == playerBasicInformation.Chemistry)
        {
            ChemistryUI.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void chooseLaw()
    {
        LawUI.transform.GetChild(0).gameObject.SetActive(false);
        MechanicUI.transform.GetChild(0).gameObject.SetActive(false);
        ChemistryUI.transform.GetChild(0).gameObject.SetActive(false);

        LawUI.transform.GetChild(0).gameObject.SetActive(true);
        playerBattle.Role = playerBasicInformation.Law;
    }

    public void chooseMechanic()
    {
        LawUI.transform.GetChild(0).gameObject.SetActive(false);
        MechanicUI.transform.GetChild(0).gameObject.SetActive(false);
        ChemistryUI.transform.GetChild(0).gameObject.SetActive(false);

        MechanicUI.transform.GetChild(0).gameObject.SetActive(true);
        playerBattle.Role = playerBasicInformation.Mechanic;
    }

    public void chooseChemistry()
    {
        LawUI.transform.GetChild(0).gameObject.SetActive(false);
        MechanicUI.transform.GetChild(0).gameObject.SetActive(false);
        ChemistryUI.transform.GetChild(0).gameObject.SetActive(false);

        ChemistryUI.transform.GetChild(0).gameObject.SetActive(true);
        playerBattle.Role = playerBasicInformation.Chemistry;
    }
}
