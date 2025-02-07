using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recovery : Plate
{
    protected override void Init()
    {
        //Ű���尡 ��� �⺻���� ������
        keywords = new Keyword[2];
        keywords[0] = new Keyword(3, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
        keywords[1] = new Keyword(0, tool[1]);
        keywords[1].Conditions.Add(new Limit_Keyword(keywords[1], 0, 30));
    }

    //�ؽ�Ʈ
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("���� ���� ���谡���� <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " �� ü���� ȸ�������ش�. ���� �տ� ���Ϳ��� <color=#C54646><link=word>" + keywords[1].Word + "</link></color>" +
            " �� ������� �ش�");
    }

    //�����
    public override void Defense() 
    {
        if (keywords[0].Word == 0)
            return;
        List<DefenceInfo> defenceInfos = new()
        {
            new DefenceInfo(GameManager.Instance.Adventurer[0], keywords[0].Word, 0)
        };
        //�� ������ ĳ���Ϳ� ����
        GameManager.Instance.CyclePipeLine.Defence.Fulfillment.Add(new PL_ShieldNHeal(self, defenceInfos, SoundManager.Instance.Recovery));
    }

    //���ݱ��
    public override void Attack() 
    {
        if(keywords[1].Word == 0)
            return;

        List<AttackInfo> attackInfos = new()
        {
            new AttackInfo(GameManager.Instance.Enemys[0], keywords[1].Word)
        };

        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, attackInfos, SoundManager.Instance.Recovery));
    }
}
