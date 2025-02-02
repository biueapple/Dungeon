using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ī�尡 ���п� ������ ���
public class HandState_Card : IState_Card
{
    //�� ����� �޴� ī��
    private readonly Card card;
    //���콺�� ����ٴϴ� �ڷ�ƾ
    private Coroutine coroutine;

    public HandState_Card(Card card)
    {
        this.card = card;
    }

    //���콺�� �÷������� ������ ũ�� ������
    public void OnPointerEnter()
    {
        //���콺�� ����ٴϰ� ���� �ʴٸ� (ī�带 Ŭ���� ���¶��)
        //���� ���̵���
        Zoom.Insatnce.ViewON(card);
    }

    public void OnPointerExit()
    {
        //���콺�� �ٶ�ٴϰ� ���� �ʴٸ� (ī�带 Ŭ���� ���¶��)
        Zoom.Insatnce.ViewOFF();//���� ������ �ʵ���
    }

    //���콺 down
    public void OnPointerDown()
    {
        //���п��� ����
        //hand.RemoveCard(card);
        //���콺�� ����ٴϵ��� �ϰ�
        coroutine = card.StartCoroutine(MF());
        //ī�带 ���� ������Ʈ�� ���α�
        Zoom.Insatnce.ViewOFF();
        Zoom.Insatnce.Active = false;
    }

    //���콺 up
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

    //���콺�� ����ٴϰ� �ϴ� �ڷ�ƾ
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
