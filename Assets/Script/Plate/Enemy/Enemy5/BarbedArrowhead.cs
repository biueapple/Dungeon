using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbedArrowhead : Plate
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
            ("������ ���谡���� <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " �� ������� �ְ� �λ��� 1�ϰ� ������.");
    }
    //�����
    public override void Defense(){}
    //���ݱ��
    public override void Attack()
    {
        //����� ����
        float damage = keywords[0].Word;
        //������� 0�̸� ������ �����Ѱ��� �ƴ�
        if (damage == 0)
            return;
        int index = Random.Range(0, GameManager.Instance.Adventurer.Count);

        List<AttackInfo> list = new()
        {
            new AttackInfo(GameManager.Instance.Adventurer[index], damage)
        };

        //�λ�
        GameManager.Instance.Adventurer[index].PCB.Add(new InjuryCreator(GameManager.Instance.Adventurer[index], 1));

        //������ ������ ĳ���Ϳ� ����
        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, list, null));
    }
}
