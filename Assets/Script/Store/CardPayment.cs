using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//상점에서 비용으로 낼 카드를 덱에서 직접 고르는 창
//씬에 미리 배치할 필요 없이 처음 열 때 코드로 UI를 만듬
public class CardPayment : MonoBehaviour
{
    private static CardPayment instance;

    //구매 후에도 모험가마다 덱에 남아있어야 하는 최소 카드 수
    public const int MinDeck = 2;

    //카드 한장이 그리드에서 차지하는 크기
    private static readonly Vector2 cellSize = new(160, 240);

    private TextMeshProUGUI title;
    private RectTransform content;
    private Button confirm;

    //지불할 가치
    private int price;
    //지불이 끝났을 때 호출
    private Action onPaid;
    //창에 꺼내놓은 덱의 카드들
    private readonly List<Card> cards = new();
    //선택된 카드들
    private readonly List<Card> selected = new();
    //카드를 담고 있는 칸
    private readonly List<GameObject> cells = new();

    //모든 모험가 덱의 전체 카드 (사용 전 + 사용 후, 모험가 순서대로)
    public static List<Card> DeckCards
    {
        get { return GameManager.Instance.Dummy.Cards; }
    }

    //모험가마다 최소 덱 수를 남기고 낼 수 있는 카드들로 가치 합 price 이상을 만들 수 있는지
    public static bool CanPay(int price)
    {
        int sum = 0;
        foreach (CharacterDeck deck in GameManager.Instance.Dummy.Decks)
        {
            List<Card> cards = deck.Cards;
            sum += cards.Select(c => c.Worth).Where(w => w > 0).OrderByDescending(w => w).Take(cards.Count - MinDeck).Sum();
        }
        return sum >= price;
    }

    //선택한 카드들의 가치 합
    private int SelectedWorth { get { return selected.Sum(c => c.Worth); } }
    //이 카드의 덱에서 더 고를 수 있는지 (그 모험가의 덱에 최소 덱 수는 남아야 함)
    private bool CanSelect(Card card)
    {
        int deckCount = cards.Count(c => c.Deck == card.Deck);
        int selectedCount = selected.Count(c => c.Deck == card.Deck);
        return selectedCount < deckCount - MinDeck;
    }

    //창 열기
    public static void Open(int price, Action onPaid)
    {
        if (!CanPay(price))
            return;
        if (instance == null)
            instance = Create();
        instance.Show(price, onPaid);
    }

    private void Show(int price, Action onPaid)
    {
        this.price = price;
        this.onPaid = onPaid;
        selected.Clear();

        cards.AddRange(DeckCards);

        for (int i = 0; i < cards.Count; i++)
        {
            //칸을 만들고 그 안에 카드를 크기에 맞춰 넣기
            GameObject cell = new("Cell", typeof(RectTransform));
            cell.transform.SetParent(content, false);
            cells.Add(cell);

            Card card = cards[i];
            RectTransform rect = (RectTransform)card.transform;
            card.transform.SetParent(cell.transform, false);
            card.transform.localPosition = Vector3.zero;
            float scale = Mathf.Min(cellSize.x / rect.rect.width, cellSize.y / rect.rect.height);
            card.transform.localScale = Vector3.one * scale;
            card.gameObject.SetActive(true);
            card.Raycast = true;
            card.State = new PaymentState_Card(card, this);

            //어느 모험가의 카드인지 표시
            TextMeshProUGUI owner = RuntimeUI.CreateText(cell.transform, "Owner", 24);
            RectTransform ownerRect = owner.rectTransform;
            ownerRect.anchorMin = new Vector2(0.5f, 1);
            ownerRect.anchorMax = new Vector2(0.5f, 1);
            ownerRect.pivot = new Vector2(0.5f, 0);
            ownerRect.sizeDelta = new Vector2(cellSize.x, 28);
            ownerRect.anchoredPosition = new Vector2(0, 2);
            owner.text = card.Deck != null && card.Deck.Owner != null ? card.Deck.Owner.JobName : "";
        }

        //다른 UI보다 위에, 카드 확대(Zoom)보다는 아래에 보이도록
        RuntimeUI.ToFront(gameObject);
        Zoom.Insatnce.BringToFront();
        gameObject.SetActive(true);
        Refresh();
    }

