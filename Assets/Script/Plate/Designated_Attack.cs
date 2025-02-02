using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Designated_Attack : Plate
{
    protected override void Init()
    {
        //키워드가 몇개고 기본값이 얼마인지
        keywords = new Keyword[2];
        keywords[0] = new Keyword(3, tool[0]);
        keywords[0].Attributes.Add(new Circulation_Keyword(1, 3));
        keywords[1] = new Keyword(3, tool[1]);
        keywords[1].Conditions.Add(new Limit_Keyword(keywords[1], 0, 30));
    }

    //텍스트
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("상대 <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " 번째 적에게 <color=#C54646><link=word>" + keywords[1].Word + "</link></color> 대미지를 준다.");
    }

    //방어기능
    public override void Defense() { }

    //공격기능
    public override void Attack()
    {
        //keywords[0].Word 번째 적을 찾으며 없다면 공격하지 않음
        int index = keywords[0].Word - 1;
        if (index < foe.Count)
        {
            //대미지 설정
            float damage = keywords[0].Word;
            //대미지가 0이면 공격을 수행한것이 아님
            if (damage == 0)
                return;

            //공격을 수행한 캐릭터에 속함
            GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, new List<AttackInfo>() { new (foe[index], damage) }, SoundManager.Instance.Sword));
        }
    }
}
