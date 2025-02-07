using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowOfMalice : Plate
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
            ("���� ���� ���谡���� <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " �� ������� �ְ� <color=#C54646><link=word>" + keywords[1].Word + "</link></color>" +
            " �� ������� 2�ϰ� �ִ� ������ �ο��Ѵ�.");
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
            new AttackInfo(GameManager.Instance.Adventurer[0], damage)
        };

        if (GameManager.Instance.Adventurer.Count > 0)
        {
            //���� �ο�
            GameManager.Instance.Adventurer[0].PCB.Add(new BleedingCreator(GameManager.Instance.Adventurer[0], keywords[1].Word, 2));
        }

        //������ ������ ĳ���Ϳ� ����
        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, list, SoundManager.Instance.Sword));
    }
}
