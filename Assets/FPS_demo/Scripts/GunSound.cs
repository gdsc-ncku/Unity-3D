using UnityEngine;

public class GunSound : MonoBehaviour
{
    public static GunSound Instance { get; set; }

    public AudioSource shootingSound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
