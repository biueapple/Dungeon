using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorePipeLine : IPipeLine
{
    private IPipeLine next;
    public IPipeLine Next { get => next; set => next = value; }

    private readonly GameObject store;
    private readonly Sorting relicSorting;
    private readonly Button button;
    private readonly List<CardPack> cardPacks = new();
    public List<CardPack> CardPacks { get { return cardPacks; } }

    public StorePipeLine(IPipeLine next, GameObject store, PowerRelic powerRelic, DefenseRelic defenseRelic)
    {
        this.next = next;
        this.store = store;
        relicSorting = store.transform.GetChild(1).GetComponent<Sorting>();
        relicSorting.Size = 100;

        Transform parent = store.transform.GetChild(2);
        for (int i = 0; i < parent.childCount; i++)
        {
            cardPacks.Add(parent.GetChild(i).GetComponent<CardPack>());
            cardPacks[^1].Cards.ForEach(c => c.State = new StoreInState_Card(c, cardPacks[^1], this, 20));
        }

        button = store.transform.GetChild(3).GetComponent<Button>();
        button.onClick.AddListener(OnButtonEnd);
        PowerRelic power = Object.Instantiate(powerRelic, relicSorting.transform);
        power.State = new RelicStore(power, power, 10);
        relicSorting.Add(power.transform);
        DefenseRelic defense = Object.Instantiate(defenseRelic, relicSorting.transform);
        defense.State = new RelicStore(defense, defense, 10);
        relicSorting.Add(defense.transform);
    }

    public void End()
    {
        store.SetActive(false);
    }

    public void Start()
    {
        //상점 만들고 기존의 파이프라인과는 다른 길을 걸어야 함
        store.SetActive(true);
    }

    public void Update()
    {

    }

    public void OnButtonEnd()
    {
        GameManager.Instance.PipeLine = next;
    }
}
