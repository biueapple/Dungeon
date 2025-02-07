using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defensive_Response : Plate
{
    protected override void Init()
    {
        //Ű���尡 ��� �⺻���� ������
        keywords = new Keyword[2];
        keywords[0] = new Keyword(0, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
        keywords[1] = new Keyword(2, tool[1]);
        keywords[1].Conditions.Add(new Limit_Keyword(keywords[1], 0, 30));
    }

    //�ؽ�Ʈ
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("���͵鿡�� <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " ���ظ� ������ ��� ���谡���� ��ȣ���� <color=#C54646><link=word>" + keywords[1].Word + "</link></color> ��´�.");
    }

    //�����
    public override void Defense()
    {
        //������ ���� �ʾҰų� ���� ��ȣ���� 0�̶�� �������� ����
        if (keywords[1].Word == 0 || keywords[0].Word == 0)
            return;
        List<DefenceInfo> defenceInfos = new ();
        for(int i = 0; i < GameManager.Instance.Adventurer.Count; i++)
        {
            defenceInfos.Add(new DefenceInfo(GameManager.Instance.Adventurer[i], 0, keywords[1].Word));
        }
        //�� ������ ĳ���Ϳ� ����
        GameManager.Instance.CyclePipeLine.Defence.Fulfillment.Add(new PL_ShieldNHeal(self, defenceInfos, null));
    }

    //���ݱ��
    public override void Attack()
    {
        //����� ����
        float damage = keywords[0].Word;
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
