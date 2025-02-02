using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    //���� ������ 
    private readonly List<Card> before = new();
    public List<Card> Before { get { return before; } }
    //����
    private readonly List<Card> after = new();
    public List<Card> After { get { return after; } }

    //��ο츦 �Ҷ� �̵��� ��θ� ��������
    [SerializeField]
    private Bazier bazier;
    //�÷��̾��� ����
    [SerializeField]
    private Hand hand;

    //å���� �� ī���ѿ� ��� ī����� �����ϸ鼭 �ʱ�ȭ �ϴ� �ܰ�
    public void Init(List<Card> cards)
    {
        for(int i = 0; i < cards.Count; i++)
        {
            //ī�� ����
            Card card = Instantiate(cards[i], new Vector2(), Quaternion.identity, transform);
            //��Ȱ��ȭ
            card.gameObject.SetActive(false);
            //���� ������� ���� ī�� ����Ʈ�� �߰�
            before.Add(card);
            //���� ����
            UIManager.Instance.Shuffle(before);
        }
    }

    //ī�带 ����������
    public void AfterCard(Card card)
    {
        //��Ȱ��ȭ
        card.gameObject.SetActive(false);
        if (card.Volatility)
        {
            //�ֹ߼� ī���� ����
            Destroy(card.gameObject);
        }
        else
        {
            //����� ����Ʈ�� �߰�
            after.Add(card);
            //before.remove�� ���� ������ �̹� ��ο� �ܰ迡�� remove�ϱ⶧��
        }
    }

    //��ο�� ī�尡 �̵��ϴ� ����� �������� �� ���п� �߰�
    public IEnumerator Draw()
    {
        //�̵���δ�� �̵�
        while (bazier != null && bazier.Value < 1)
        {
            bazier.Value += Time.deltaTime * 2;
            yield return null;
        }

        //������� ���� ī�尡 ���ٸ� ����� ī�带 ��� �ű�� ����
        if(before.Count == 0)
        {
            before.AddRange(after);
            UIManager.Instance.Shuffle(before);
            after.Clear();
        }

        //�̵���� �ʱ�ȭ
        bazier.Value = 0;
        
        //ī�尡 ���� ���п� �ִ� �������� �ְ�
        before[0].State = new HandState_Card(before[0]);
        //Ȱ��ȭ
        before[0].gameObject.SetActive(true);
        //���п� �߰�
        hand.AddCard(before[0]);
        //���� ���� ī�� ����Ʈ���� ����
        before.RemoveAt(0);
    }

}
