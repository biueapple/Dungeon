using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ī�尡 �� �ȿ� �������� ���
public class PackInState_Card : IState_Card
{
    //�ڽ��� ���� ��
    private readonly CardPack pack;
    //�ڽ��� ���� ī��
    private readonly Card card;
    //���콺 down���� pack ��ġ
    private Vector3 position;
    //���콺�� ���󰡴���
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
        //���� ����
        Zoom.Insatnce.ViewON(card);
    }
    public void OnPointerExit()
    {
        //���� ����
        Zoom.Insatnce.ViewOFF();
    }
    public void OnPointerDown()
    {
        //���� ���� �ִ����� ����ϰ�
        position = pack.transform.position;
        //���콺�� ����ٴϵ��� �ϰ�
        coroutine = card.StartCoroutine(MF());
        //ī�带 ���� ������Ʈ�� ���α�
        Zoom.Insatnce.Active = false;
    }
    public void OnPointerUp()
    {
        //������ up�ߴٸ� �Ҽ�
        //�ƴ϶�� ���ư���

        //���̻� ���콺�� ����ٴ��� ����
        card.StopCoroutine(coroutine);
        //���콺�� ����ٴ��� �ʰ� �ִٴ� ��
        coroutine = null;

        //���� Ŭ���ߴ��� �Ǵ�
        if (UIManager.Instance.TryGetGraphicRayFindParent(out Deck deck))
        {
            //�߰�
            if (deck.InputCard(pack))
            {
                //�߰��� ���������� ���� �� ���� ����
            }
        }

        //���� �ٽ� ���� ��ġ�� (���� ������ ���� ���� �� ����)
        pack.transform.position = position;
        //������ �ٽ� ���̵���
        Zoom.Insatnce.Active = true;
    }
    //���콺�� ����ٴϰ� �ϴ� �ڷ�ƾ
    private IEnumerator MF()
    {
        while (true)
        {
            pack.transform.position = Input.mousePosition;
            yield return null;
        }
    }
}
