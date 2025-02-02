using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//ī�尡 2�� �� �ִ� ī��ڽ�
public class CardBoxTwo : CardBox
{
    //ī���� �̸��� ������ ������Ʈ
    [SerializeField]
    private TextMeshProUGUI text1;
    [SerializeField]
    private TextMeshProUGUI text2;

    //�ʱ�ȭ
    public void Init(Card card1, Card card2, Deck deck)
    {
        //�ǵ����� �� deck
        this.deck = deck;
        //�ڽ��� ũ��
        size = 50;
        //�������� ī����� ����
        cards = new Card[2];
        cards[0] = card1;
        cards[1] = card2;
        //�̸� �����ֱ�
        text1.text = card1.name; 
        text2.text = card2.name;
    }
}
