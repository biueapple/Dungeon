using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weak_Attack : Plate
{
    protected override void Init()
    {
        //키워드가 몇개고 기본값이 얼마인지
        keywords = new Keyword[1];
        keywords[0] = new Keyword(5, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
    }

    //텍스트
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("몬스터들에게 <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " 대미지를 준다. 보호막에게는 절반의 대미지만 준다.");
    }

    //방어기능
    public override void Defense() { }

    //공격기능
    public override void Attack()
    {
        //대미지 설정
        float damage = keywords[0].Word;
        if (damage == 0)
            return;

        List<AttackInfo> list = new();

        //남아있는 적의 수
        switch (GameManager.Instance.Enemys.Count)
        {
            //공격을 수행
            case 1:
                {
                    list.Add(new AttackInfo(GameManager.Instance.Enemys[0], damage));
                }
                break;
            case 2:
                {
                    list.Add(new AttackInfo(GameManager.Instance.Enemys[0], damage * 0.7f));

                    list.Add(new AttackInfo(GameManager.Instance.Enemys[1], damage * 0.3f));
                }
                break;
            case 3:
                {
                    list.Add(new AttackInfo(GameManager.Instance.Enemys[0], damage * 0.6f));

                    list.Add(new AttackInfo(GameManager.Instance.Enemys[1], damage * 0.3f));

                    list.Add(new AttackInfo(GameManager.Instance.Enemys[2], damage * 0.1f));
                }
                break;
            default:
                return;
        }
        //이 공격은 쉴드에게 절반의 피해만 입힌다는 클래스
        PL_AttackNHit pl = new(self, list, SoundManager.Instance.Fire)
        {
            ShieldChange = new ShieldHalfChange(),
        };
        //공격을 수행한 캐릭터에 속함
        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(pl);
    }
}
