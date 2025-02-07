using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowingDown : Plate
{
    protected override void Init()
    {
        //Ű���尡 ��� �⺻���� ������
        keywords = new Keyword[2];
        keywords[0] = new Keyword(2, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
        keywords[1] = new Keyword(2, tool[1]);
        keywords[1].Conditions.Add(new Limit_Keyword(keywords[1], 0, 30));
    }

    //�ؽ�Ʈ
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("�ڽſ��� <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " �� ��ȣ���� �ش�. ��� ���Ϳ��� <color=#C54646><link=word>" + keywords[1].Word + "</link></color>" +
            " �� ��ȣ���� �ش�.");
    }

    //�����
    public override void Defense()
    {
        List<DefenceInfo> defenceInfos = new();

        if (keywords[0].Word != 0)
        {
            defenceInfos.Add(new DefenceInfo(self, 0, keywords[0].Word));
        }
        if (keywords[1].Word != 0)
        {
            for (int i = 0; i < GameManager.Instance.Enemys.Count; i++)
            {
                defenceInfos.Add(new DefenceInfo(GameManager.Instance.Enemys[i], 0, keywords[1].Word));
            }
        }

        //�� ������ ĳ���Ϳ� ����
        if (defenceInfos.Count > 0)
            GameManager.Instance.CyclePipeLine.Defence.Fulfillment.Add(new PL_ShieldNHeal(self, defenceInfos, null));
    }

    //���ݱ��
    public override void Attack() { }
}
