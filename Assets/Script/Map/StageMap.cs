using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum StageType
{
    Battle,
    Store,
    Boss,
}

//맵의 칸 하나
public class StageNode
{
    public StageType Type { get; }
    //몇번째 층인지 (0부터)
    public int Floor { get; }
    //DungeonPipeLine에서 어떤 몬스터 구성을 쓸지
    public int Level { get; }
    //다음 층에서 갈 수 있는 칸들
    public List<StageNode> Next { get; } = new();

    public StageNode(StageType type, int floor, int level)
    {
        Type = type;
        Floor = floor;
        Level = level;
    }
}

//스테이지를 고르는 맵 (매 판 무작위로 생성)
//씬에 미리 배치할 필요 없이 코드로 UI를 만듬
public class StageMap : MonoBehaviour
{
    //보스를 포함한 층 수
    public const int FloorCount = 6;
    //층마다 칸 수
    private const int MinNodes = 2;
    private const int MaxNodes = 3;
    //층마다 상점이 나올 확률
    private static readonly float[] storeChance = { 0f, 0.3f, 0.3f, 0.3f, 0.5f };
    //층마다 전투의 몬스터 구성 (DungeonPipeLine의 case 번호)
    private static readonly int[] battleLevel = { 0, 1, 1, 2, 2 };
    //보스의 몬스터 구성
    private const int BossLevel = 3;

    //맵이 그려지는 영역 크기
    private static readonly Vector2 areaSize = new(1500, 860);
    private static readonly Vector2 nodeSize = new(150, 80);

    private readonly List<List<StageNode>> floors = new();
    private readonly Dictionary<StageNode, Button> buttons = new();
    private readonly List<(StageNode from, StageNode to, Image line)> lines = new();
    //지나온 칸들
    private readonly List<StageNode> visited = new();
    //현재 있는 칸 (아직 출발 전이라면 null)
    private StageNode current;
    public StageNode Current { get { return current; } }

    private RectTransform area;
    private Action<StageNode> onSelect;

    //지금 갈 수 있는 칸들
    private List<StageNode> Available
    {
        get { return current == null ? floors[0] : current.Next; }
    }

    //맵 만들기
    public static StageMap Create()
    {
        GameObject root = RuntimeUI.CreateScreen("StageMap", new Color(0.08f, 0.07f, 0.1f, 0.97f));
        StageMap map = root.AddComponent<StageMap>();

        TextMeshProUGUI title = RuntimeUI.CreateText(root.transform, "Title", 44);
        RuntimeUI.Stretch(title.rectTransform, new Vector2(0, 0.91f), new Vector2(1, 0.99f));
        title.text = "다음 스테이지를 고르세요";

        GameObject area = new("Area", typeof(RectTransform));
        area.transform.SetParent(root.transform, false);
        map.area = (RectTransform)area.transform;
        map.area.sizeDelta = areaSize;
        map.area.anchoredPosition = new Vector2(0, -30);

        map.Generate();
        map.Build();
        root.SetActive(false);
        return map;
    }

    //무작위 맵 생성
    private void Generate()
    {
        for (int f = 0; f < FloorCount; f++)
        {
            List<StageNode> floor = new();
            //마지막 층은 보스 하나
            if (f == FloorCount - 1)
            {
                floor.Add(new StageNode(StageType.Boss, f, BossLevel));
            }
            else
            {
                int count = Random.Range(MinNodes, MaxNodes + 1);
                for (int i = 0; i < count; i++)
                {
                    StageType type = Random.value < storeChance[f] ? StageType.Store : StageType.Battle;
                    floor.Add(new StageNode(type, f, battleLevel[f]));
                }
                //층마다 전투는 최소 하나
                if (floor.All(n => n.Type == StageType.Store))
                    floor[Random.Range(0, count)] = new StageNode(StageType.Battle, f, battleLevel[f]);
            }
            floors.Add(floor);
        }

        //층끼리 연결 (선이 서로 엇갈리지 않도록 비슷한 위치끼리)
        for (int f = 0; f < FloorCount - 1; f++)
        {
            List<StageNode> a = floors[f];
            List<StageNode> b = floors[f + 1];
            for (int i = 0; i < a.Count; i++)
            {
                int j = Near(i, a.Count, b.Count);
                Connect(a[i], b[j]);
                //가끔 옆 칸으로도 갈래길
                int side = j + (Random.value < 0.5f ? -1 : 1);
                if (side >= 0 && side < b.Count && Random.value < 0.5f)
                    Connect(a[i], b[side]);
            }
            //다음 층의 모든 칸은 어딘가에서 갈 수 있어야 함
            for (int j = 0; j < b.Count; j++)
            {
                if (!a.Any(n => n.Next.Contains(b[j])))
                    Connect(a[Near(j, b.Count, a.Count)], b[j]);
            }
        }
    }

