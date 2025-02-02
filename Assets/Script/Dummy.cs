using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    //쓰지 않은거 
    private readonly List<Card> before = new();
    public List<Card> Before { get { return before; } }
    //쓴거
    private readonly List<Card> after = new();
    public List<Card> After { get { return after; } }

    //드로우를 할때 이동할 경로를 설정해줌
    [SerializeField]
    private Bazier bazier;
    //플레이어의 손패
    [SerializeField]
    private Hand hand;

    //책에서 고른 카드팩에 담긴 카드들을 생성하면서 초기화 하는 단계
    public void Init(List<Card> cards)
    {
        for(int i = 0; i < cards.Count; i++)
        {
            //카드 생성
            Card card = Instantiate(cards[i], new Vector2(), Quaternion.identity, transform);
            //비활성화
            card.gameObject.SetActive(false);
            //아직 사용하지 않은 카드 리스트에 추가
            before.Add(card);
            //덱을 섞기
            UIManager.Instance.Shuffle(before);
        }
    }

    //카드를 사용했을경우
    public void AfterCard(Card card)
    {
        //비활성화
        card.gameObject.SetActive(false);
        if (card.Volatility)
        {
            //휘발성 카드라면 삭제
            Destroy(card.gameObject);
        }
        else
        {
            //사용한 리스트에 추가
            after.Add(card);
            //before.remove가 없는 이유는 이미 드로우 단계에서 remove하기때문
        }
    }

    //드로우시 카드가 이동하는 연출과 도착했을 때 손패에 추가
    public IEnumerator Draw()
    {
        //이동경로대로 이동
        while (bazier != null && bazier.Value < 1)
        {
            bazier.Value += Time.deltaTime * 2;
            yield return null;
        }

        //사용하지 않은 카드가 없다면 사용한 카드를 모두 옮기고 섞기
        if(before.Count == 0)
        {
            before.AddRange(after);
            UIManager.Instance.Shuffle(before);
            after.Clear();
        }

        //이동경로 초기화
        bazier.Value = 0;
        
        //카드가 지금 손패에 있는 상태임을 넣고
        before[0].State = new HandState_Card(before[0]);
        //활성화
        before[0].gameObject.SetActive(true);
        //손패에 추가
        hand.AddCard(before[0]);
        //쓰지 않은 카드 리스트에서 빼기
        before.RemoveAt(0);
    }

}
