using UnityEngine;

public class OneMinus : Card
{
    private void Start()
    {
        value = -1;
        friendly = true;
        Description = "�Ʊ��� ��ġ �ϳ��� 1 ���ҽ�Ų��.";
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