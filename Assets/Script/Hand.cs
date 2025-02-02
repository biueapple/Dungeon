using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    //정렬해주는 클래스
    [SerializeField]
    private Sorting sorting;
    public Sorting Sorting => sorting;

    //손패에 있는 카드들
    private readonly List<Card> cards = new ();
    public List<Card> Cards { get { return cards; } }

    //정렬할 객체의 크기와 그 사이의 간격
    private void Start()
    {
        sorting.Size = 150;
        sorting.Padding = 10;
    }

    //손패에 카드 추가
    public void AddCard(Card card)
    {
        //리스트에 추가하고
        cards.Add(card);
        //정렬해주기
        sorting.Add(card.transform);
    }

    //손패에 카드 제거
    public void RemoveCard(Card card)
    {
        //리스트에서 없애고
        cards.Remove(card);
        //정렬해주기
        sorting.Remove(card.transform);
    }
}
