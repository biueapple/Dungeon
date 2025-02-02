using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : Plate
{
    protected override void Init()
    {
        //Ű���尡 ��� �⺻���� ������
        keywords = new Keyword[1];
        keywords[0] = new Keyword(1, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
    }

    //�ؽ�Ʈ
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("��뿡�� <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " ���ظ� �����ϰ� ������.");
    }

    //�����
    public override void Defense() { }

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
        switch (foe.Count)
        {
            //������ ����
            case 1:
                {
                    list.Add(new AttackInfo(foe[0], damage));
                }
                break;
            case 2:
                {
                    list.Add(new AttackInfo(foe[0], damage));

                    list.Add(new AttackInfo(foe[1], damage));
                }
                break;
            case 3:
                {
                    list.Add(new AttackInfo(foe[0], damage));

                    list.Add(new AttackInfo(foe[1], damage));

                    list.Add(new AttackInfo(foe[2], damage));
                }
                break;
            default:
                return;
        }
        //������ ������ ĳ���Ϳ� ����
        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, list, SoundManager.Instance.Fire));
    }
}
