using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//카드가 2개 들어가 있는 카드박스
public class CardBoxTwo : CardBox
{
    //카드의 이름을 보여줄 컴포넌트
    [SerializeField]
    private TextMeshProUGUI text1;
    [SerializeField]
    private TextMeshProUGUI text2;

    //초기화
    public void Init(Card card1, Card card2, Deck deck)
    {
        //되돌릴때 쓸 deck
        this.deck = deck;
        //박스의 크기
        size = 50;
        //저장중인 카드들의 정보
        cards = new Card[2];
        cards[0] = card1;
        cards[1] = card2;
        //이름 보여주기
        text1.text = card1.name; 
        text2.text = card2.name;
    }
}
