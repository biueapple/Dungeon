using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardBox : MonoBehaviour , IPointerDownHandler
{
    //���ӿ� ������ �ִ� ī���
    protected Card[] cards;
    public Card[] Card { get { return cards; } }
    //�����Ҷ� ������
    protected Deck deck;

    //�ڽ��� ũ�� (�����Ҷ� �����)
    protected float size;
    public float Size { get => size; }
    public void OnPointerDown(PointerEventData eventData)
    {
        //��Ŭ���ϸ� deck���� ���� ������
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            deck.OutputCard(this);
        }
    }
}
