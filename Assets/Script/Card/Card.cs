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

    //카드의 가치 (상점에서 비용으로 낼 때 사용) -1, 1, 2, 3 중 하나, 0이면 가치 없음
    public virtual int Worth { get { return 0; } }

    //아무 효과가 없는 카드인지 (손패에 남아있어도 턴을 넘길 수 있고 턴이 끝나면 사용한 덱으로)
    public virtual bool NoEffect { get { return false; } }

    //카드 아래쪽에 박혀있는 가치를 나타내는 보석
    private Image gem;
    private TextMeshProUGUI gemText;
    private bool gemReady = false;
    private static readonly Dictionary<int, Color> gemColor = new()
    {
        { -1, new Color(0.3f, 0.22f, 0.32f) },
        { 1, new Color(0.3f, 0.8f, 0.4f) },
        { 2, new Color(0.3f, 0.55f, 1f) },
        { 3, new Color(0.95f, 0.25f, 0.35f) },
    };

    //카드의 백그라운드
    [SerializeField] 
    protected Image back;
    //카드의 아이콘
    [SerializeField]
    protected Image icon;
    public Sprite Icon { get { return icon.sprite; } set { icon.sprite = value; } }
    //카드의 설명이 입력되어 있는 컴포넌트
    [SerializeField]
    protected TextMeshProUGUI textComponent;
    public string Description { get { return textComponent.text; } set { textComponent.text = value; } }
    public TMP_FontAsset Font { get { return textComponent.font; } }

    public bool Raycast { get { return back.raycastTarget; } set { back.raycastTarget = value; } }
    //카드 백그라운드 색 (상점에서 비용으로 선택됐는지 표시할때 사용)
    public Color BackColor { get { return back.color; } set { back.color = value; } }

    //카드가 어떤 상황에 어떻게 반응해야 하는지 경우가 많아져서 state로 나누기로 함
    //지금 카드가 무슨 상황인지 어떻게 반응해야 하는지
    [SerializeField]
    protected IState_Card state;
    public IState_Card State { get { return state; } set { state?.Exit(); state = value; state?.Enter(); } }   

    private void OnEnable()
    {
        //처음 보여질때 자신의 가치로 보석 박기 (확대 카드처럼 밖에서 정해준 경우는 그대로)
        if (!gemReady)
            Gem = Worth;
    }

    //보석이 나타낼 가치 (0이면 보석 없음)
    public int Gem
    {
        set
        {
            gemReady = true;
            if (value == 0)
            {
                if (gem != null)
                    gem.gameObject.SetActive(false);
                return;
            }
            if (gem == null)
                CreateGem();
            gem.gameObject.SetActive(true);
            gem.color = gemColor.TryGetValue(value, out Color color) ? color : Color.white;
            gemText.text = value.ToString();
        }
    }

    //카드 아래쪽 가운데에 마름모 모양 보석 만들기
    private void CreateGem()
    {
        RectTransform rect = (RectTransform)transform;
        float size = rect.rect.width * 0.22f;

        GameObject obj = new("Gem", typeof(RectTransform), typeof(Image), typeof(Outline));
        obj.transform.SetParent(transform, false);
        RectTransform gemRect = (RectTransform)obj.transform;
        gemRect.anchorMin = new Vector2(0.5f, 0);
        gemRect.anchorMax = new Vector2(0.5f, 0);
        gemRect.anchoredPosition = new Vector2(0, size * 0.5f);
        gemRect.sizeDelta = new Vector2(size, size);
        gemRect.localRotation = Quaternion.Euler(0, 0, 45);
        gem = obj.GetComponent<Image>();
        gem.raycastTarget = false;
        Outline outline = obj.GetComponent<Outline>();
        outline.effectColor = new Color(0, 0, 0, 0.8f);
        outline.effectDistance = new Vector2(2, -2);

        GameObject textObj = new("Worth", typeof(RectTransform), typeof(TextMeshProUGUI));
        textObj.transform.SetParent(obj.transform, false);
        RectTransform textRect = (RectTransform)textObj.transform;
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;
        //보석은 돌아가 있지만 글자는 똑바로
        textRect.localRotation = Quaternion.Euler(0, 0, -45);
        gemText = textObj.GetComponent<TextMeshProUGUI>();
        gemText.font = textComponent.font;
        gemText.fontSize = size * 0.6f;
        gemText.fontStyle = FontStyles.Bold;
        gemText.alignment = TextAlignmentOptions.Center;
        gemText.color = Color.white;
        gemText.raycastTarget = false;
    }

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
