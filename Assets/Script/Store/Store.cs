using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField]
    private Relic[] relicResources;
    //자리는 3개
    //카드팩
    [SerializeField]
    private Sorting pack;
    //렉릭
    [SerializeField]
    private Sorting relic;
    //플레이트 강화 (플라이트 강화는 한개만)
    [SerializeField]
    private Transform plate;

    //물건들을 만들어서 진열 해놓기
    public void Display()
    {
        //이전에 만들었던 물건들을 없애기
        for(int i = relic.List.Count - 1; i >= 0; i--)
        {
            Transform transform = relic.List[i];
            Destroy(transform.gameObject);
            relic.Remove(transform);
        }

        //pack 최대 2개
        //pack.Size = 200;
        //for (int i = 0; i < parent.childCount; i++)
        //{
        //    cardPacks.Add(parent.GetChild(i).GetComponent<CardPack>());
        //    cardPacks[^1].Cards.ForEach(c => c.State = new StoreInState_Card(c, cardPacks[^1], this, 20));
        //}


        //relic 최대 2 개씩
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
