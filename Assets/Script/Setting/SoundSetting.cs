using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;
using UnityEngine.Rendering.Universal;

public class SoundSetting : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    //»ç¿îµå
    [SerializeField]
    private Slider master;
    [SerializeField]
    private Slider BGM;
    [SerializeField]
    private Slider soundEffect;

    public Action<float> masterHandler;
    public Action<float> BGMHandler;
    public Action<float> soundEffectHandler;

    public void Refresh()
    {
        if(audioMixer.GetFloat("Master", out float value))
        {
            master.value = value;
        }
        if (audioMixer.GetFloat("BGM", out value))
        {
            BGM.value = value;
        }
        if (audioMixer.GetFloat("SoundEffect", out value))
        {
            soundEffect.value = value;
        }
    }

    public void Apply(float master, float bgm, float soundEffect)
    {
        audioMixer.SetFloat("Master", master);

        audioMixer.SetFloat("BGM", bgm);

        audioMixer.SetFloat("SoundEffect", soundEffect);
        Refresh();
    }

    public void OnSliderMaster()
    {
        audioMixer.SetFloat("Master", master.value);
        masterHandler?.Invoke(master.value);
    }
    public void OnSliderBGM()
    {
        audioMixer.SetFloat("BGM", BGM.value);
        BGMHandler?.Invoke(BGM.value);
    }
    public void OnSliderSoundEffect()
    {
        audioMixer.SetFloat("SoundEffect", soundEffect.value);
        soundEffectHandler?.Invoke(soundEffect.value);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
