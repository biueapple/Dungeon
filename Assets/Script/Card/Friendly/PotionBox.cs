using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//포션 상자 : 아군 캐릭터에게 사용하면 손패를 최대 3장까지 버리고 서로 다른 무작위 포션 3장을 가져옴
public class PotionBox : Card
{
    //버릴 수 있는 최대 장수
    private const int MaxDiscard = 3;
    //가져올 포션 수
    private const int PotionCount = 3;
    //포션 카드가 들어있는 폴더
    private const string PotionFolder = "Card/Potion";

    //카드의 가치
    public override int Worth { get { return 2; } }
    //마법사 전용 카드
    public override System.Type Job { get { return typeof(Wizard); } }

    private void Start()
    {
        value = 0;
        friendly = true;
        Description = "포션 상자\n손패를 최대 3장까지 버리고 서로 다른 무작위 포션 3장을 가져온다.";
    }

    public override void UpCharacter(Character character)
    {
        //아군(모험가)에게만 사용 가능
        if (character == null || !GameManager.Instance.Adventurer.Contains(character))
        {
            UpEmpty();
            return;
        }
        GameManager.Instance.Hand.RemoveCard(this);
        GameManager.Instance.Dummy.AfterCard(this);
        Zoom.Insatnce.Active = true;

        HandDiscard.Open(MaxDiscard, "버릴 카드를 고르세요", discard =>
        {
            //고른 카드들은 사용한 덱으로
            for (int i = 0; i < discard.Count; i++)
            {
                GameManager.Instance.Hand.RemoveCard(discard[i]);
                GameManager.Instance.Dummy.AfterCard(discard[i]);
            }
            //서로 겹치지 않게 무작위 포션
            List<Card> potions = Resources.LoadAll<Card>(PotionFolder).OrderBy(_ => Random.value).Take(PotionCount).ToList();
            for (int i = 0; i < potions.Count; i++)
            {
                new IndividualCard("Potion/" + potions[i].name);
            }
            GameManager.Instance.Hand.Sorting.Sort(null);
        });
    }
}
