using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ī�尡 �÷���Ʈ�� ���Ҷ�
public class PlateInState_Card : IState_Card
{
    //�� ����� ���� ī��
    private readonly Card card;
    //�� ī�尡 ���� �÷���Ʈ
    private readonly Plate plate;
    //����
    private readonly Hand hand;
    //���콺�� ����ٴϴ���
    private Coroutine coroutine;

    public PlateInState_Card(Card card, Plate plate, Hand hand)
    {
        this.card = card;
        this.plate = plate;
        this.hand = hand;
    }

    public void Enter() { }
    public void Exit() { }
    //���� ����
    public void OnPointerEnter() 
    {
        Zoom.Insatnce.ViewON(card);
    }
    //���� ����
    public void OnPointerExit()
    {
        Zoom.Insatnce.ViewOFF();
    }


    public void OnPointerDown()
    {
        //���п��� ����
        plate.OutputKeyword(card);

        card.State = new HandState_Card(card);
        hand.AddCard(card);

        card.State.OnPointerDown();
    }

    public void OnPointerUp()
    {
        //���̻� ���콺�� ����ٴ��� ����
        card.StopCoroutine(coroutine);
        //���콺�� ����ٴ��� �ʰ� �ִٴ� ��
        coroutine = null;
        //���� Ŭ���ߴ��� �Ǵ�
        if (UIManager.Instance.TryGetGraphicRayFindParent(out Plate plate))
        {
            card.UpPlate(plate);
        }
        if (UIManager.Instance.TryGetGraphicRayFindParent(out Card c))
        {
            if (c == card)
                c = null;
            else
                card.UpCard(c);
        }
        if (UIManager.Instance.TryGetGraphicRayFindParent(out PlayAnimation animation))
        {
            card.UpCharacter(animation.transform.parent.GetComponent<Character>());
        }
        if (plate == null && c == null && animation == null)
        {
            card.UpEmpty();
        }
    }
}
