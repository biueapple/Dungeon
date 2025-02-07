using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVolatilityPlus : Card
{
    private void Start()
    {
        value = 1;
        friendly = false;
        volatility = true;
        Description = "������ ��ġ �ϳ��� 1 ������Ų��.";
    }

    public override void UpPlate(Plate plate)
    {
        //�߰�
        if (plate.InputKeyword(this))
        {
            GameManager.Instance.Hand.RemoveCard(this);
            //�����ߴٸ� state�� �����ؾ� ��
            State = new PlateInState_Card(this, plate, GameManager.Instance.Hand);
            Zoom.Insatnce.Active = true;
            return;
        }
        else
        {
            UpEmpty();
        }
    }

}
