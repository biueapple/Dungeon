using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purification : Plate
{
    protected override void Init()
    {
        //Ű���尡 ��� �⺻���� ������
        keywords = new Keyword[0];
    }

    //�ؽ�Ʈ
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("��� ���谡���� �����̻��� �����Ѵ�.");
    }

    //�����
    public override void Defense()
    {
        List<DefenceInfo> defenceInfos = new();

        for (int i = 0; i < GameManager.Instance.Adventurer.Count; i++)
        {
            for (int p = GameManager.Instance.Adventurer[i].PCB.Count - 1; p >= 0; p--)
            {
                if (GameManager.Instance.Adventurer[i].PCB[p] is IDebuff)
                {
                    GameManager.Instance.Adventurer[i].PCB.RemoveAt(p);
                    defenceInfos.Add(new DefenceInfo(GameManager.Instance.Adventurer[i], 0, 0));
                }
            } 
        }

        //�� ������ ĳ���Ϳ� ����
        if (defenceInfos.Count > 0)
            GameManager.Instance.CyclePipeLine.Defence.Fulfillment.Add(new PL_ShieldNHeal(self, defenceInfos, SoundManager.Instance.Recovery));
    }

    //���ݱ��
    public override void Attack() { }
}
