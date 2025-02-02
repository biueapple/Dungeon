using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualCard
{
    public IndividualCard(string str)
    {
        CreateCard(str);
    }

    public void CreateCard(string str)
    {
        Card card = Object.Instantiate(Resources.Load<Card>("Card/" + str), GameManager.Instance.Dummy.transform);
        //카드가 지금 손패에 있는 상태임을 넣고
        card.State = new HandState_Card(card);
        //활성화
        card.gameObject.SetActive(true);
        //손패에 추가
        GameManager.Instance.Hand.AddCard(card);
    }
}
