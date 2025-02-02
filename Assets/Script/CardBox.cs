using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardBox : MonoBehaviour , IPointerDownHandler
{
    //몸속에 가지고 있는 카드들
    protected Card[] cards;
    public Card[] Card { get { return cards; } }
    //삭제할때 쓰려고
    protected Deck deck;

    //박스의 크기 (정렬할때 사용함)
    protected float size;
    public float Size { get => size; }
    public void OnPointerDown(PointerEventData eventData)
    {
        //우클릭하면 deck에서 빼서 삭제함
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            deck.OutputCard(this);
        }
    }
}
