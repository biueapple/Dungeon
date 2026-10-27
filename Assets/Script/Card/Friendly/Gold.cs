using UnityEngine;

//아무 효과가 없는 카드 (전투 보상으로 얻을 수 있음)
public class Gold : Card
{
    private void Awake()
    {
        //다른 카드와 구분되도록 금색 백그라운드
        BackColor = new Color(1f, 0.8f, 0.25f);
    }

    private void Start()
    {
        value = 0;
        friendly = true;
        Description = "골드\n아무 효과가 없다.\n모험가의 플레이트에 내면 버려진다.";
    }

    public override void UpPlate(Plate plate)
    {
        //아군의 플레이트에 내면 효과 없이 사용한 덱으로
        if (GameManager.Instance.Adventurer.Contains(plate.Self))
        {
            GameManager.Instance.Hand.RemoveCard(this);
            GameManager.Instance.Dummy.AfterCard(this);
            Zoom.Insatnce.Active = true;
        }
        else
        {
            UpEmpty();
        }
    }
}
