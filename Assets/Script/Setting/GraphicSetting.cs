using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GraphicSetting : MonoBehaviour
{
    //guideWindow의 설정
    [SerializeField]
    private Dropdown resolution;
    private readonly List<Resolution> resolutions = new();
    [SerializeField]
    private Dropdown frame;
    private List<int> frameRate;
    [SerializeField]
    private Dropdown screenMode;
    [SerializeField]
    private Dropdown anti_Aliasing;

    public Action<Resolution> ResolutionHandler;
    public Action<int> FrameHandler;
    public Action<FullScreenMode> ScreenModeHandler;
    public Action<AntialiasingMode> Anti_AliasingHandler;

    private void Awake()
    {
        //해상도 옵션 초기화
        resolution.options.Clear();
        resolutions.AddRange(Screen.resolutions);
        for (int i = 0; i < resolutions.Count; i++)
        {
            Dropdown.OptionData optionData = new()
            {
                text = resolutions[i].width + " X " + resolutions[i].height + " " + (int)resolutions[i].refreshRateRatio.value + "hz"
            };
            resolution.options.Add(optionData);

            if (Screen.width == resolutions[i].width && Screen.height == resolutions[i].height)
            {
                resolution.value = i;
            }
        }
        resolution.RefreshShownValue();

        //프레임 옵션 초기화
        frame.options.Clear();
        if (Application.targetFrameRate > 240)
            Application.targetFrameRate = 240;
        frameRate = new() 
        {
            240,
            120,
            60,
            30
        };
        for (int i = 0; i < frameRate.Count; i++)
        {
            Dropdown.OptionData data = new()
            {
                text = frameRate[i].ToString()
            };
            frame.options.Add(data);

            if (Application.targetFrameRate == frameRate[i])
            {
                frame.value = i;
                break;
            }
        }
        frame.RefreshShownValue();

        //화면모드 옵션 초기화
        screenMode.options.Clear();
        Dropdown.OptionData data1 = new()
        {
            text = "전체화면"
        };
        Dropdown.OptionData data2 = new()
        {
            text = "테두리 없는 창모드"
        };
        Dropdown.OptionData data3 = new()
        {
            text = "이건 뭐지"
        };
        Dropdown.OptionData data4 = new()
        {
            text = "창모드"
        };
        screenMode.options.Add(data1);
        screenMode.options.Add(data2);
        screenMode.options.Add(data3);
        screenMode.options.Add(data4);

        screenMode.value = (int)Screen.fullScreenMode;
        screenMode.RefreshShownValue();

        //안티 에일리어싱
        anti_Aliasing.options.Clear();
        if (Camera.main.TryGetComponent(out UniversalAdditionalCameraData cameraData))
        {
            for (int i = 0; i < 4; i++)
            {
                Dropdown.OptionData anti = new() { text = ((AntialiasingMode)i).ToString() };
                anti_Aliasing.options.Add(anti);
                if (cameraData.antialiasing == (AntialiasingMode)i)
                {
                    anti_Aliasing.value = i;
                    break;
                }
            }
        }
        else
        {
            anti_Aliasing.gameObject.SetActive(false);
        }
        anti_Aliasing.RefreshShownValue();
    }

    public void Refresh()
    {
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].width && Screen.height == resolutions[i].height)
            {
                resolution.value = i;
                break;
            }
        }
        for (int i = 0; i < frameRate.Count; i++)
        {
            if (Application.targetFrameRate == frameRate[i])
            {
                frame.value = i;
                break;
            }
        }
        screenMode.value = (int)Screen.fullScreenMode;

        UniversalAdditionalCameraData cameraData = Camera.main.GetComponent<UniversalAdditionalCameraData>();
        for (int i = 0; i <= 4; i++)
        {
            if (cameraData.antialiasing == (AntialiasingMode)i)
            {
                anti_Aliasing.value = i;
                break;
            }
        }
    }

    public void Apply(Resolution resolution, int frame, FullScreenMode fullScreenMode, AntialiasingMode antialiasingMode)
    {
        Screen.SetResolution(resolution.width, resolution.height, fullScreenMode, resolution.refreshRateRatio);

        Application.targetFrameRate = frame;

        UniversalAdditionalCameraData cameraData = Camera.main.GetComponent<UniversalAdditionalCameraData>();
        cameraData.antialiasing = antialiasingMode;

        Refresh();
    }

    public void OnButtonResolution()
    {
        Screen.SetResolution(resolutions[resolution.value].width, resolutions[resolution.value].height, (FullScreenMode)screenMode.value, resolutions[resolution.value].refreshRateRatio);
        ResolutionHandler?.Invoke(resolutions[resolution.value]);
    }
    public void OnButtonFrame()
    {
        Application.targetFrameRate = frameRate[frame.value];
        FrameHandler?.Invoke(frameRate[frame.value]);
    }
    public void OnButtonScreenMode()
    {
        Screen.SetResolution(resolutions[resolution.value].width, resolutions[resolution.value].height, (FullScreenMode)screenMode.value, resolutions[resolution.value].refreshRateRatio);
        ScreenModeHandler?.Invoke((FullScreenMode)screenMode.value);
    }
    public void OnButtonAnti_Aliasing()
    {
        UniversalAdditionalCameraData cameraData = Camera.main.GetComponent<UniversalAdditionalCameraData>();
        cameraData.antialiasing = (AntialiasingMode)anti_Aliasing.value;
        Anti_AliasingHandler?.Invoke((AntialiasingMode)anti_Aliasing.value);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
