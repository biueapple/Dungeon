using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    //�������ִ� Ŭ����
    [SerializeField]
    private Sorting sorting;
    public Sorting Sorting => sorting;

    //���п� �ִ� ī���
    private readonly List<Card> cards = new ();
    public List<Card> Cards { get { return cards; } }

    //������ ��ü�� ũ��� �� ������ ����
    private void Start()
    {
        sorting.Size = 150;
        sorting.Padding = 10;
    }

    //���п� ī�� �߰�
    public void AddCard(Card card)
    {
        //����Ʈ�� �߰��ϰ�
        cards.Add(card);
        //�������ֱ�
        sorting.Add(card.transform);
    }

    //���п� ī�� ����
    public void RemoveCard(Card card)
    {
        //����Ʈ���� ���ְ�
        cards.Remove(card);
        //�������ֱ�
        sorting.Remove(card.transform);
    }
}
