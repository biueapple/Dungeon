using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPipeLine : IPipeLine
{
    //���� ����� ����������
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
        //�� ���� �ݹ�
        for (int i = GameManager.Instance.PCB.Count - 1; i >= 0; i--)
        {
            GameManager.Instance.PCB[i].TurnStart();
        }
    }

    public void Update() 
    {
        //���� ���谡�� �����ִ� �÷���Ʈ�� ���ٸ� �ڵ����� ��� ���и� ������ ���� �ܰ��
        if (NonePlate())
        {
            for (int i = GameManager.Instance.Hand.Cards.Count - 1; i >= 0; i--)
            {
                GameManager.Instance.Dummy.AfterCard(GameManager.Instance.Hand.Cards[i]);
                GameManager.Instance.Hand.RemoveCard(GameManager.Instance.Hand.Cards[i]);
            }
            TurnEndButtonPush();
        }
        //���� ���谡�� ��� �÷���Ʈ�� ��� Ű���尡 0�̸鼭 ���п� minus�ۿ� ���ٸ� ���� �ܰ��
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
        //�� �� �ݹ�
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
