using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//캐릭터 한명이 가지고 있는 덱
public class CharacterDeck
{
    //덱의 주인
    private readonly Character owner;
    public Character Owner { get { return owner; } }
    //쓰지 않은거
    private readonly List<Card> before = new();
    public List<Card> Before { get { return before; } }
    //쓴거
    private readonly List<Card> after = new();
    public List<Card> After { get { return after; } }
    //덱의 전체 카드 (사용 전 + 사용 후)
    public List<Card> Cards { get { return before.Concat(after).ToList(); } }

    public CharacterDeck(Character owner)
    {
        this.owner = owner;
    }

    //사용하지 않은 카드가 없다면 사용한 카드를 모두 옮기고 섞기
    public void Refill()
    {
        if (before.Count == 0)
        {
            before.AddRange(after);
            UIManager.Instance.Shuffle(before);
            after.Clear();
        }
    }

    //덱에서 카드 빼기
    public void Remove(Card card)
    {
        before.Remove(card);
        after.Remove(card);
    }
}

//모든 캐릭터의 덱을 관리 (카드 오브젝트들은 이 오브젝트 아래에 있음)
public class Dummy : MonoBehaviour
{
    //캐릭터마다의 덱
    private readonly List<CharacterDeck> decks = new();
    //살아있는 모험가들의 덱
    public List<CharacterDeck> Decks
    {
        get { return decks.Where(d => d.Owner != null && GameManager.Instance.Adventurer.Contains(d.Owner)).ToList(); }
    }
    //모든 덱의 전체 카드
    public List<Card> Cards { get { return Decks.SelectMany(d => d.Cards).ToList(); } }

    //드로우를 할때 이동할 경로를 설정해줌
    [SerializeField]
    private Bazier bazier;
    //플레이어의 손패
    [SerializeField]
    private Hand hand;

    //주인을 정하지 않고 뽑을때 다음에 뽑을 덱
    private int nextDeck = 0;

    //캐릭터의 덱
    public CharacterDeck DeckOf(Character character)
    {
        return decks.FirstOrDefault(d => d.Owner == character);
    }

    //모험가들의 시작 카드로 덱을 만들면서 초기화 하는 단계
    public void Init(List<Character> adventurers)
    {
        for (int i = 0; i < adventurers.Count; i++)
        {
            CharacterDeck deck = new(adventurers[i]);
            decks.Add(deck);
            string[] cards = adventurers[i].StartingCards;
            for (int c = 0; c < cards.Length; c++)
            {
                Card prefab = Resources.Load<Card>("Card/" + cards[c]);
                if (prefab == null)
                {
                    Debug.Log("시작 카드를 찾을 수 없음 : " + cards[c]);
                    continue;
                }
                //카드 생성
                Card card = Instantiate(prefab, new Vector2(), Quaternion.identity, transform);
                card.Deck = deck;
                //비활성화
                card.gameObject.SetActive(false);
                //아직 사용하지 않은 카드 리스트에 추가
                deck.Before.Add(card);
            }
            //덱을 섞기
            UIManager.Instance.Shuffle(deck.Before);
        }
        nextDeck = 0;
    }

    //모든 덱과 카드 삭제
    public void Clear()
    {
        for (int i = 0; i < decks.Count; i++)
        {
            decks[i].Cards.ForEach(c => Destroy(c.gameObject));
        }
        decks.Clear();
    }

    //새 카드를 캐릭터의 덱에 추가 (사용한 카드 리스트에 넣어서 다음에 덱을 섞을 때 들어오도록)
    public void AddCard(Card card, Character owner)
    {
        card.Deck = DeckOf(owner);
        card.transform.SetParent(transform, false);
        AfterCard(card);
    }

    //덱에서 카드 빼기 (상점에서 비용으로 낸 카드 등)
    public void Remove(Card card)
    {
        card.Deck?.Remove(card);
    }

    //카드를 사용했을경우
    public void AfterCard(Card card)
    {
        //비활성화
        card.gameObject.SetActive(false);
        //주인이 없는 카드(전투중에 생성된 카드 등)는 가장 앞의 모험가 덱으로
        CharacterDeck deck = card.Deck ?? Decks.FirstOrDefault();
        if (card.Volatility || deck == null)
        {
            //휘발성 카드라면 삭제
            Destroy(card.gameObject);
        }
        else
        {
            card.Deck = deck;
            //사용한 리스트에 추가
            deck.After.Add(card);
            //before.remove가 없는 이유는 이미 드로우 단계에서 remove하기때문
        }
    }

    //드로우시 카드가 이동하는 연출과 도착했을 때 손패에 추가
    //from : 이 캐릭터의 덱에서 뽑음 (null이거나 카드가 없다면 돌아가면서 다른 덱에서)
    public IEnumerator Draw(Character from = null)
    {
        CharacterDeck deck = PickDeck(from);
        //뽑을 수 있는 카드가 하나도 없다면 뽑지 않음
        if (deck == null)
            yield break;

        //이동경로대로 이동
        while (bazier != null && bazier.Value < 1)
        {
            bazier.Value += Time.deltaTime * 2;
            yield return null;
        }

        //사용하지 않은 카드가 없다면 사용한 카드를 모두 옮기고 섞기
        deck.Refill();

        //이동경로 초기화
        if (bazier != null)
            bazier.Value = 0;

        Card card = deck.Before[0];
        //카드가 지금 손패에 있는 상태임을 넣고
        card.State = new HandState_Card(card);
        //활성화
        card.gameObject.SetActive(true);
        //손패에 추가
        hand.AddCard(card);
        //쓰지 않은 카드 리스트에서 빼기
        deck.Before.RemoveAt(0);
    }

    //뽑을 덱 고르기
    private CharacterDeck PickDeck(Character from)
    {
        CharacterDeck deck = from != null ? DeckOf(from) : null;
        if (deck != null && deck.Cards.Count > 0 && GameManager.Instance.Adventurer.Contains(from))
            return deck;
        //정해진 덱이 없다면 살아있는 모험가의 덱을 돌아가면서
        List<CharacterDeck> list = Decks;
        for (int i = 0; i < list.Count; i++)
        {
            deck = list[(nextDeck + i) % list.Count];
            if (deck.Cards.Count > 0)
            {
                nextDeck = (nextDeck + i + 1) % list.Count;
                return deck;
            }
        }
        return null;
    }
}
