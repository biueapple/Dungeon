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
        //처음 화면으로 이동하고 모든 것들을 초기화
        //GameManager.Instance.GameInit();
        gamavoer.SetActive(true);
    }

    public void Update()
    {

    }
}
