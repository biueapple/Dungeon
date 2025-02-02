using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System;

public class LoadSaveManager
{
    private static LoadSaveManager instance;
    public static LoadSaveManager Instance
    {
        get
        {
            instance ??= new LoadSaveManager();
            return instance;
        }
    }
    //저장 경로 : C:\Users\사용자명\AppData\LocalLow\DefaultCompany\...
    public void SaveJson<T>(T data, string path)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, path);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(fullPath, json);
    }

    //불러오기 경로 : C:\Users\사용자명\AppData\LocalLow\DefaultCompany\...
    public bool LoadJson<T>(ref T t, string path)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, path);
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            t = JsonUtility.FromJson<T>(json);
            return true;
        }
        return false;
    }
}
