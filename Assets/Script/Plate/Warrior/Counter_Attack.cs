using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter_Attack : Plate
{
    protected override void Init()
    {
        //Ű���尡 ��� �⺻���� ������
        keywords = new Keyword[1];
        keywords[0] = new Keyword(8, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
    }

    //�ؽ�Ʈ
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("�ڽſ��� ��ȣ���� <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " ȹ���ϸ� ���� �� ���� ��ȣ����ŭ ���͵鿡�� ���ظ� ������.");
    }

    //�����
    public override void Defense()
    {
        //��ȣ�� ȹ��
        //���� ��� ������ 0�̶�� �� �����Ѱ��� �ƴ�
        if (keywords[0].Word == 0)
            return;
        List<DefenceInfo> defenceInfos = new ()
        {
            new DefenceInfo(self, 0, keywords[0].Word)
        };
        //�� ������ ĳ���Ϳ� ����
        GameManager.Instance.CyclePipeLine.Defence.Fulfillment.Add(new PL_ShieldNHeal(self, defenceInfos, null));
    }

    //���ݱ��
    public override void Attack()
    {
        //����� ����
        float damage = self.Shield;
        //������� 0�̸� ������ �����Ѱ��� �ƴ�
        if (damage == 0)
            return;

        List<AttackInfo> list = new();

        //�����ִ� ���� ��
        switch (GameManager.Instance.Enemys.Count)
        {
            //������ ����
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
        //������ ������ ĳ���Ϳ� ����
        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, list, SoundManager.Instance.Sword));
    }
}
