using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPack : MonoBehaviour
{
    //���� ������ �ִ� ī���
    [SerializeField]
    protected List<Card> cards = new ();
    public List<Card> Cards { get { return cards; } }

    //ī����� �����ؾ� �� (���� ����� �޶� sorting Ŭ������ ������� ����)
    //ī�尡 �������κ��� ������ �Ÿ�
    [SerializeField]
    protected float range = 140;
    //ī�峢���� �Ÿ� (����)
    [SerializeField]
    protected float bet = 20;

    protected void Start()
    {
        //�ڽ��� ���� ��� ī���� ���´� �Ѽӿ� ���� ������
        for (int i = 0; i < cards.Count; i++)
        {
            if(cards[i].State == null)
                cards[i].State = new PackInState_Card(this, cards[i]);
        }
        //����
        Sorting();
    }

    //���� (ī�带 ����ٸ� �̵��ϰ� �ٽ� ���� ȭ������ ���ư��ٸ� ���ư��� ���� �ؾ���)
    public void Sorting()
    {
        //ī��� �߰��� �������� �����ؾ� �ϴ� ���������� �� ���·� ������ ������
        float minus = (cards.Count - 1) * bet * 0.5f;
        //ī�� ����ŭ �ݺ�
        for (int i = 0; i < cards.Count; i++)
        {
            //���� ���
            float angle = ((i * bet) - minus + 90) * Mathf.Deg2Rad;
            //��ġ���
            float x = Mathf.Cos(angle) * range;
            float y = Mathf.Sin(angle) * range;
            //ī���� ��ġ�� �����ֱ� (y���� - range�� �� ������ ���� ������ �Ÿ��� ������ ���� ���� �ʾ� y���� ������ ������ �ذ���)
            cards[i].transform.localPosition = new Vector3(x, y - range, 0);
            //������ ���� (�ڿ�������)
            cards[i].transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg - 90);
        }
    }

}
