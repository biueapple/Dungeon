using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowOfMalice : Plate
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
            ("가장 앞의 모험가에게 <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " 의 대미지를 주고 <color=#C54646><link=word>" + keywords[1].Word + "</link></color>" +
            " 의 대미지를 2턴간 주는 출혈을 부여한다.");
    }

    //방어기능
    public override void Defense()
    {
    }

    //공격기능
    public override void Attack()
    {
        //대미지 설정
        float damage = keywords[0].Word;
        //대미지가 0이면 공격을 수행한것이 아님
        if (damage == 0)
            return;

        List<AttackInfo> list = new()
        {
            new AttackInfo(GameManager.Instance.Adventurer[0], damage)
        };

        if (GameManager.Instance.Adventurer.Count > 0)
        {
            //출혈 부여
            GameManager.Instance.Adventurer[0].PCB.Add(new BleedingCreator(GameManager.Instance.Adventurer[0], keywords[1].Word, 2));
        }

        //공격을 수행한 캐릭터에 속함
        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, list, SoundManager.Instance.Sword));
    }
}
