using UnityEngine;

//낡은 방어구 : 아군 캐릭터에게 사용하면 방어도 3을 부여하는 휘발성 카드 (사용하면 사라짐)
public class OldArmor : Card
{
    //휘발성 카드 (사용하면 사라짐)
    public override bool Volatility { get { return true; } }
    //부여할 방어도
    private const float ShieldAmount = 3;

    //카드의 가치
    public override int Worth { get { return 1; } }

    private void Start()
    {
        value = 0;
        friendly = true;
        Description = "낡은 방어구\n모험가 하나에게 방어도 3을 부여한다.\n휘발성";
    }

    public override void UpCharacter(Character character)
    {
        //아군(모험가)에게만 사용 가능
        if (character == null || !GameManager.Instance.Adventurer.Contains(character))
        {
            UpEmpty();
            return;
        }
        character.Shield += ShieldAmount;
        DamageView.Instance.View(character, Color.gray, ShieldAmount);
        //휘발성이라 AfterCard에서 삭제됨
        GameManager.Instance.Hand.RemoveCard(this);
        GameManager.Instance.Dummy.AfterCard(this);
        Zoom.Insatnce.Active = true;
    }
}
