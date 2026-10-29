using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter_Attack : Plate
{
    protected override void Init()
    {
        //공격 대상을 정할 수 있는 플레이트
        useTarget = true;
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
            " 획득하며 수비 후 남은 보호막만큼 " + TargetText + "에게 피해를 입힌다.");
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

        //플레이트에 지정된 대상에게 공격
        Character target = Target;
        if (target == null)
            return;
        list.Add(new AttackInfo(target, damage));
        //공격을 수행한 캐릭터에 속함
        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, list, SoundManager.Instance.Sword));
    }
}
