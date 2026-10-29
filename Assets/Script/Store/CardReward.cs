using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

//전투를 클리어하면 골드 카드와 무작위 카드 중 원하는 만큼 골라 모험가의 덱에 추가하는 창
//씬에 미리 배치할 필요 없이 처음 열 때 코드로 UI를 만듬
public class CardReward : MonoBehaviour
{
    private static CardReward instance;

    //항상 나오는 골드 카드
    private const string GoldCard = "Adventurer/Gold";
    //무작위로 나올 수 있는 카드가 들어있는 폴더
    private const string PoolFolder = "Card/Adventurer";
    //무작위 카드 수
    private const int MinRandom = 2;
    private const int MaxRandom = 3;

    //카드 한장이 차지하는 크기
    private static readonly Vector2 cellSize = new(240, 360);

    private RectTransform content;
    //보상이 끝났을 때 호출 (골랐든 건너뛰었든)
    private Action onDone;
    //보상으로 보여주는 카드들
    private readonly List<Card> cards = new();
    //선택된 카드들
    private readonly List<Card> selected = new();
    //확인 버튼의 글자 (몇장 받는지 표시)
    private TextMeshProUGUI confirmLabel;
    //카드를 담고 있는 칸
    private readonly List<GameObject> cells = new();

    //창 열기
    public static void Open(Action onDone)
    {
        if (instance == null)
            instance = Create();
        instance.Show(onDone);
    }

    private void Show(Action onDone)
    {
        this.onDone = onDone;

        //골드 카드 + 서로 다른 무작위 카드들
        Card gold = Resources.Load<Card>("Card/" + GoldCard);
        List<Card> list = new() { gold };
        list.AddRange(Pool(gold).OrderBy(_ => Random.value).Take(Random.Range(MinRandom, MaxRandom + 1)));

        for (int i = 0; i < list.Count; i++)
        {
            //칸을 만들고 그 안에 카드를 크기에 맞춰 넣기
            GameObject cell = new("Cell", typeof(RectTransform));
            cell.transform.SetParent(content, false);
            cells.Add(cell);

            Card card = Instantiate(list[i], cell.transform);
            RectTransform rect = (RectTransform)card.transform;
            card.transform.localPosition = Vector3.zero;
            float scale = Mathf.Min(cellSize.x / rect.rect.width, cellSize.y / rect.rect.height);
            card.transform.localScale = Vector3.one * scale;
            card.gameObject.SetActive(true);
            card.Raycast = true;
            card.State = new RewardState_Card(card, this);
            cards.Add(card);
        }

        //다른 UI보다 위에, 카드 확대(Zoom)보다는 아래에 보이도록
        RuntimeUI.ToFront(gameObject);
        Zoom.Insatnce.BringToFront();
        gameObject.SetActive(true);
        Refresh();
    }

    //보상으로 나올 수 있는 카드들 (골드, 휘발성 카드, 가치가 마이너스인 카드, 살아있는 모험가 누구도 가질 수 없는 전용 카드는 제외)
    private static IEnumerable<Card> Pool(Card gold)
    {
        return Resources.LoadAll<Card>(PoolFolder).Where(c => c != gold && !c.Volatility && c.Worth >= 0
            && GameManager.Instance.Adventurer.Any(c.CanBelongTo));
    }

    //카드를 클릭했을 때 선택/해제 (원하는 만큼 고를 수 있음)
    public bool Toggle(Card card)
    {
        bool chosen = !selected.Remove(card);
        if (chosen)
            selected.Add(card);
        Refresh();
        return chosen;
    }

    private void Refresh()
    {
        confirmLabel.text = selected.Count == 0 ? "받지 않기" : $"{selected.Count}장 받기";
    }

    //선택한 카드들을 한장씩 넣을 모험가를 골라서 그 덱에 추가
    private void OnConfirm()
    {
        //선택 표시(색)를 원래대로
        selected.ForEach(c => c.State = null);
        gameObject.SetActive(false);
        Zoom.Insatnce.ViewOFF();
        if (selected.Count > 0)
            SoundManager.Instance.Play(SoundManager.Instance.Coin);
        AssignNext(0);
    }

    private void AssignNext(int index)
    {
        if (index >= selected.Count)
        {
            Close();
            return;
        }
        Card card = selected[index];
        List<Character> owners = GameManager.Instance.Adventurer.Where(card.CanBelongTo).ToList();
        //넣을 수 있는 모험가가 한명뿐이면 바로
        if (owners.Count <= 1)
        {
            if (owners.Count == 1)
                Give(card, owners[0]);
            AssignNext(index + 1);
            return;
        }
        string count = selected.Count > 1 ? $" ({index + 1}/{selected.Count})" : "";
        CharacterPick.Open("이 카드를 넣을 모험가를 고르세요" + count, owner =>
        {
            Give(card, owner);
            AssignNext(index + 1);
        }, card.CanBelongTo, card);
    }

    //카드를 owner의 덱에 추가
    private void Give(Card card, Character owner)
    {
        cards.Remove(card);
        card.transform.localScale = Vector3.one;
        //사용한 카드 리스트에 넣어서 다음에 덱을 섞을 때 들어오도록
        GameManager.Instance.Dummy.AddCard(card, owner);
    }

    private void Close()
    {
        //고르지 않은 카드들은 삭제
        cards.ForEach(c => Destroy(c.gameObject));
        cards.Clear();
        selected.Clear();
        cells.ForEach(c => Destroy(c));
        cells.Clear();
        Zoom.Insatnce.ViewOFF();
        gameObject.SetActive(false);
        onDone?.Invoke();
    }

    //UI 생성
    private static CardReward Create()
    {
        //화면 전체를 어둡게 덮어서 뒤쪽 클릭을 막음
        GameObject root = RuntimeUI.CreateScreen("CardReward", new Color(0, 0, 0, 0.8f));
        CardReward reward = root.AddComponent<CardReward>();

        TextMeshProUGUI title = RuntimeUI.CreateText(root.transform, "Title", 44);
        RuntimeUI.Stretch(title.rectTransform, new Vector2(0, 0.8f), new Vector2(1, 0.92f));
        title.text = "전투 승리! 덱에 추가할 카드를 원하는 만큼 고르세요";

        GameObject content = new("Content", typeof(RectTransform), typeof(HorizontalLayoutGroup));
        content.transform.SetParent(root.transform, false);
        RuntimeUI.Stretch((RectTransform)content.transform, new Vector2(0.05f, 0.3f), new Vector2(0.95f, 0.75f));
        HorizontalLayoutGroup layout = content.GetComponent<HorizontalLayoutGroup>();
        layout.spacing = 60;
        layout.childAlignment = TextAnchor.MiddleCenter;
        layout.childControlWidth = false;
        layout.childControlHeight = false;
        layout.childForceExpandWidth = false;
        layout.childForceExpandHeight = false;
        reward.content = (RectTransform)content.transform;

        //아무것도 고르지 않고 누르면 건너뛰기
        Button confirm = RuntimeUI.CreateButton(root.transform, "받기", new Vector2(0.425f, 0.1f), new Vector2(0.575f, 0.18f));
        confirm.onClick.AddListener(reward.OnConfirm);
        reward.confirmLabel = confirm.GetComponentInChildren<TextMeshProUGUI>();

        root.SetActive(false);
        return reward;
    }
}
