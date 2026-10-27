using UnityEngine;

//전투 보상으로 고를 카드를 보여주는 창에 있을때의 기능
public class RewardState_Card : IState_Card
{
    private readonly Card card;
    private readonly CardReward reward;
    //선택되기 전의 백그라운드 색
    private Color color;

    //골드 카드의 금색과 구분되도록 초록색
    private static readonly Color selectedColor = new(0.45f, 0.9f, 0.45f);

    public RewardState_Card(Card card, CardReward reward)
    {
        this.card = card;
        this.reward = reward;
    }

    public void Enter()
    {
        color = card.BackColor;
    }
    public void Exit()
    {
        card.BackColor = color;
    }
    public void OnPointerEnter()
    {
        Zoom.Insatnce.ViewON(card);
    }
    public void OnPointerExit()
    {
        Zoom.Insatnce.ViewOFF();
    }
    //클릭하면 선택/해제
    public void OnPointerDown()
    {
        card.BackColor = reward.Toggle(card) ? selectedColor : color;
    }
    public void OnPointerUp() { }
}
