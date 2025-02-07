using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

//책에 있던 카드팩들중 자신의 덱으로 사용할 것들을 보관하는 장소 (하스스톤의 덱만들때 오른쪽에 있는 덱 리스트같은거)
public class Deck : MonoBehaviour
{
    //카드가 2개 들어있는 카드박스
    [SerializeField]
    private CardBoxTwo prefab2;
    //카드가 3개 들어있는 카드박스 (현재까지 존재하지 않음)
    [SerializeField]
    private CardBoxThree prefab3;

    //카드팩을 정렬할 위치
    [SerializeField]
    private RectTransform contant;
    //게임시작버튼 (활성화의 조건이 있음)
    [SerializeField]
    private GameObject startButton;
    //지금 몇장 넣었는지 보여주기 위한 컴포넌트
    [SerializeField]
    private TextMeshProUGUI count;

    //정렬되어 있는 카드박스들
    private List<CardBox> cardBoxes;
    public List<CardBox> CardBoxes { get { return cardBoxes; } }
    //정렬의 위치
    private float y;

    private void Start()
    {
        cardBoxes = new ();
        y = 0;
    }

    //카드팩을 넣기
    public bool InputCard(CardPack pack)
    {
        //카드팩에 카드가 몇개짜리인지
        switch(pack.Cards.Count)
        {
            //2개짜리인 경우
            case 2:
                //두개짜리 카드팩 박스를 만들고
                CardBoxTwo two = Instantiate(prefab2, contant);
                //초기화 (무슨 카드를 가지고 있는지, 다시 빼기위해 deck도 참조해줘야 함)
                two.Init(pack.Cards[0], pack.Cards[1], this);
                //위치 조정
                two.transform.localPosition = new Vector3(0, y + contant.rect.yMax - two.Size, 0);
                //정렬해야 하는 위치 다시 맞춰주고
                y -= two.Size * 2;
                //리스트에 추가
                cardBoxes.Add(two);
                break;
            case 3:
                //두개짜리 카드팩 박스를 만들고
                CardBoxThree three = Instantiate(prefab3, contant);
                //초기화 (무슨 카드를 가지고 있는지, 다시 빼기위해 deck도 참조해줘야 함)
                three.Init(pack.Cards[0], pack.Cards[1], pack.Cards[2], this);
                //위치 조정
                three.transform.localPosition = new Vector3(0, y + contant.rect.yMax - three.Size, 0);
                //정렬해야 하는 위치 다시 맞춰주고
                y -= three.Size * 2;
                //리스트에 추가
                cardBoxes.Add(three);
                break;
            default:
                break;
        }

        //게임 시작 가능한지 체크
        StartCheck();
        return true;
    }

    //카드박스를 deck에서 빼내기
    public void OutputCard(CardBox box)
    {
        //몇번째 리스트인지
        int index = cardBoxes.IndexOf(box);
        //박스의 크기만큼 다시 정렬을 어디서 부터 해야하는지에 대한 높이를 조절하고
        y += box.Size * 2;
        //리스트에서 빼주고
        cardBoxes.RemoveAt(index);
        //박스 삭제
        Destroy(box.gameObject);
        //리스트 안의 빈공간을 채우고 다시 정렬
        for(int i = index; i < cardBoxes.Count; i++)
        {
            cardBoxes[i].transform.position += new Vector3(0, box.Size * 2, 0);
        }
        //시작 가능한지 체크
        StartCheck();
    }

    //모든 카드팩 빼기
    public void InitDeck()
    {
        for (int i = 0; i < cardBoxes.Count; ++i)
        {
            OutputCard(cardBoxes[i]);
        }
    }

    //시작 가능한지 확인
    private void StartCheck()
    {
        //모든 카드박스 안의 카드들의 숫자를 합쳐서
        int sum = cardBoxes.Sum(x => x.Card.Length);
        count.text = sum + " / 10";
        //10보다 높거나 같으면 게임시작 가능
        if (sum >= 10)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }
}
