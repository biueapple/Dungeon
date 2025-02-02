using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPack : MonoBehaviour
{
    //팩이 가지고 있는 카드들
    [SerializeField]
    protected List<Card> cards = new ();
    public List<Card> Cards { get { return cards; } }

    //카드들을 정렬해야 함 (정렬 방식이 달라 sorting 클래스를 사용하지 않음)
    //카드가 원점으로부터 떨어진 거리
    [SerializeField]
    protected float range = 140;
    //카드끼리의 거리 (각도)
    [SerializeField]
    protected float bet = 20;

    protected void Start()
    {
        //자신이 가진 모든 카드의 상태는 팩속에 속한 상태임
        for (int i = 0; i < cards.Count; i++)
        {
            if(cards[i].State == null)
                cards[i].State = new PackInState_Card(this, cards[i]);
        }
        //정렬
        Sorting();
    }

    //정렬 (카드를 골랐다면 이동하고 다시 고르는 화면으로 돌아간다면 돌아가서 정렬 해야지)
    public void Sorting()
    {
        //카드는 중간을 기준으로 정렬해야 하니 절반정도는 뺀 상태로 정렬을 시작함
        float minus = (cards.Count - 1) * bet * 0.5f;
        //카드 수만큼 반복
        for (int i = 0; i < cards.Count; i++)
        {
            //각도 계산
            float angle = ((i * bet) - minus + 90) * Mathf.Deg2Rad;
            //위치계산
            float x = Mathf.Cos(angle) * range;
            float y = Mathf.Sin(angle) * range;
            //카드의 위치를 정해주기 (y값에 - range를 한 이유는 서로 떨어진 거리와 각도가 서로 맞지 않아 y값을 조절해 문제를 해결함)
            cards[i].transform.localPosition = new Vector3(x, y - range, 0);
            //각도도 변경 (자연스럽게)
            cards[i].transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg - 90);
        }
    }

}
