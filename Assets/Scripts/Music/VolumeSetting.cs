using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider backgroundSlider;

    private void Awake()
    {
        SetBackgroundVolume();
        SetSFXVolume();
        gameObject.SetActive(false);
    }

    public void SetBackgroundVolume()
    {
        float _volume = backgroundSlider.value;
        audioMixer.SetFloat("Background", Mathf.Log10(_volume) * 20);
    }

    public void SetSFXVolume()
    {
        float _volume = sfxSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(_volume) * 20);
    }
}
