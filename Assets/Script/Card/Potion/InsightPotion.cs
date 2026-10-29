//통찰 포션 : 아군 캐릭터에게 사용하면 그 캐릭터의 덱에서 카드 1장을 뽑음 (휘발성)
public class InsightPotion : Card
{
    //휘발성 카드 (사용하면 사라짐)
    public override bool Volatility { get { return true; } }
    //카드의 가치
    public override int Worth { get { return 1; } }

    private void Start()
    {
        value = 0;
        friendly = true;
        Description = "통찰 포션\n모험가 하나의 덱에서 카드를 1장 뽑는다.\n휘발성";
    }

    public override void UpCharacter(Character character)
    {
        //아군(모험가)에게만 사용 가능
        if (character == null || !GameManager.Instance.Adventurer.Contains(character))
        {
            UpEmpty();
            return;
        }
        GameManager.Instance.Hand.RemoveCard(this);
        GameManager.Instance.Dummy.AfterCard(this);
        new IndividualDraw(1, character);
        Zoom.Insatnce.Active = true;
    }
}