    //카드를 클릭했을 때 선택/해제
    public bool Toggle(Card card)
    {
        if (selected.Remove(card))
        {
            Refresh();
            return false;
        }
        if (!CanSelect(card))
            return false;
        selected.Add(card);
        Refresh();
        return true;
    }

    private void Refresh()
    {
        title.text = $"비용으로 낼 카드를 선택하세요 (가치 {SelectedWorth}/{price})\n<size=26>모험가마다 덱에 최소 {MinDeck}장은 남아야 하고, 넘친 가치는 돌려받지 못함</size>";
        confirm.interactable = SelectedWorth >= price;
    }

    private void OnConfirm()
    {
        //선택한 카드들은 덱에서 제거
        for (int i = 0; i < selected.Count; i++)
        {
            GameManager.Instance.Dummy.Remove(selected[i]);
            cards.Remove(selected[i]);
            Destroy(selected[i].gameObject);
        }
        Close();
        onPaid?.Invoke();
    }

    private void Close()
    {
        //남은 카드들은 다시 덱으로
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].State = null;
            cards[i].transform.SetParent(GameManager.Instance.Dummy.transform, false);
            cards[i].transform.localScale = Vector3.one;
            cards[i].gameObject.SetActive(false);
        }
        cards.Clear();
        selected.Clear();
        cells.ForEach(c => Destroy(c));
        cells.Clear();
        Zoom.Insatnce.ViewOFF();
        gameObject.SetActive(false);
    }

    //UI 생성
    private static CardPayment Create()
    {
        //화면 전체를 어둡게 덮어서 뒤쪽 클릭을 막음
        GameObject root = RuntimeUI.CreateScreen("CardPayment", new Color(0, 0, 0, 0.8f));
        CardPayment payment = root.AddComponent<CardPayment>();

        payment.title = RuntimeUI.CreateText(root.transform, "Title", 40);
        RuntimeUI.Stretch(payment.title.rectTransform, new Vector2(0, 0.88f), new Vector2(1, 0.98f));

        //카드 목록 (많으면 스크롤)
        GameObject view = new("View", typeof(RectTransform), typeof(Image), typeof(RectMask2D), typeof(ScrollRect));
        view.transform.SetParent(root.transform, false);
        RuntimeUI.Stretch((RectTransform)view.transform, new Vector2(0.05f, 0.15f), new Vector2(0.95f, 0.86f));
        view.GetComponent<Image>().color = new Color(1, 1, 1, 0.05f);

        GameObject content = new("Content", typeof(RectTransform), typeof(GridLayoutGroup), typeof(ContentSizeFitter));
        content.transform.SetParent(view.transform, false);
        RectTransform contentRect = (RectTransform)content.transform;
        contentRect.anchorMin = new Vector2(0, 1);
        contentRect.anchorMax = new Vector2(1, 1);
        contentRect.pivot = new Vector2(0.5f, 1);
        contentRect.sizeDelta = Vector2.zero;
        GridLayoutGroup grid = content.GetComponent<GridLayoutGroup>();
        grid.cellSize = cellSize;
        grid.spacing = new Vector2(20, 40);
        grid.padding = new RectOffset(20, 20, 40, 20);
        grid.childAlignment = TextAnchor.UpperCenter;
        content.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        payment.content = contentRect;

        ScrollRect scroll = view.GetComponent<ScrollRect>();
        scroll.content = contentRect;
        scroll.horizontal = false;
        scroll.movementType = ScrollRect.MovementType.Clamped;
        scroll.scrollSensitivity = 30;

        payment.confirm = RuntimeUI.CreateButton(root.transform, "확인", new Vector2(0.55f, 0.03f), new Vector2(0.7f, 0.11f));
        payment.confirm.onClick.AddListener(payment.OnConfirm);
        Button cancel = RuntimeUI.CreateButton(root.transform, "취소", new Vector2(0.3f, 0.03f), new Vector2(0.45f, 0.11f));
        cancel.onClick.AddListener(payment.Close);

        root.SetActive(false);
        return payment;
    }
}
