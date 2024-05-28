using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerGetDamage : MonoBehaviour
{
    [SerializeField] PlayerBattleValueScriptable PlayerInfo;
    private float intensity = 0;
    private Volume volume;
    private Vignette vignette;
    // Start is called before the first frame update
    void Start()
    {
        volume = Camera.main.GetComponent<Volume>();
        if (volume.profile.TryGet<Vignette>(out vignette))
        {
            vignette.color.value = new Color(0.878f, 0.0f, 0.0f);
            vignette.active = false;
        }
        PlayerInfo.HealthDecrease.AddListener(GetDamage);
    }

    private void GetDamage()
    {
        StopCoroutine(GetHitEffect());
        StartCoroutine(GetHitEffect());
    }

    IEnumerator GetHitEffect()
    {
        intensity = 0.4f;
        vignette.active = true;
        vignette.intensity.Override(0.4f);

        yield return new WaitForSeconds(0.2f);

        while (intensity > 0 && PlayerInfo.GetHealth() > 0)
        {
            intensity -= 0.01f;
            if (intensity < 0)
            {
                intensity = 0;
            }

            vignette.intensity.Override(intensity);

            yield return null;
        }

        if (PlayerInfo.GetHealth() > 0)
        {
            vignette.active = false;
        }
        else
        {
            vignette.color.value = Color.black;
        }
        yield break;
    }
}
