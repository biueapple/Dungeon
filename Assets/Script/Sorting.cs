using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//transform들을 정렬해주는 클래스
public class Sorting : MonoBehaviour
{
    //현재 소속되어 있는 transfrom들
    private readonly List<Transform> list = new();
    public List<Transform> List { get => list; }

    //현재 소속되어 있는 transform의 숫자
    public int Count { get { return list.Count; } }

    //소속되어 있는 transform의 크기
    private float size;
    public float Size { get => size; set => size = value; }
    //transform끼리의 간격
    private float padding;
    public float Padding { get => padding; set => padding = value; }
    //혹시 정렬 후 callback해줘야 하는게 있다면 호출해줌
    private Action<Transform> action = null;
    public Action<Transform> Action { get => action; set => action = value; }

    //정렬
    public void Sort(Transform tf)
    {
        float average = (size + padding) * 0.5f;
        float minus = list.Count * -average + average;
        //카드 위치를 정해주기
        for (int i = 0; i < list.Count; i++)
        {
            list[i].position = transform.position + new Vector3(minus + i * (size + padding), 0, 0);
        }
        action?.Invoke(tf);
    }
    //제거
    public void Remove(Transform tf)
    {
        list.Remove(tf);
        Sort(tf);
    }
    //추가
    public void Add(Transform tf)
    {
        list.Add(tf);
        Sort(tf);
    }
}
