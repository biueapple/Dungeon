using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//상점에서 비용으로 낼 카드를 고르는 창에 있을때의 기능
public class PaymentState_Card : IState_Card
{
    private readonly Card card;
    private readonly CardPayment payment;
    //선택되기 전의 백그라운드 색
    private Color color;

    private static readonly Color selectedColor = new(1f, 0.85f, 0.3f);

    public PaymentState_Card(Card card, CardPayment payment)
    {
        this.card = card;
        this.payment = payment;
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
        card.BackColor = payment.Toggle(card) ? selectedColor : color;
    }
    public void OnPointerUp() { }
}
