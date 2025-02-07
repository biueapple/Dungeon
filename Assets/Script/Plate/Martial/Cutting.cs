using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutting : Plate
{
    protected override void Init()
    {
        //키워드가 몇개고 기본값이 얼마인지
        keywords = new Keyword[3];
        keywords[0] = new Keyword(0, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
        keywords[1] = new Keyword(2, tool[1]);
        keywords[1].Conditions.Add(new Limit_Keyword(keywords[1], 0, 30));
        keywords[2] = new Keyword(2, tool[2]);
        keywords[2].Conditions.Add(new Limit_Keyword(keywords[2], 0, 30));
    }

    //텍스트
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("몬스터들에게 <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " 의 대미지를 주고 가장 앞의 몬스터에게 <color=#C54646><link=word>" + keywords[1].Word + "</link></color>" +
            " 의 대미지를 주는 출혈을 <color=#C54646><link=wow>" + keywords[2].Word + "</link></color>" + 
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

        //남아있는 적의 수
        switch (GameManager.Instance.Enemys.Count)
        {
            //공격을 수행
            case 1:
                {
                    list.Add(new AttackInfo(GameManager.Instance.Enemys[0], damage));
                }
                break;
            case 2:
                {
                    list.Add(new AttackInfo(GameManager.Instance.Enemys[0], damage * 0.7f));

                    list.Add(new AttackInfo(GameManager.Instance.Enemys[1], damage * 0.3f));
                }
                break;
            case 3:
                {
                    list.Add(new AttackInfo(GameManager.Instance.Enemys[0], damage * 0.6f));

                    list.Add(new AttackInfo(GameManager.Instance.Enemys[1], damage * 0.3f));

                    list.Add(new AttackInfo(GameManager.Instance.Enemys[2], damage * 0.1f));
                }
                break;
            default:
                return;
        }
        if(GameManager.Instance.Enemys.Count > 0)
        {
            //출혈 부여
            GameManager.Instance.Enemys[0].PCB.Add(new BleedingCreator(GameManager.Instance.Enemys[0], keywords[1].Word, keywords[2].Word));
        }
        
        //공격을 수행한 캐릭터에 속함
        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, list, SoundManager.Instance.Sword));
    }
}
