using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutting : Plate
{
    protected override void Init()
    {
        //Ű���尡 ��� �⺻���� ������
        keywords = new Keyword[3];
        keywords[0] = new Keyword(0, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
        keywords[1] = new Keyword(2, tool[1]);
        keywords[1].Conditions.Add(new Limit_Keyword(keywords[1], 0, 30));
        keywords[2] = new Keyword(2, tool[2]);
        keywords[2].Conditions.Add(new Limit_Keyword(keywords[2], 0, 30));
    }

    //�ؽ�Ʈ
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("���͵鿡�� <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " �� ������� �ְ� ���� ���� ���Ϳ��� <color=#C54646><link=word>" + keywords[1].Word + "</link></color>" +
            " �� ������� �ִ� ������ <color=#C54646><link=wow>" + keywords[2].Word + "</link></color>" + 
            " �� �ο��Ѵ�.");
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
        if(GameManager.Instance.Enemys.Count > 0)
        {
            //���� �ο�
            GameManager.Instance.Enemys[0].PCB.Add(new BleedingCreator(GameManager.Instance.Enemys[0], keywords[1].Word, keywords[2].Word));
        }
        
        //������ ������ ĳ���Ϳ� ����
        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, list, SoundManager.Instance.Sword));
    }
}
