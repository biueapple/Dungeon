using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPipeLine : IPipeLine
{
    private readonly GameObject gamavoer;
    public IPipeLine Next { get => null; set { } }

    public GameOverPipeLine(GameObject gamavoer)
    {
        this.gamavoer = gamavoer;
    }

    public void End()
    {

    }

    public void Start()
    {
        //ó�� ȭ������ �̵��ϰ� ��� �͵��� �ʱ�ȭ
        //GameManager.Instance.GameInit();
        gamavoer.SetActive(true);
    }

    public void Update()
    {

    }
}
