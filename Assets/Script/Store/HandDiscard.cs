using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//손패에서 버릴 카드를 최대 max장까지 고르는 창
//손패의 카드는 그대로 두고 창에는 복사본을 보여줌
public static class HandDiscard
{
    //카드 한장이 차지하는 크기
    private static readonly Vector2 cellSize = new(200, 300);

    //onDone : 고른 손패 카드들 (아무것도 고르지 않을 수도 있음)
    public static void Open(int max, string title, Action<List<Card>> onDone)
    {
        List<Card> hand = new(GameManager.Instance.Hand.Cards);
        List<Card> selected = new();
        //손패가 없다면 고를게 없음
        if (hand.Count == 0)
        {
            onDone(selected);
            return;
        }

        //화면 전체를 어둡게 덮어서 뒤쪽 클릭을 막음
        GameObject root = RuntimeUI.CreateScreen("HandDiscard", new Color(0, 0, 0, 0.8f));

        TextMeshProUGUI text = RuntimeUI.CreateText(root.transform, "Title", 44);
        RuntimeUI.Stretch(text.rectTransform, new Vector2(0, 0.8f), new Vector2(1, 0.92f));

        GameObject content = new("Content", typeof(RectTransform), typeof(HorizontalLayoutGroup));
        content.transform.SetParent(root.transform, false);
        RuntimeUI.Stretch((RectTransform)content.transform, new Vector2(0.03f, 0.3f), new Vector2(0.97f, 0.75f));
        HorizontalLayoutGroup layout = content.GetComponent<HorizontalLayoutGroup>();
        layout.spacing = 30;
        layout.childAlignment = TextAnchor.MiddleCenter;
        layout.childControlWidth = false;
        layout.childControlHeight = false;
        layout.childForceExpandWidth = false;
        layout.childForceExpandHeight = false;

        Button confirm = RuntimeUI.CreateButton(root.transform, "", new Vector2(0.4f, 0.1f), new Vector2(0.6f, 0.18f));
        TextMeshProUGUI confirmLabel = confirm.GetComponentInChildren<TextMeshProUGUI>();

        void Refresh()
        {
            text.text = $"{title} ({selected.Count}/{max})";
            confirmLabel.text = selected.Count == 0 ? "버리지 않기" : $"{selected.Count}장 버리기";
        }

        //복사본 -> 손패의 카드
        Dictionary<Card, Card> original = new();
        for (int i = 0; i < hand.Count; i++)
        {
            GameObject cell = new("Cell", typeof(RectTransform));
            cell.transform.SetParent(content.transform, false);
            ((RectTransform)cell.transform).sizeDelta = cellSize;

            Card copy = UnityEngine.Object.Instantiate(hand[i], cell.transform);
            original[copy] = hand[i];
            RectTransform rect = (RectTransform)copy.transform;
            copy.transform.localPosition = Vector3.zero;
            copy.transform.localRotation = Quaternion.identity;
            float scale = Mathf.Min(cellSize.x / rect.rect.width, cellSize.y / rect.rect.height);
            copy.transform.localScale = Vector3.one * scale;
            copy.gameObject.SetActive(true);
            copy.Raycast = true;
            copy.State = new SelectState_Card(copy, c =>
            {
                if (selected.Remove(original[c]))
                {
                    Refresh();
                    return false;
                }
                if (selected.Count >= max)
                    return false;
                selected.Add(original[c]);
                Refresh();
                return true;
            });
        }

        confirm.onClick.AddListener(() =>
        {
            Zoom.Insatnce.ViewOFF();
            UnityEngine.Object.Destroy(root);
            onDone(selected);
        });

        Refresh();
        RuntimeUI.ToFront(root);
        Zoom.Insatnce.BringToFront();
    }
}
