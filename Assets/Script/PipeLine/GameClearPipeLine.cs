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
        //ó�� ȭ������ �̵��ϰ� ��� �͵��� �ʱ�ȭ
        //GameManager.Instance.GameInit();
        gamaclear.SetActive(true);
    }

    public void Update()
    {

    }
}
