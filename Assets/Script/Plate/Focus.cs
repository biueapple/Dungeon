using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Focus : Plate
{
    protected override void Init()
    {
        //Ű���尡 ��� �⺻���� ������
        keywords = new Keyword[1];
        keywords[0] = new Keyword(2, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
    }

    //�ؽ�Ʈ
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("��� �Ʊ��� <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " ��ŭ ��ȣ���� ȹ���ϸ� ���� �Ͽ� ī�带 1�� �� �̴´�.");
    }

    //�����
    public override void Defense()
    {
        //���� ���� ��ȣ���� ���ٸ� �ڿ� ȿ���� �̷����� ����
        if (keywords[0].Word == 0)
            return;
        //�ݹ� (�߰���ο�) �߰�
        GameManager.Instance.PCB.Add(new AdditionalDraws());
        GameManager.Instance.PCB.Add(new ReductionDraws()); 
        List<DefenceInfo> defenceInfos = new();
        for (int i = 0; i < ally.Count; i++)
        {
            defenceInfos.Add(new DefenceInfo(ally[i], 0, keywords[0].Word));
        }
        //�� ������ ĳ���Ϳ� ����
        GameManager.Instance.CyclePipeLine.Defence.Fulfillment.Add(new PL_ShieldNHeal(self, defenceInfos, SoundManager.Instance.Recovery));
    }

    //���ݱ��
    public override void Attack() { }
}
