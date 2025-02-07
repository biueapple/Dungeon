using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPipeLine : IPipeLine
{
    //다음 진행될 파이프라인
    private IPipeLine next;
    public IPipeLine Next { get { return next; } set { next = value; } }

    public PlayPipeLine(IPipeLine next)
    {
        this.next = next;
    }

    public void TurnEndButtonPush()
    {
        GameManager.Instance.PipeLine = next;
    }

    public void Start()
    {
        //턴 시작 콜백
        for (int i = GameManager.Instance.PCB.Count - 1; i >= 0; i--)
        {
            GameManager.Instance.PCB[i].TurnStart();
        }
    }

    public void Update() 
    {
        //만약 모험가의 남아있는 플레이트가 없다면 자동으로 모든 손패를 버리고 다음 단계로
        if (NonePlate())
        {
            for (int i = GameManager.Instance.Hand.Cards.Count - 1; i >= 0; i--)
            {
                GameManager.Instance.Dummy.AfterCard(GameManager.Instance.Hand.Cards[i]);
                GameManager.Instance.Hand.RemoveCard(GameManager.Instance.Hand.Cards[i]);
            }
            TurnEndButtonPush();
        }
        //만약 모험가의 모든 플레이트의 모든 키워드가 0이면서 손패에 minus밖에 없다면 다음 단계로
        if (PlateZero() && MinusHand())
        {
            for (int i = GameManager.Instance.Hand.Cards.Count - 1; i >= 0; i--)
            {
                GameManager.Instance.Dummy.AfterCard(GameManager.Instance.Hand.Cards[i]);
                GameManager.Instance.Hand.RemoveCard(GameManager.Instance.Hand.Cards[i]);
            }
            TurnEndButtonPush();
        }
    }

    public void End()
    {
        //턴 끝 콜백
        for (int i = GameManager.Instance.PCB.Count - 1; i >= 0; i--)
        {
            GameManager.Instance.PCB[i].TurnEnd();
        }
    }

    private bool NonePlate()
    {
        for (int i = 0; i < GameManager.Instance.Adventurer.Count; i++)
        {
            if (GameManager.Instance.Adventurer[i].Plate != null)
                return false;
        }
        return true;
    }

    private bool PlateZero()
    {
        for(int i = 0; i < GameManager.Instance.Adventurer.Count; i++)
        {
            if (GameManager.Instance.Adventurer[i].Plate != null)
            {
                for(int p = 0; i < GameManager.Instance.Adventurer[i].Plate.Keyword.Length; p++)
                {
                    if (GameManager.Instance.Adventurer[i].Plate.Keyword[p].Word != 0)
                        return false;
                }
            }
        }
        return true;
    }
    private bool MinusHand()
    {
        for (int i = 0; i < GameManager.Instance.Hand.Cards.Count; i++)
        {
            if (GameManager.Instance.Hand.Cards[i].Friendly && GameManager.Instance.Hand.Cards[i].Value >= 0)
                return false;
        }
        return true;
    }
}
