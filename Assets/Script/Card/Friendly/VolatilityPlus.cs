using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolatilityPlus : Card
{
    private void Start()
    {
        value = 1;
        friendly = true;
        volatility = true;
        Description = "아군의 수치 하나를 1 증가시킨다.";
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
