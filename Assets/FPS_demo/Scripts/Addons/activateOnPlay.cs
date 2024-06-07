using TMPro;
using UnityEngine;


public class activateOnPlay : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().enabled = true;
    }
}
