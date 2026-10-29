using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Nimble_Attack : Plate
{
    protected override void Init()
    {
        //공격 대상을 정할 수 있는 플레이트
        useTarget = true;
        //키워드가 몇개고 기본값이 얼마인지
        keywords = new Keyword[2];
        keywords[0] = new Keyword(1, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
        keywords[1] = new Keyword(2, tool[1]);
        keywords[1].Conditions.Add(new Limit_Keyword(keywords[1], 0, 30));
    }

    //텍스트
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            (TargetText + "에게 <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " 피해를 <color=#C54646><link=word>" + keywords[1].Word + "</link></color> 회 입힌다.");
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

        //keyword[1]회 만큼 반복
        for (int i = 0; i < keywords[1].Word; i++)
        {
            List<AttackInfo> list = new();
            //플레이트에 지정된 대상에게 공격
            Character target = Target;
            if (target == null)
                return;
            list.Add(new AttackInfo(target, damage));
            //공격을 수행한 캐릭터에 속함
            GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, list, SoundManager.Instance.Sword));
        }
    }
}
