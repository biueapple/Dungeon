using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : Card
{
    private void Start()
    {
        value = 0;
        friendly = true;
        Description = "���谡�� �÷���Ʈ�� �ϳ� �μ��� ī�� ������ �̰� �ֹ߼��� ���谡 +1 ī�带 �ϳ� �����Ѵ�.";
    }

    public override void UpPlate(Plate plate)
    {
        //�Ʊ��� �÷���Ʈ���
        if(GameManager.Instance.Adventurer.Contains(plate.Self))
        {
            //��ο�� ����
            new IndividualDraw(1);
            new IndividualCard("Adventurer/VolatilityPlus");
            //�νô� �÷���Ʈ�� �ִ� ī�带 ���п� �߰������ �ϴµ�
            for(int i  = 0; i < plate.Keyword.Length; i++)
            {
                for (int j = plate.Keyword[i].Space.Count - 1; j >= 0; j--)
                {
                    //ī�尡 ���� ���п� �ִ� �������� �ְ�
                    plate.Keyword[i].Space[j].State = new HandState_Card(plate.Keyword[i].Space[j]);
                    //Ȱ��ȭ
                    plate.Keyword[i].Space[j].gameObject.SetActive(true);
                    //���п� �߰�
                    GameManager.Instance.Hand.AddCard(plate.Keyword[i].Space[j]);
                    plate.Keyword[i].Space.RemoveAt(j);
                }
            }
            //�ڽ��� ���Ǿ����� ����� ������ ���ư���
            plate.Dummy.Before.Remove(this);
            GameManager.Instance.Hand.RemoveCard(this);
            plate.Dummy.AfterCard(this);
            //�÷���Ʈ �μ���
            new IndividualBroken(plate.Self);
        }
        Zoom.Insatnce.Active = true;
    }
}
