using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVolatilityPlus : Card
{
    //휘발성 카드 (사용하면 사라짐)
    public override bool Volatility { get { return true; } }
    private void Start()
    {
        value = 1;
        friendly = false;
        Description = "몬스터의 수치 하나를 1 증가시킨다.";
    }

    public override void UpPlate(Plate plate)
    {
        //추가
        if (plate.InputKeyword(this))
        {
            GameManager.Instance.Hand.RemoveCard(this);
            //성공했다면 state를 변경해야 함
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
