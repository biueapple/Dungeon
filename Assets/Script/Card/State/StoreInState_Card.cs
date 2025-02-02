using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class StoreInState_Card : IState_Card
{
    //이 기능이 속한 카드
    private readonly Card card;
    private readonly CardPack pack;
    private readonly StorePipeLine pipeLine;
    private readonly int price;

    public StoreInState_Card(Card card, CardPack pack, StorePipeLine storePipeLine, int price)
    {
        this.card = card;
        this.pack = pack;
        pipeLine = storePipeLine;
        this.price = price;
    }

    public void Enter() { }
    public void Exit() { }
    //설명 보기
    public void OnPointerEnter()
    {
        Zoom.Insatnce.ViewON(card);
    }
    //설명 끄기
    public void OnPointerExit()
    {
        Zoom.Insatnce.ViewOFF();
    }


    public void OnPointerDown()
    {
        if(GameManager.Instance.Money >= price)
        {
            //상점에서 삭제 후 dummy에 추가
            pipeLine.CardPacks.Remove(pack);
            for (int i = 0; i < pack.Cards.Count; i++)
            {
                pack.Cards[i].transform.SetParent(GameManager.Instance.Dummy.transform);
                pack.Cards[i].gameObject.SetActive(false);
                GameManager.Instance.Dummy.Before.Add(pack.Cards[i]);
                pack.Cards[i].State = null;
                Zoom.Insatnce.InstructionOff();
                SoundManager.Instance.Play(SoundManager.Instance.Coin);
            }
            Object.Destroy(pack.gameObject);

            GameManager.Instance.Money -= price;
        }
        
    }

    public void OnPointerUp(){}
}
