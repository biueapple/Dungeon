

//렐릭의 설명과 가격을 보여줘야 함
using UnityEngine;

public class RelicStore : IRelicState, IInstruction
{
    private readonly Relic relic;
    private readonly IInstruction instruction;

    public RelicStore(Relic relic, IInstruction instruction)
    {
        this.relic = relic;
        this.instruction = instruction;
    }

    public Vector3 Position { get => relic.Position; set => relic.Position = value; }

    public string Instruction()
    {
        string price = $"가격 카드 {relic.Price}장\n";
        if (!CardPayment.CanPay(relic.Price))
            price += $"<color=#C54646>덱에 최소 {CardPayment.MinDeck}장은 남아있어야 함</color>\n";
        return price + instruction.Instruction();
    }

    //구입 (덱에서 비용으로 낼 카드를 고르는 창을 열고 다 고르면 구입)
    public void OnMouseClick()
    {
        if (!CardPayment.CanPay(relic.Price))
            return;
        Zoom.Insatnce.InstructionOff();
        CardPayment.Open(relic.Price, () =>
        {
            relic.RelicCreate();
            Object.Destroy(relic.Transform.gameObject);
            SoundManager.Instance.Play(SoundManager.Instance.Coin);
        });
    }

    public void OnMouseEnter()
    {
        Zoom.Insatnce.Instruction(this);
    }

    public void OnMouseExit()
    {
        Zoom.Insatnce.InstructionOff();
    }
}
