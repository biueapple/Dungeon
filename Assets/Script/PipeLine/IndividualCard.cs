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
        //ī�尡 ���� ���п� �ִ� �������� �ְ�
        card.State = new HandState_Card(card);
        //Ȱ��ȭ
        card.gameObject.SetActive(true);
        //���п� �߰�
        GameManager.Instance.Hand.AddCard(card);
    }
}
