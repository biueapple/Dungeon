using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittingDown : Plate
{
    protected override void Init()
    {
        //키워드가 몇개고 기본값이 얼마인지
        keywords = new Keyword[2];
        keywords[0] = new Keyword(4, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
        keywords[1] = new Keyword(1, tool[1]);
        keywords[1].Conditions.Add(new Limit_Keyword(keywords[1], 0, 30));
    }

    //텍스트
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("모험가들에게 <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " 의 대미지를 주고 손패에 휘발성 모험가 -1카드를 <color=#C54646><link=word>" + keywords[1].Word + "</link></color>" +
            " 장 생성한다.");
    }

    //방어기능
    public override void Defense() { }

    //공격기능
    public override void Attack()
    {
        //대미지 설정
        float damage = keywords[0].Word;
        //대미지가 0이면 공격을 수행한것이 아님
        if (damage == 0)
            return;

        List<AttackInfo> list = new();

        //남아있는 적의 수
        switch (GameManager.Instance.Adventurer.Count)
        {
            //공격을 수행
            case 1:
                {
                    list.Add(new AttackInfo(GameManager.Instance.Adventurer[0], damage));
                }
                break;
            case 2:
                {
                    list.Add(new AttackInfo(GameManager.Instance.Adventurer[0], damage * 0.7f));

                    list.Add(new AttackInfo(GameManager.Instance.Adventurer[1], damage * 0.3f));
                }
                break;
            case 3:
                {
                    list.Add(new AttackInfo(GameManager.Instance.Adventurer[0], damage * 0.6f));

                    list.Add(new AttackInfo(GameManager.Instance.Adventurer[1], damage * 0.3f));

                    list.Add(new AttackInfo(GameManager.Instance.Adventurer[2], damage * 0.1f));
                }
                break;
            default:
                return;
        }

        //카드 추가
        for (int i = 0; i < keywords[1].Word; i++)
        {
            new IndividualCard("Adventurer/VolatilityMinus");
        }

        //공격을 수행한 캐릭터에 속함
        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, list, SoundManager.Instance.Sword));
    }
}
