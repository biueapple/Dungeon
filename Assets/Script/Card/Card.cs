using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    //이 카드가 가지고 있는 수치
    //[SerializeField]
    protected int value;
    public int Value { get { return value; } }

    //이 수치가 누구에게 해당될 수 있는가
    //[SerializeField]
    protected bool friendly;
    public bool Friendly { get {  return friendly; } }

    protected bool volatility = false;
    public bool Volatility { get { return volatility; } }

    //카드의 아이콘
    [SerializeField]
    protected Image icon;
    public Sprite Icon { get { return icon.sprite; } set { icon.sprite = value; } }
    //카드의 설명이 입력되어 있는 컴포넌트
    [SerializeField]
    protected TextMeshProUGUI textComponent;
    public string Description { get { return textComponent.text; } set { textComponent.text = value; } }

    //카드가 어떤 상황에 어떻게 반응해야 하는지 경우가 많아져서 state로 나누기로 함
    //지금 카드가 무슨 상황인지 어떻게 반응해야 하는지
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

    //어느 위에서 up 했는지 콜백을 해줘야 할듯
    //다른 카드 위에서 up했다
    public virtual void UpCard(Card card) { UpEmpty(); }
    //plate 위에서 up했다
    public virtual void UpPlate(Plate plate) { UpEmpty(); }
    //character 위에서 up했다
    public virtual void UpCharacter(Character character) { UpEmpty(); }
    //빈곳에서 up했다
    public virtual void UpEmpty()
    {
        GameManager.Instance.Hand.Sorting.Sort(null);
        Zoom.Insatnce.Active = true;
    }
}
