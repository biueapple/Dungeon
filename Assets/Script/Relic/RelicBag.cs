

//������ ���� ���� ���ָ� ��
public class RelicBag : IRelicState
{
    private readonly IInstruction instruction;

    public string Instruction()
    {
        return instruction.Instruction();
    }

    public void OnMouseClick()
    {

    }

    public void OnMouseEnter()
    {
        Zoom.Insatnce.Instruction(instruction);
    }

    public void OnMouseExit()
    {
        Zoom.Insatnce.InstructionOff();
    }
}
