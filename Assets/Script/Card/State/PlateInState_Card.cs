using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//카드가 플레이트에 속할때
public class PlateInState_Card : IState_Card
{
    //이 기능이 속한 카드
    private readonly Card card;
    //이 카드가 속한 플레이트
    private readonly Plate plate;
    //손패
    private readonly Hand hand;
    //마우스를 따라다니는지
    private Coroutine coroutine;

    public PlateInState_Card(Card card, Plate plate, Hand hand)
    {
        this.card = card;
        this.plate = plate;
        this.hand = hand;
    }

    public void Enter() { }
    public void Exit() { }
    //설명 보기
    public void OnPointerEnter() 
    {
        Zoom.Insatnce.ViewON(card);
    }
    //설명 끄기
    public void OnPointerExit()
    {
        Zoom.Insatnce.ViewOFF();
    }


    public void OnPointerDown()
    {
        //손패에서 빼고
        plate.OutputKeyword(card);

        card.State = new HandState_Card(card);
        hand.AddCard(card);

        card.State.OnPointerDown();
    }

    public void OnPointerUp()
    {
        //더이상 마우스를 따라다니지 말고
        card.StopCoroutine(coroutine);
        //마우스를 따라다니지 않고 있다는 뜻
        coroutine = null;
        //판을 클릭했는지 판단
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
