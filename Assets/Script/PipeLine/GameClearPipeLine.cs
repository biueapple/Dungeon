using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearPipeLine : IPipeLine
{
    private readonly GameObject gamaclear;
    public IPipeLine Next { get => null; set { } }

    public GameClearPipeLine(GameObject gamaclear)
    {
        this.gamaclear = gamaclear;
    }

    public void End()
    {

    }

    public void Start()
    {
        //처음 화면으로 이동하고 모든 것들을 초기화
        //GameManager.Instance.GameInit();
        gamaclear.SetActive(true);
    }

    public void Update()
    {

    }
}
