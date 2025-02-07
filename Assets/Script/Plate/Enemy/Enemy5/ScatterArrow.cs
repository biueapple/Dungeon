using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterArrow : Plate
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
            ("���谡�鿡�� <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " �� ������� �ش�. �� ������ ����� ����� �ڿ������� �Ѵ�.");
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

        List<AttackInfo> list = new();

        switch (GameManager.Instance.Adventurer.Count)
        {
            //������ ����
            case 1:
                {
                    list.Add(new AttackInfo(GameManager.Instance.Adventurer[0], damage));
                }
                break;
            case 2:
                {
                    list.Add(new AttackInfo(GameManager.Instance.Adventurer[1], damage * 0.7f));

                    list.Add(new AttackInfo(GameManager.Instance.Adventurer[0], damage * 0.3f));
                }
                break;
            case 3:
                {
                    list.Add(new AttackInfo(GameManager.Instance.Adventurer[2], damage * 0.6f));

                    list.Add(new AttackInfo(GameManager.Instance.Adventurer[1], damage * 0.3f));

                    list.Add(new AttackInfo(GameManager.Instance.Adventurer[0], damage * 0.1f));
                }
                break;
            default:
                return;
        }

        //������ ������ ĳ���Ϳ� ����
        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, list, null));
    }
}
