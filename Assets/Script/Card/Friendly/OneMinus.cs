using UnityEngine;

public class OneMinus : Card
{
    //카드의 가치
    public override int Worth { get { return -1; } }

    private void Start()
    {
        value = -1;
        friendly = true;
        Description = "모험가의 수치 하나를 1 감소시킨다.";
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