    //from층의 index번째 칸과 위치가 가장 비슷한 to층의 칸 번호
    private static int Near(int index, int from, int to)
    {
        if (from <= 1)
            return (to - 1) / 2;
        return Mathf.RoundToInt(index * (to - 1) / (float)(from - 1));
    }

    private static void Connect(StageNode from, StageNode to)
    {
        if (!from.Next.Contains(to))
            from.Next.Add(to);
    }

    //칸의 화면상 위치 (아래층이 0층)
    private Vector2 Position(StageNode node)
    {
        List<StageNode> floor = floors[node.Floor];
        float x = (floor.IndexOf(node) + 1f) / (floor.Count + 1f) * areaSize.x - areaSize.x * 0.5f;
        float y = node.Floor / (FloorCount - 1f) * areaSize.y - areaSize.y * 0.5f;
        return new Vector2(x, y);
    }

    //UI 만들기
    private void Build()
    {
        //선을 먼저 만들어서 칸 뒤에 그려지도록
        foreach (StageNode from in floors.SelectMany(f => f))
        {
            foreach (StageNode to in from.Next)
            {
                GameObject obj = new("Line", typeof(RectTransform), typeof(Image));
                obj.transform.SetParent(area, false);
                RectTransform rect = (RectTransform)obj.transform;
                Vector2 a = Position(from);
                Vector2 b = Position(to);
                rect.pivot = new Vector2(0, 0.5f);
                rect.anchoredPosition = a;
                rect.sizeDelta = new Vector2(Vector2.Distance(a, b), 6);
                rect.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(b.y - a.y, b.x - a.x) * Mathf.Rad2Deg);
                Image image = obj.GetComponent<Image>();
                image.raycastTarget = false;
                lines.Add((from, to, image));
            }
        }

        foreach (StageNode node in floors.SelectMany(f => f))
        {
            Button button = RuntimeUI.CreateButton(area, NodeName(node.Type), NodeColor(node.Type));
            RectTransform rect = (RectTransform)button.transform;
            rect.sizeDelta = node.Type == StageType.Boss ? nodeSize * 1.4f : nodeSize;
            rect.anchoredPosition = Position(node);
            StageNode target = node;
            button.onClick.AddListener(() => Select(target));
            buttons.Add(node, button);
        }
    }

    private static string NodeName(StageType type)
    {
        return type switch
        {
            StageType.Store => "상점",
            StageType.Boss => "보스",
            _ => "전투",
        };
    }

    private static Color NodeColor(StageType type)
    {
        return type switch
        {
            StageType.Store => new Color(0.85f, 0.65f, 0.2f),
            StageType.Boss => new Color(0.75f, 0.15f, 0.2f),
            _ => new Color(0.35f, 0.45f, 0.7f),
        };
    }

    //맵을 보여주고 칸을 고르면 onSelect 호출
    public void Show(Action<StageNode> onSelect)
    {
        this.onSelect = onSelect;
        List<StageNode> available = Available;
        foreach (var pair in buttons)
        {
            bool canGo = available.Contains(pair.Key);
            bool done = visited.Contains(pair.Key);
            pair.Value.interactable = canGo;
            Image image = pair.Value.GetComponent<Image>();
            Color color = NodeColor(pair.Key.Type);
            //갈 수 있는 칸은 밝게, 지나온 칸은 어둡게, 나머지는 흐리게
            image.color = canGo ? color : done ? color * 0.45f : new Color(color.r, color.g, color.b, 0.35f);
            pair.Value.transform.localScale = canGo ? Vector3.one * 1.1f : Vector3.one;
        }
        foreach (var (from, to, line) in lines)
        {
            bool path = visited.Contains(from) && visited.Contains(to);
            bool next = from == current && available.Contains(to);
            line.color = path ? new Color(1, 1, 1, 0.9f) : next ? new Color(1, 0.9f, 0.5f, 0.9f) : new Color(1, 1, 1, 0.15f);
        }
        RuntimeUI.ToFront(gameObject);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Select(StageNode node)
    {
        if (!Available.Contains(node))
            return;
        current = node;
        visited.Add(node);
        Hide();
        onSelect?.Invoke(node);
    }
}
