using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//씬에 미리 배치하지 않고 코드로 UI를 만들때 쓰는 공용 함수들
public static class RuntimeUI
{
    //한글이 나오는 폰트 (카드에 쓰인 폰트를 가져옴)
    public static TMP_FontAsset Font
    {
        get
        {
            Card card = GameManager.Instance.Dummy.Before.Concat(GameManager.Instance.Dummy.After).FirstOrDefault();
            return card != null ? card.Font : null;
        }
    }

    //화면 전체를 덮는 창 (뒤쪽 클릭을 막음)
    public static GameObject CreateScreen(string name, Color color)
    {
        GameObject root = new(name, typeof(RectTransform), typeof(Image));
        root.transform.SetParent(UIManager.Instance.Canvas.transform, false);
        Stretch((RectTransform)root.transform, Vector2.zero, Vector2.one);
        root.GetComponent<Image>().color = color;
        return root;
    }

    //다른 UI들보다 앞에 보이도록
    public static void ToFront(GameObject screen)
    {
        screen.transform.SetAsLastSibling();
    }

    public static void Stretch(RectTransform rect, Vector2 min, Vector2 max)
    {
        rect.anchorMin = min;
        rect.anchorMax = max;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }

    public static TextMeshProUGUI CreateText(Transform parent, string name, float size)
    {
        GameObject obj = new(name, typeof(RectTransform), typeof(TextMeshProUGUI));
        obj.transform.SetParent(parent, false);
        TextMeshProUGUI text = obj.GetComponent<TextMeshProUGUI>();
        TMP_FontAsset font = Font;
        if (font != null)
            text.font = font;
        text.fontSize = size;
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.white;
        text.raycastTarget = false;
        return text;
    }

    public static Button CreateButton(Transform parent, string label, Color color)
    {
        GameObject obj = new(label, typeof(RectTransform), typeof(Image), typeof(Button));
        obj.transform.SetParent(parent, false);
        obj.GetComponent<Image>().color = color;
        TextMeshProUGUI text = CreateText(obj.transform, "Label", 32);
        Stretch(text.rectTransform, Vector2.zero, Vector2.one);
        text.text = label;
        return obj.GetComponent<Button>();
    }

    public static Button CreateButton(Transform parent, string label, Vector2 min, Vector2 max)
    {
        Button button = CreateButton(parent, label, new Color(0.25f, 0.25f, 0.3f));
        Stretch((RectTransform)button.transform, min, max);
        return button;
    }
}
