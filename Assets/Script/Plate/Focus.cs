using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Focus : Plate
{
    protected override void Init()
    {
        //키워드가 몇개고 기본값이 얼마인지
        keywords = new Keyword[1];
        keywords[0] = new Keyword(2, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
    }

    //텍스트
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("모든 아군이 <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " 만큼 보호막을 획득하며 다음 턴에 카드를 1장 더 뽑는다.");
    }

    //방어기능
    public override void Defense()
    {
        //만약 얻을 보호막이 없다면 뒤에 효과도 이뤄지지 않음
        if (keywords[0].Word == 0)
            return;
        //콜백 (추가드로우) 추가
        GameManager.Instance.PCB.Add(new AdditionalDraws());
        GameManager.Instance.PCB.Add(new ReductionDraws()); 
        List<DefenceInfo> defenceInfos = new();
        for (int i = 0; i < ally.Count; i++)
        {
            defenceInfos.Add(new DefenceInfo(ally[i], 0, keywords[0].Word));
        }
        //방어를 수행한 캐릭터에 속함
        GameManager.Instance.CyclePipeLine.Defence.Fulfillment.Add(new PL_ShieldNHeal(self, defenceInfos, SoundManager.Instance.Recovery));
    }

    //공격기능
    public override void Attack() { }
}
