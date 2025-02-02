using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//카드가 손패에 있을때 기능
public class HandState_Card : IState_Card
{
    //이 기능을 받는 카드
    private readonly Card card;
    //마우스를 따라다니는 코루틴
    private Coroutine coroutine;

    public HandState_Card(Card card)
    {
        this.card = card;
    }

    //마우스를 올려놨을때 설명을 크게 보도록
    public void OnPointerEnter()
    {
        //마우스를 따라다니고 있지 않다면 (카드를 클릭한 상태라면)
        //설명 보이도록
        Zoom.Insatnce.ViewON(card);
    }

    public void OnPointerExit()
    {
        //마우스를 다라다니고 있지 않다면 (카드를 클릭한 상태라면)
        Zoom.Insatnce.ViewOFF();//설명 보이지 않도록
    }

    //마우스 down
    public void OnPointerDown()
    {
        //손패에서 빼고
        //hand.RemoveCard(card);
        //마우스를 따라다니도록 하고
        coroutine = card.StartCoroutine(MF());
        //카드를 줌한 오브젝트를 꺼두기
        Zoom.Insatnce.ViewOFF();
        Zoom.Insatnce.Active = false;
    }

    //마우스 up
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
        if(UIManager.Instance.TryGetGraphicRayFindParent(out Card c))
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

    //마우스를 따라다니게 하는 코루틴
    private IEnumerator MF()
    {
        while (true)
        {
            card.transform.position = Input.mousePosition + new Vector3(75, -125, 0);
            yield return null;
        }
    }

    public void Enter() { }

    public void Exit() { }
}
