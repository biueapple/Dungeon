

//렐릭의 설명과 가격을 보여줘야 함
using UnityEngine;

public class RelicStore : IRelicState, IInstruction
{
    private readonly IRelic relic;
    private readonly IInstruction instruction;
    //가격
    private readonly int price;

    public RelicStore(IRelic relic, IInstruction instruction, int price)
    {
        this.relic = relic;
        this.instruction = instruction;
        this.price = price;
    }

    public Vector3 Position { get => relic.Position; set => relic.Position = value; }

    public string Instruction()
    {
        return $"가격 {price}\n" + instruction.Instruction();
    }

    //구입
    public void OnMouseClick()
    {
        if(GameManager.Instance.Money >= price)
        {
            relic.RelicCreate();
            Object.Destroy(relic.Transform.gameObject);
            GameManager.Instance.Money -= price;
            Zoom.Insatnce.InstructionOff();
            SoundManager.Instance.Play(SoundManager.Instance.Coin);
        }
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
