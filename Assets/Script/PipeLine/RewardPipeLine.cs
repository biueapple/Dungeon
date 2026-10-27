//전투를 클리어하고 보상 카드를 고르는 단계 (고르거나 건너뛰면 다음 단계로)
public class RewardPipeLine : IPipeLine
{
    private IPipeLine next;
    public IPipeLine Next { get => next; set => next = value; }

    public RewardPipeLine(IPipeLine next)
    {
        this.next = next;
    }

    public void Start()
    {
        CardReward.Open(() => GameManager.Instance.PipeLine = next);
    }

    public void Update() { }

    public void End() { }
}
