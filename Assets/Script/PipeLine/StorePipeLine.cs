using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorePipeLine : IPipeLine
{
    private IPipeLine next;
    public IPipeLine Next { get => next; set => next = value; }

    private readonly Store store;

    public StorePipeLine(IPipeLine next, Store store)
    {
        this.next = next;
        this.store = store;
        store.Display();
    }

    public void End()
    {
        store.gameObject.SetActive(false);
    }

    public void Start()
    {
        //���� ����� ������ ���������ΰ��� �ٸ� ���� �ɾ�� ��
        store.gameObject.SetActive(true);
    }

    public void Update()
    {

    }

    public void OnButtonEnd()
    {
        GameManager.Instance.PipeLine = next;
    }
}
