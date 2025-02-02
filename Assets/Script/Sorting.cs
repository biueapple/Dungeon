using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//transform���� �������ִ� Ŭ����
public class Sorting : MonoBehaviour
{
    //���� �ҼӵǾ� �ִ� transfrom��
    private readonly List<Transform> list = new();
    public List<Transform> List { get => list; }

    //���� �ҼӵǾ� �ִ� transform�� ����
    public int Count { get { return list.Count; } }

    //�ҼӵǾ� �ִ� transform�� ũ��
    private float size;
    public float Size { get => size; set => size = value; }
    //transform������ ����
    private float padding;
    public float Padding { get => padding; set => padding = value; }
    //Ȥ�� ���� �� callback����� �ϴ°� �ִٸ� ȣ������
    private Action<Transform> action = null;
    public Action<Transform> Action { get => action; set => action = value; }

    //����
    public void Sort(Transform tf)
    {
        float average = (size + padding) * 0.5f;
        float minus = list.Count * -average + average;
        //ī�� ��ġ�� �����ֱ�
        for (int i = 0; i < list.Count; i++)
        {
            list[i].position = transform.position + new Vector3(minus + i * (size + padding), 0, 0);
        }
        action?.Invoke(tf);
    }
    //����
    public void Remove(Transform tf)
    {
        list.Remove(tf);
        Sort(tf);
    }
    //�߰�
    public void Add(Transform tf)
    {
        list.Add(tf);
        Sort(tf);
    }
}
