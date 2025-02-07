using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter_Attack : Plate
{
    protected override void Init()
    {
        //키워드가 몇개고 기본값이 얼마인지
        keywords = new Keyword[1];
        keywords[0] = new Keyword(8, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
    }

    //텍스트
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("자신에게 보호막을 <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " 획득하며 수비 후 남은 보호막만큼 몬스터들에게 피해를 입힌다.");
    }

    //방어기능
    public override void Defense()
    {
        //보호막 획득
        //만약 얻는 방어력이 0이라면 방어를 수행한것이 아님
        if (keywords[0].Word == 0)
            return;
        List<DefenceInfo> defenceInfos = new ()
        {
            new DefenceInfo(self, 0, keywords[0].Word)
        };
        //방어를 수행한 캐릭터에 속함
        GameManager.Instance.CyclePipeLine.Defence.Fulfillment.Add(new PL_ShieldNHeal(self, defenceInfos, null));
    }

    //공격기능
    public override void Attack()
    {
        //대미지 설정
        float damage = self.Shield;
        //대미지가 0이면 공격을 수행한것이 아님
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
        //공격을 수행한 캐릭터에 속함
        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, list, SoundManager.Instance.Sword));
    }
}
