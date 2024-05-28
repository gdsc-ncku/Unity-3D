using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Slider HealthBar;
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
}
