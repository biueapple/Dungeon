using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curse : Plate
{
    protected override void Init()
    {
        //키워드가 몇개고 기본값이 얼마인지
        keywords = new Keyword[1];
        keywords[0] = new Keyword(0, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
    }

    //텍스트
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("모든 몬스터들의 상태이상의 지속시간을 <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " 만큼 늘리고 즉시 발동시킨다.");
    }

    //방어기능
    public override void Defense() { }

    //공격기능
    public override void Attack()
    {
        List<AttackInfo> list = new();
        for (int i = 0; i < GameManager.Instance.Enemys.Count; i++)
        {
            for (int p = GameManager.Instance.Enemys[i].PCB.Count - 1; p >= 0; p--)
            {
                if (GameManager.Instance.Enemys[i].PCB[p] is IDebuff debuff)
                {
                    debuff.Count++;
                    debuff.Invocation();
                    list.Add(new AttackInfo(GameManager.Instance.Enemys[i], debuff.Damage));
                }
            }
        }

        //대미지가 0이면 공격을 수행한것이 아님
        if (list.Count == 0)
            return;

        //공격을 수행한 캐릭터에 속함
        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, list, SoundManager.Instance.Recovery));
    }
}
