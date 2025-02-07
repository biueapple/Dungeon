using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snipe : Plate
{
    protected override void Init()
    {
        //Ű���尡 ��� �⺻���� ������
        keywords = new Keyword[1];
        keywords[0] = new Keyword(4, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
    }

    //�ؽ�Ʈ
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("���� ���� ���谡���� <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " �� ������� �ش�.");
    }

    //�����
    public override void Defense()
    {
    }

    //���ݱ��
    public override void Attack()
    {
        //����� ����
        float damage = keywords[0].Word;
        //������� 0�̸� ������ �����Ѱ��� �ƴ�
        if (damage == 0)
            return;

        List<AttackInfo> list = new()
        {
            new AttackInfo(GameManager.Instance.Adventurer[^1], damage)
        };

        //������ ������ ĳ���Ϳ� ����
        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, list, null));
    }
}
