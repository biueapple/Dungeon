using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerSetting : MonoBehaviour
{
    [SerializeField]
    private SoundSetting soundSetting;
    [SerializeField]
    private GraphicSetting graphicSetting;
    [SerializeField]
    private Setting setting;

    private void Start()
    {
        if (LoadSaveManager.Instance.LoadJson(ref setting, "setting"))
        {
            //�ε� ����
            graphicSetting.Apply(setting.Resolution, setting.FrameRate, setting.FullScreenMode, setting.Antialiasing);
            soundSetting.Apply(setting.FullSound, setting.BGM, setting.SoundEffect);
        }
        else
        {
            //�ε� ����
            graphicSetting.Apply(graphicSetting.Defalut, 60, FullScreenMode.FullScreenWindow, AntialiasingMode.None);
            soundSetting.Refresh();
            setting.Resolution = graphicSetting.Defalut;
            setting.FrameRate = 60;
            setting.FullScreenMode = FullScreenMode.FullScreenWindow;
            setting.Antialiasing = AntialiasingMode.None;
            setting.FullSound = 0;
            setting.BGM = 0;
            setting.SoundEffect = 0;
        }

        graphicSetting.ResolutionHandler += ResolutionHandler;
        graphicSetting.FrameHandler += FrameHandler;
        graphicSetting.ScreenModeHandler += FullScreenModeHandler;
        graphicSetting.Anti_AliasingHandler += AntialiasingHandler;
        soundSetting.masterHandler += FullSoundHandler;
        soundSetting.BGMHandler += BGMHandler;
        soundSetting.soundEffectHandler += SoundEffectHandler;

        graphicSetting.gameObject.SetActive(false);
        soundSetting.gameObject.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        LoadSaveManager.Instance.SaveJson(setting, "setting");
    }

    private void ResolutionHandler(Resolution resolution)
    {
        setting.Resolution = resolution;
    }
    private void FrameHandler(int frame)
    {
        setting.FrameRate = frame;
    }
    private void FullScreenModeHandler(FullScreenMode mode)
    {
        setting.FullScreenMode = mode;
    }
    private void AntialiasingHandler(AntialiasingMode mode)
    {
        setting.Antialiasing = mode;
    }
    private void FullSoundHandler(float value)
    {
        setting.FullSound = value;
    }
    private void BGMHandler(float value)
    {
        setting.BGM = value;
    }
    private void SoundEffectHandler(float value)
    {
        setting.SoundEffect = value;
    }
}

[System.Serializable]
public class Setting
{
    //���
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;
    [SerializeField]
    private RefreshRate refreshRate;

    public Resolution Resolution { get { return new Resolution() { width = width, height = height, refreshRateRatio = refreshRate }; } set { width = value.width; height = value.height; refreshRate = value.refreshRateRatio; } }
    //������
    [SerializeField]
    private int frameRate;
    public int FrameRate { get { return frameRate; } set { frameRate = value; } }
    //ȭ����
    [SerializeField]
    private FullScreenMode fullScreenMode;
    public FullScreenMode FullScreenMode { get { return fullScreenMode; } set { fullScreenMode = value; } }
    //��Ƽ���ϸ����
    [SerializeField]
    private AntialiasingMode antialiasing;
    public AntialiasingMode Antialiasing { get { return antialiasing; } set { antialiasing = value; } }

    //��ü����
    [SerializeField]
    private float fullSound;
    public float FullSound { get { return fullSound; } set { fullSound = value; } }
    //bgm
    [SerializeField]
    private float bgm;
    public float BGM { get { return bgm; } set { bgm = value; } }
    //ȿ����
    [SerializeField]
    private float soundEffect;
    public float SoundEffect { get { return soundEffect; } set { soundEffect = value; } }
}

