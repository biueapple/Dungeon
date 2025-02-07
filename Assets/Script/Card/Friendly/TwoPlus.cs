
public class TwoPlus : Card
{
    private void Start()
    {
        value = 2;
        friendly = true;
        Description = "���谡�� ��ġ �ϳ��� 2 ������Ų��.";
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
