using UnityEngine;

//회복 포션 : 아군 캐릭터에게 사용하면 체력 3 회복 (휘발성)
public class HealPotion : Card
{
    //회복할 체력
    private const float Amount = 3;

    //휘발성 카드 (사용하면 사라짐)
    public override bool Volatility { get { return true; } }
    //카드의 가치
    public override int Worth { get { return 1; } }

    private void Start()
    {
        value = 0;
        friendly = true;
        Description = "회복 포션\n모험가 하나의 체력을 3 회복한다.\n휘발성";
    }

    public override void UpCharacter(Character character)
    {
        //아군(모험가)에게만 사용 가능
        if (character == null || !GameManager.Instance.Adventurer.Contains(character))
        {
            UpEmpty();
            return;
        }
        //최대 체력을 넘지 않도록
        character.HP = Mathf.Min(character.HP + Amount, character.HPMax);
        GameManager.Instance.Hand.RemoveCard(this);
        GameManager.Instance.Dummy.AfterCard(this);
        Zoom.Insatnce.Active = true;
    }
}
