using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weak_Attack : Plate
{
    protected override void Init()
    {
        //Ű���尡 ��� �⺻���� ������
        keywords = new Keyword[1];
        keywords[0] = new Keyword(5, tool[0]);
        keywords[0].Conditions.Add(new Limit_Keyword(keywords[0], 0, 30));
    }

    //�ؽ�Ʈ
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("���͵鿡�� <color=#C54646><link=example>" + keywords[0].Word + "</link></color>" +
            " ������� �ش�. ��ȣ�����Դ� ������ ������� �ش�.");
    }

    //�����
    public override void Defense() { }

    //���ݱ��
    public override void Attack()
    {
        //����� ����
        float damage = keywords[0].Word;
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
        //�� ������ ���忡�� ������ ���ظ� �����ٴ� Ŭ����
        PL_AttackNHit pl = new(self, list, SoundManager.Instance.Fire)
        {
            ShieldChange = new ShieldHalfChange(),
        };
        //������ ������ ĳ���Ϳ� ����
        GameManager.Instance.CyclePipeLine.Attack.Fulfillment.Add(pl);
    }
}
