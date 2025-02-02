using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GraphicSetting : MonoBehaviour
{
    //guideWindow�� ����
    [SerializeField]
    private Dropdown resolution;
    private List<Resolution> resolutions = new();
    public Resolution Defalut;
    [SerializeField]
    private Dropdown frame;
    private List<int> frameRate = new();
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
        //�ػ� �ɼ� �ʱ�ȭ
        resolution.options.Clear();
        // ���� �ػ� ����Ʈ ��������
        resolutions.AddRange(Screen.resolutions);

        // �ߺ��� �����ϰ� ���� ���� refresh rate�� ������ Dictionary ����
        Dictionary<string, Resolution> resolutionDict = new();

        foreach (var res in resolutions)
        {
            string key = res.width + "x" + res.height;

            // ���� ���� �ػ󵵰� �ִٸ� refreshRateRatio�� �� ���� ��츸 ����
            if (!resolutionDict.ContainsKey(key) || resolutionDict[key].refreshRateRatio.value < res.refreshRateRatio.value)
            {
                resolutionDict[key] = res;
            }
        }

        // Dictionary���� �ߺ� ���ŵ� �ػ� ����Ʈ�� ��ȯ
        resolutions = resolutionDict.Values.ToList();

        // Dropdown�� �߰�
        resolution.options.Clear();
        for (int i = 0; i < resolutions.Count; i++)
        {
            Dropdown.OptionData optionData = new()
            {
                text = $"{resolutions[i].width} X {resolutions[i].height} {(int)resolutions[i].refreshRateRatio.value}hz"
            };
            resolution.options.Add(optionData);

            // ���� ȭ�� �ػ󵵿� ��ġ�ϸ� �⺻ ���ð� ����
            if (Screen.width == resolutions[i].width && Screen.height == resolutions[i].height)
            {
                resolution.value = i;
            }
            if (resolutions[i].width == 1920 && resolutions[i].height == 1080)
            {
                Defalut = resolutions[i];
            }
        }
        resolution.RefreshShownValue();

        //������ �ɼ� �ʱ�ȭ
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

        //ȭ���� �ɼ� �ʱ�ȭ
        screenMode.options.Clear();
        Dropdown.OptionData data1 = new()
        {
            text = "��üȭ��"
        };
        Dropdown.OptionData data2 = new()
        {
            text = "�׵θ� ���� â���"
        };
        Dropdown.OptionData data3 = new()
        {
            text = "�̰� ����"
        };
        Dropdown.OptionData data4 = new()
        {
            text = "â���"
        };
        screenMode.options.Add(data1);
        screenMode.options.Add(data2);
        screenMode.options.Add(data3);
        screenMode.options.Add(data4);

        screenMode.value = (int)Screen.fullScreenMode;
        screenMode.RefreshShownValue();

        //��Ƽ ���ϸ����
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

        if (Camera.main.TryGetComponent(out UniversalAdditionalCameraData cameraData))
        {
            for (int i = 0; i <= 4; i++)
            {
                if (cameraData.antialiasing == (AntialiasingMode)i)
                {
                    anti_Aliasing.value = i;
                    break;
                }
            }
        }
    }

    public void Apply(Resolution resolution, int frame, FullScreenMode fullScreenMode, AntialiasingMode antialiasingMode)
    {
        Screen.SetResolution(resolution.width, resolution.height, fullScreenMode, resolution.refreshRateRatio);

        Application.targetFrameRate = frame;

        if(Camera.main.TryGetComponent(out UniversalAdditionalCameraData cameraData))
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
