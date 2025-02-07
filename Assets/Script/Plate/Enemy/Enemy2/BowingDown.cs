using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowingDown : Plate
{
    protected override void Init()
    {
        //키워드가 몇개고 기본값이 얼마인지
        keywords = new Keyword[2];
        keywords[0] = new Keyword(2, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
        keywords[1] = new Keyword(2, tool[1]);
        keywords[1].Conditions.Add(new Limit_Keyword(keywords[1], 0, 30));
    }

    //텍스트
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("자신에게 <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " 의 보호막을 준다. 모든 몬스터에게 <color=#C54646><link=word>" + keywords[1].Word + "</link></color>" +
            " 의 보호막을 준다.");
    }

    //방어기능
    public override void Defense()
    {
        List<DefenceInfo> defenceInfos = new();

        if (keywords[0].Word != 0)
        {
            defenceInfos.Add(new DefenceInfo(self, 0, keywords[0].Word));
        }
        if (keywords[1].Word != 0)
        {
            for (int i = 0; i < GameManager.Instance.Enemys.Count; i++)
            {
                defenceInfos.Add(new DefenceInfo(GameManager.Instance.Enemys[i], 0, keywords[1].Word));
            }
        }

        //방어를 수행한 캐릭터에 속함
        if (defenceInfos.Count > 0)
            GameManager.Instance.CyclePipeLine.Defence.Fulfillment.Add(new PL_ShieldNHeal(self, defenceInfos, null));
    }

    //공격기능
    public override void Attack() { }
}
