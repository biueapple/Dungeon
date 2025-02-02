using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : Plate
{
    protected override void Init()
    {
        //키워드가 몇개고 기본값이 얼마인지
        keywords = new Keyword[1];
        keywords[0] = new Keyword(1, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
    }

    //텍스트
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("상대에게 <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " 피해를 동일하게 입힌다.");
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
        switch (foe.Count)
        {
            //공격을 수행
            case 1:
                {
                    list.Add(new AttackInfo(foe[0], damage));
                }
                break;
            case 2:
                {
                    list.Add(new AttackInfo(foe[0], damage));

                    list.Add(new AttackInfo(foe[1], damage));
                }
                break;
            case 3:
                {
                    list.Add(new AttackInfo(foe[0], damage));

                    list.Add(new AttackInfo(foe[1], damage));

                    list.Add(new AttackInfo(foe[2], damage));
                }
                break;
            default:
                return;
        }
        //공격을 수행한 캐릭터에 속함
        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, list, SoundManager.Instance.Fire));
    }
}
