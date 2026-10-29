using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamestrike : Plate
{
    protected override void Init()
    {
        //공격 대상을 정할 수 있는 플레이트
        useTarget = true;
        //키워드가 몇개고 기본값이 얼마인지
        keywords = new Keyword[4];
        keywords[0] = new Keyword(1, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
        keywords[1] = new Keyword(0, tool[1]);
        keywords[1].Conditions.Add(new Limit_Keyword(keywords[1], 0, 30));
        keywords[2] = new Keyword(1, tool[2]);
        keywords[2].Conditions.Add(new Limit_Keyword(keywords[2], 0, 30));
        keywords[3] = new Keyword(1, tool[3]);
        keywords[3].Conditions.Add(new Limit_Keyword(keywords[3], 0, 30));
    }

    //텍스트
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            (TargetText + "에게 <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " 의 대미지를 주고 앞에서부터 <color=#C54646><link=word>" + keywords[1].Word + "</link></color>" +
            " 만큼의 몬스터에게 <color=#C54646><link=wow>" + keywords[2].Word + "</link></color>" +
            " 의 대미지를 주는 화상을 <color=#C54646><link=wow>" + keywords[3].Word + "</link></color>" +
            " 턴 부여한다.");
    }

    //방어기능
    public override void Defense() { }

    //공격기능
    public override void Attack()
    {
        //대미지 설정
        float damage = keywords[0].Word;
        //대미지가 0이면 공격을 수행한것이 아님
        if (damage == 0)
            return;

        List<AttackInfo> list = new();

        //플레이트에 지정된 대상에게 공격
        Character target = Target;
        if (target == null)
            return;
        list.Add(new AttackInfo(target, damage));
        for(int i = 0; i < keywords[1].Word; i++)
        {
            if(i < GameManager.Instance.Enemys.Count)
            {
                //화상 부여
                GameManager.Instance.Enemys[i].PCB.Add(new BurnCreator(GameManager.Instance.Enemys[i], keywords[2].Word, keywords[3].Word));
            }
        }
        

        //공격을 수행한 캐릭터에 속함
        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, list, SoundManager.Instance.Fire));
    }
}
