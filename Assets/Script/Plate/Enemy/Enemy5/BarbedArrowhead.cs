using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbedArrowhead : Plate
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
            ("랜덤한 모험가에게 <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " 의 대미지를 주고 부상을 1턴간 입힌다.");
    }
    //방어기능
    public override void Defense(){}
    //공격기능
    public override void Attack()
    {
        //대미지 설정
        float damage = keywords[0].Word;
        //대미지가 0이면 공격을 수행한것이 아님
        if (damage == 0)
            return;
        int index = Random.Range(0, GameManager.Instance.Adventurer.Count);

        List<AttackInfo> list = new()
        {
            new AttackInfo(GameManager.Instance.Adventurer[index], damage)
        };

        //부상
        GameManager.Instance.Adventurer[index].PCB.Add(new InjuryCreator(GameManager.Instance.Adventurer[index], 1));

        //공격을 수행한 캐릭터에 속함
        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, list, null));
    }
}
