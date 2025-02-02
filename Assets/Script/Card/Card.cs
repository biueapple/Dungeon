using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    //�� ī�尡 ������ �ִ� ��ġ
    //[SerializeField]
    protected int value;
    public int Value { get { return value; } }

    //�� ��ġ�� �������� �ش�� �� �ִ°�
    //[SerializeField]
    protected bool friendly;
    public bool Friendly { get {  return friendly; } }

    protected bool volatility = false;
    public bool Volatility { get { return volatility; } }

    //ī���� ������
    [SerializeField]
    protected Image icon;
    public Sprite Icon { get { return icon.sprite; } set { icon.sprite = value; } }
    //ī���� ������ �ԷµǾ� �ִ� ������Ʈ
    [SerializeField]
    protected TextMeshProUGUI textComponent;
    public string Description { get { return textComponent.text; } set { textComponent.text = value; } }

    //ī�尡 � ��Ȳ�� ��� �����ؾ� �ϴ��� ��찡 �������� state�� ������� ��
    //���� ī�尡 ���� ��Ȳ���� ��� �����ؾ� �ϴ���
    [SerializeField]
    protected IState_Card state;
    public IState_Card State { get { return state; } set { state?.Exit(); state = value; state?.Enter(); } }   

    public void OnPointerEnter(PointerEventData eventData)
    {
        state?.OnPointerEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        state?.OnPointerExit();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        state?.OnPointerDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        state?.OnPointerUp();
    }

    //��� ������ up �ߴ��� �ݹ��� ����� �ҵ�
    //�ٸ� ī�� ������ up�ߴ�
    public virtual void UpCard(Card card) { UpEmpty(); }
    //plate ������ up�ߴ�
    public virtual void UpPlate(Plate plate) { UpEmpty(); }
    //character ������ up�ߴ�
    public virtual void UpCharacter(Character character) { UpEmpty(); }
    //������� up�ߴ�
    public virtual void UpEmpty()
    {
        GameManager.Instance.Hand.Sorting.Sort(null);
        Zoom.Insatnce.Active = true;
    }
}
