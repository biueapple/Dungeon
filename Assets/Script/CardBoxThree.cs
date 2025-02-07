using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardBoxThree : CardBox
{
    //ī���� �̸��� ������ ������Ʈ
    [SerializeField]
    private TextMeshProUGUI text1;
    [SerializeField]
    private TextMeshProUGUI text2;
    [SerializeField]
    private TextMeshProUGUI text3;

    //�ʱ�ȭ
    public void Init(Card card1, Card card2, Card card3, Deck deck)
    {
        //�ǵ����� �� deck
        this.deck = deck;
        //�ڽ��� ũ��
        size = 80;
        //�������� ī����� ����
        cards = new Card[3];
        cards[0] = card1;
        cards[1] = card2;
        cards[2] = card3;
        //�̸� �����ֱ�
        text1.text = card1.name;
        text2.text = card2.name;
        text3.text = card3.name;
    }
}
