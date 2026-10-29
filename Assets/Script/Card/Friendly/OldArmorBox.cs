//오래된 방어구 상자 : 아군 캐릭터에게 사용하면 손패에 낡은 방어구 2장을 생성
public class OldArmorBox : Card
{
    //생성할 낡은 방어구 수
    private const int Count = 2;

    //카드의 가치
    public override int Worth { get { return 2; } }
    //전사 전용 카드
    public override System.Type Job { get { return typeof(Warrior); } }

    private void Start()
    {
        value = 0;
        friendly = true;
        Description = "오래된 방어구 상자\n손패에 휘발성인 낡은 방어구 카드를 2장 생성한다.";
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
        for (int i = 0; i < Count; i++)
        {
            new IndividualCard("Adventurer/OldArmor");
        }
        Zoom.Insatnce.Active = true;
    }
}
