using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recovery : Plate
{
    protected override void Init()
    {
        //키워드가 몇개고 기본값이 얼마인지
        keywords = new Keyword[2];
        keywords[0] = new Keyword(3, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
        keywords[1] = new Keyword(0, tool[1]);
        keywords[1].Conditions.Add(new Limit_Keyword(keywords[1], 0, 30));
    }

    //텍스트
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("가장 앞의 모험가에게 <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " 의 체력을 회복시켜준다. 가장 앞에 몬스터에게 <color=#C54646><link=word>" + keywords[1].Word + "</link></color>" +
            " 의 대미지를 준다");
    }

    //방어기능
    public override void Defense() 
    {
        if (keywords[0].Word == 0)
            return;
        List<DefenceInfo> defenceInfos = new()
        {
            new DefenceInfo(GameManager.Instance.Adventurer[0], keywords[0].Word, 0)
        };
        //방어를 수행한 캐릭터에 속함
        GameManager.Instance.CyclePipeLine.Defence.Fulfillment.Add(new PL_ShieldNHeal(self, defenceInfos, SoundManager.Instance.Recovery));
    }

    //공격기능
    public override void Attack() 
    {
        if(keywords[1].Word == 0)
            return;

        List<AttackInfo> attackInfos = new()
        {
            new AttackInfo(GameManager.Instance.Enemys[0], keywords[1].Word)
        };

        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, attackInfos, SoundManager.Instance.Recovery));
    }
}
