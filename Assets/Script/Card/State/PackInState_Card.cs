using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//카드가 팩 안에 있을때의 기능
public class PackInState_Card : IState_Card
{
    //자신이 속한 팩
    private readonly CardPack pack;
    //자신이 속한 카드
    private readonly Card card;
    //마우스 down시의 pack 위치
    private Vector3 position;
    //마우스를 따라가는지
    private Coroutine coroutine;

    public PackInState_Card(CardPack pack, Card card)
    {
        this.pack = pack;
        this.card = card;
    }

    public void Enter() { }
    public void Exit() { }
    public void OnPointerEnter()
    {
        //설명 보기
        Zoom.Insatnce.ViewON(card);
    }
    public void OnPointerExit()
    {
        //설명 끄기
        Zoom.Insatnce.ViewOFF();
    }
    public void OnPointerDown()
    {
        //원래 팩이 있던곳을 기억하고
        position = pack.transform.position;
        //마우스를 따라다니도록 하고
        coroutine = card.StartCoroutine(MF());
        //카드를 줌한 오브젝트를 꺼두기
        Zoom.Insatnce.Active = false;
    }
    public void OnPointerUp()
    {
        //덱에서 up했다면 소속
        //아니라면 돌아가기

        //더이상 마우스를 따라다니지 말고
        card.StopCoroutine(coroutine);
        //마우스를 따라다니지 않고 있다는 뜻
        coroutine = null;

        //판을 클릭했는지 판단
        if (UIManager.Instance.TryGetGraphicRayFindParent(out Deck deck))
        {
            //추가
            if (deck.InputCard(pack))
            {
                //추가가 성공했을때 따로 할 일은 없음
            }
        }

        //팩은 다시 원래 위치로 (팩은 무한히 덱에 넣을 수 있음)
        pack.transform.position = position;
        //설명이 다시 보이도록
        Zoom.Insatnce.Active = true;
    }
    //마우스를 따라다니게 하는 코루틴
    private IEnumerator MF()
    {
        while (true)
        {
            pack.transform.position = Input.mousePosition;
            yield return null;
        }
    }
}
