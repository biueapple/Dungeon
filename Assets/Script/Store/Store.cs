using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField]
    private Relic[] relicResources;
    //�ڸ��� 3��
    //ī����
    [SerializeField]
    private Sorting pack;
    //����
    [SerializeField]
    private Sorting relic;
    //�÷���Ʈ ��ȭ (�ö���Ʈ ��ȭ�� �Ѱ���)
    [SerializeField]
    private Transform plate;

    //���ǵ��� ���� ���� �س���
    public void Display()
    {
        //������ ������� ���ǵ��� ���ֱ�
        for(int i = relic.List.Count - 1; i >= 0; i--)
        {
            Transform transform = relic.List[i];
            Destroy(transform.gameObject);
            relic.Remove(transform);
        }

        //pack �ִ� 2��
        //pack.Size = 200;
        //for (int i = 0; i < parent.childCount; i++)
        //{
        //    cardPacks.Add(parent.GetChild(i).GetComponent<CardPack>());
        //    cardPacks[^1].Cards.ForEach(c => c.State = new StoreInState_Card(c, cardPacks[^1], this, 20));
        //}


        //relic �ִ� 2 ����
        relic.Size = 100;

        Relic r = Instantiate(relicResources[Random.Range(0, relicResources.Length)], relic.transform);
        r.State = new RelicStore(r, r, 10);
        relic.Add(r.transform);

        r = Instantiate(relicResources[Random.Range(0, relicResources.Length)], relic.transform);
        r.State = new RelicStore(r, r, 10);
        relic.Add(r.transform);
    }

    public void OnButtonNext()
    {
        GameManager.Instance.PipeLine = GameManager.Instance.PipeLine.Next;
    }
}
