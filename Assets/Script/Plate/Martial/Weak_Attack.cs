using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weak_Attack : Plate
{
    protected override void Init()
    {
        //공격 대상을 정할 수 있는 플레이트
        useTarget = true;
        //키워드가 몇개고 기본값이 얼마인지
        keywords = new Keyword[1];
        keywords[0] = new Keyword(5, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
    }

    //텍스트
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            (TargetText + "에게 <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
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

        //플레이트에 지정된 대상에게 공격
        Character target = Target;
        if (target == null)
            return;
        list.Add(new AttackInfo(target, damage));
        //이 공격은 쉴드에게 절반의 피해만 입힌다는 클래스
        PL_AttackNHit pl = new(self, list, SoundManager.Instance.Fire)
        {
            ShieldChange = new ShieldHalfChange(),
        };
        //공격을 수행한 캐릭터에 속함
        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(pl);
    }
}
