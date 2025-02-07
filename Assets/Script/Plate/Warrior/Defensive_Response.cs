using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defensive_Response : Plate
{
    protected override void Init()
    {
        //키워드가 몇개고 기본값이 얼마인지
        keywords = new Keyword[2];
        keywords[0] = new Keyword(0, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
        keywords[1] = new Keyword(2, tool[1]);
        keywords[1].Conditions.Add(new Limit_Keyword(keywords[1], 0, 30));
    }

    //텍스트
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("몬스터들에게 <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " 피해를 입히고 모든 모험가들이 보호막을 <color=#C54646><link=word>" + keywords[1].Word + "</link></color> 얻는다.");
    }

    //방어기능
    public override void Defense()
    {
        //공격을 하지 않았거나 얻을 보호막이 0이라면 수행하지 않음
        if (keywords[1].Word == 0 || keywords[0].Word == 0)
            return;
        List<DefenceInfo> defenceInfos = new ();
        for(int i = 0; i < GameManager.Instance.Adventurer.Count; i++)
        {
            defenceInfos.Add(new DefenceInfo(GameManager.Instance.Adventurer[i], 0, keywords[1].Word));
        }
        //방어를 수행한 캐릭터에 속함
        GameManager.Instance.CyclePipeLine.Defence.Fulfillment.Add(new PL_ShieldNHeal(self, defenceInfos, null));
    }

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
