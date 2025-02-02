using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Designated_Attack : Plate
{
    protected override void Init()
    {
        //Ű���尡 ��� �⺻���� ������
        keywords = new Keyword[2];
        keywords[0] = new Keyword(3, tool[0]);
        keywords[0].Attributes.Add(new Circulation_Keyword(1, 3));
        keywords[1] = new Keyword(3, tool[1]);
        keywords[1].Conditions.Add(new Limit_Keyword(keywords[1], 0, 30));
    }

    //�ؽ�Ʈ
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("��� <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " ��° ������ <color=#C54646><link=word>" + keywords[1].Word + "</link></color> ������� �ش�.");
    }

    //�����
    public override void Defense() { }

    //���ݱ��
    public override void Attack()
    {
        //keywords[0].Word ��° ���� ã���� ���ٸ� �������� ����
        int index = keywords[0].Word - 1;
        if (index < foe.Count)
        {
            //����� ����
            float damage = keywords[0].Word;
            //������� 0�̸� ������ �����Ѱ��� �ƴ�
            if (damage == 0)
                return;

            //������ ������ ĳ���Ϳ� ����
            GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, new List<AttackInfo>() { new (foe[index], damage) }, SoundManager.Instance.Sword));
        }
    }
}
