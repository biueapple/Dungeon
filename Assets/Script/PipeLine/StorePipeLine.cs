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
    }

    public void End()
    {
        store.gameObject.SetActive(false);
    }

    public void Start()
    {
        //상점 만들고 기존의 파이프라인과는 다른 길을 걸어야 함
        //맵에서 상점에 들어올때마다 물건을 새로 진열
        store.Display();
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
