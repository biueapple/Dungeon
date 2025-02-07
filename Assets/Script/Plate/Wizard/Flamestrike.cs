using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamestrike : Plate
{
    protected override void Init()
    {
        //Ű���尡 ��� �⺻���� ������
        keywords = new Keyword[4];
        keywords[0] = new Keyword(1, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
        keywords[1] = new Keyword(0, tool[1]);
        keywords[1].Conditions.Add(new Limit_Keyword(keywords[1], 0, 30));
        keywords[2] = new Keyword(1, tool[2]);
        keywords[2].Conditions.Add(new Limit_Keyword(keywords[2], 0, 30));
        keywords[3] = new Keyword(1, tool[3]);
        keywords[3].Conditions.Add(new Limit_Keyword(keywords[3], 0, 30));
    }

    //�ؽ�Ʈ
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("���͵鿡�� <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " �� ������� �ְ� �տ������� <color=#C54646><link=word>" + keywords[1].Word + "</link></color>" +
            " ��ŭ�� ���Ϳ��� <color=#C54646><link=wow>" + keywords[2].Word + "</link></color>" +
            " �� ������� �ִ� ȭ���� <color=#C54646><link=wow>" + keywords[3].Word + "</link></color>" +
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
        for(int i = 0; i < keywords[1].Word; i++)
        {
            if(GameManager.Instance.Enemys.Count - 1 < i)
            {
                //ȭ�� �ο�
                GameManager.Instance.Enemys[i].PCB.Add(new BurnCreator(GameManager.Instance.Enemys[i], keywords[2].Word, keywords[3].Word));
            }
        }
        

        //������ ������ ĳ���Ϳ� ����
        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(new PL_AttackNHit(self, list, SoundManager.Instance.Fire));
    }
}
