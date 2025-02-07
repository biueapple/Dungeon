using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curse : Plate
{
    protected override void Init()
    {
        //Ű���尡 ��� �⺻���� ������
        keywords = new Keyword[1];
        keywords[0] = new Keyword(0, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
    }

    //�ؽ�Ʈ
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("��� ���͵��� �����̻��� ���ӽð��� <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " ��ŭ �ø��� ��� �ߵ���Ų��.");
    }

    //�����
    public override void Defense() { }

    //���ݱ��
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

        //������� 0�̸� ������ �����Ѱ��� �ƴ�
        if (list.Count == 0)
            return;

        //������ ������ ĳ���Ϳ� ����
        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, list, SoundManager.Instance.Recovery));
    }
}
