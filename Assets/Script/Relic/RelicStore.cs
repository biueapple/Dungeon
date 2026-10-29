

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
        string price = $"가격 : 카드 가치 {relic.Price}\n";
        if (!CardPayment.CanPay(relic.Price))
            price += $"<color=#C54646>모험가마다 덱에 최소 {CardPayment.MinDeck}장을 남기고 낼 수 있는 가치가 부족함</color>\n";
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
            SoundManager.Instance.Play(SoundManager.Instance.Coin);
            //비용을 냈다면 장착할 모험가 고르기
            CharacterPick.Open("렐릭을 장착할 모험가를 고르세요", owner =>
            {
                relic.RelicCreate(owner);
                Object.Destroy(relic.Transform.gameObject);
            });
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
