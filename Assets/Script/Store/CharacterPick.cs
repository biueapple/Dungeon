using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//모험가 중 한명을 고르는 창 (렐릭을 장착할 캐릭터 선택 등)
//씬에 미리 배치할 필요 없이 열 때 코드로 UI를 만듬
public static class CharacterPick
{
    //한 캐릭터가 차지하는 칸의 크기
    private static readonly Vector2 cellSize = new(260, 340);

    //filter : 고를 수 있는 모험가 (null이면 모두), preview : 창 위쪽에 보여줄 카드 (무엇을 넣는지)
    public static void Open(string title, Action<Character> onPick, Predicate<Character> filter = null, Card preview = null)
    {
        //화면 전체를 어둡게 덮어서 뒤쪽 클릭을 막음
        GameObject root = RuntimeUI.CreateScreen("CharacterPick", new Color(0, 0, 0, 0.8f));

        TextMeshProUGUI text = RuntimeUI.CreateText(root.transform, "Title", 44);
        RuntimeUI.Stretch(text.rectTransform, new Vector2(0, 0.85f), new Vector2(1, 0.95f));
        text.text = title;

        //넣을 카드 보여주기
        if (preview != null)
        {
            GameObject holder = new("Preview", typeof(RectTransform));
            holder.transform.SetParent(root.transform, false);
            RuntimeUI.Stretch((RectTransform)holder.transform, new Vector2(0.5f, 0.55f), new Vector2(0.5f, 0.84f));
            Card card = UnityEngine.Object.Instantiate(preview, holder.transform);
            card.State = null;
            card.Raycast = false;
            RectTransform rect = (RectTransform)card.transform;
            card.transform.localPosition = Vector3.zero;
            float height = ((RectTransform)UIManager.Instance.Canvas.transform).rect.height * 0.29f;
            card.transform.localScale = Vector3.one * (height / rect.rect.height);
            card.gameObject.SetActive(true);
        }

        GameObject content = new("Content", typeof(RectTransform), typeof(HorizontalLayoutGroup));
        content.transform.SetParent(root.transform, false);
        RuntimeUI.Stretch((RectTransform)content.transform, new Vector2(0.05f, 0.1f), new Vector2(0.95f, 0.52f));
        HorizontalLayoutGroup layout = content.GetComponent<HorizontalLayoutGroup>();
        layout.spacing = 60;
        layout.childAlignment = TextAnchor.MiddleCenter;
        layout.childControlWidth = false;
        layout.childControlHeight = false;
        layout.childForceExpandWidth = false;
        layout.childForceExpandHeight = false;

        for (int i = 0; i < GameManager.Instance.Adventurer.Count; i++)
        {
            Character character = GameManager.Instance.Adventurer[i];
            if (filter != null && !filter(character))
                continue;
            Button button = RuntimeUI.CreateButton(content.transform, "", new Color(0.25f, 0.25f, 0.3f));
            ((RectTransform)button.transform).sizeDelta = cellSize;

            //캐릭터 그림
            PlayAnimation animation = character.GetComponentInChildren<PlayAnimation>();
            Image source = animation != null ? animation.GetComponent<Image>() : null;
            if (source != null && source.sprite != null)
            {
                GameObject picture = new("Picture", typeof(RectTransform), typeof(Image));
                picture.transform.SetParent(button.transform, false);
                RuntimeUI.Stretch((RectTransform)picture.transform, new Vector2(0.1f, 0.25f), new Vector2(0.9f, 0.95f));
                Image image = picture.GetComponent<Image>();
                image.sprite = source.sprite;
                image.preserveAspect = true;
                image.raycastTarget = false;
            }

            //몇번째 모험가인지와 이름
            TextMeshProUGUI label = button.GetComponentInChildren<TextMeshProUGUI>();
            RuntimeUI.Stretch(label.rectTransform, new Vector2(0, 0.02f), new Vector2(1, 0.25f));
            label.fontSize = 28;
            label.text = "아군 " + (i + 1) + "\n<size=22>" + character.JobName + "</size>";

            button.onClick.AddListener(() =>
            {
                UnityEngine.Object.Destroy(root);
                onPick(character);
            });
        }

        RuntimeUI.ToFront(root);
    }
}
