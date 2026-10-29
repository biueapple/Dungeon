using System;
using UnityEngine;

//창에서 카드를 클릭해 선택/해제할때의 기능 (선택 여부는 창이 정함)
public class SelectState_Card : IState_Card
{
    private readonly Card card;
    //클릭했을때 호출, 선택되었다면 true
    private readonly Func<Card, bool> toggle;
    //선택되기 전의 백그라운드 색
    private Color color;

    private static readonly Color selectedColor = new(0.45f, 0.9f, 0.45f);

    public SelectState_Card(Card card, Func<Card, bool> toggle)
    {
        this.card = card;
        this.toggle = toggle;
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
        card.BackColor = toggle(card) ? selectedColor : color;
    }
    public void OnPointerUp() { }
}
