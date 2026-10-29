using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//캐릭터를 공격 대상으로 지정하기 위한 기능
//캐릭터를 끌어다 모험가의 플레이트에 놓으면 그 플레이트의 공격 대상이 바뀜
//(나중에 캐릭터 아이콘이 생기면 끌고 다니는 표시와 대상 표시를 아이콘으로 바꾸면 됨)
public class CharacterTarget : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private static readonly Color enemyColor = new(0.88f, 0.28f, 0.28f);
    private static readonly Color allyColor = new(0.3f, 0.75f, 0.35f);

    private Character character;
    //끌고 있을때 마우스를 따라다니는 표시
    private GameObject ghost;

    //캐릭터를 부르는 이름 (보는 쪽 기준으로 같은 편이면 아군, 상대편이면 적 + 앞에서부터 몇번째인지)
    //fromAdventurer : 모험가 쪽에서 보는지
    public static string Name(Character character, bool fromAdventurer)
    {
        bool adventurer = false;
        int index = GameManager.Instance.Enemys.IndexOf(character);
        if (index < 0)
        {
            index = GameManager.Instance.Adventurer.IndexOf(character);
            adventurer = true;
        }
        if (index < 0)
            return null;
        return (adventurer == fromAdventurer ? "아군 " : "적 ") + (index + 1);
    }

    //보는 쪽 기준으로 적이면 빨간색 아군이면 초록색
    public static Color ColorOf(Character character, bool fromAdventurer)
    {
        return GameManager.Instance.Adventurer.Contains(character) == fromAdventurer ? allyColor : enemyColor;
    }

    //플레이트 문장에 들어갈 대상 표시 (플레이트 주인 기준)
    public static string Text(Character target, Character self)
    {
        bool fromAdventurer = GameManager.Instance.Adventurer.Contains(self);
        string name = target != null ? Name(target, fromAdventurer) : null;
        if (name == null)
            return "<link=target>[대상 없음]</link>";
        return "<b><color=#" + ColorUtility.ToHtmlStringRGB(ColorOf(target, fromAdventurer)) + "><link=target>[" + name + "]</link></color></b>";
    }

    public void Init(Character character)
    {
        this.character = character;
    }

    //카드를 내는 단계에서만 대상을 바꿀 수 있음
    private bool CanDrag()
    {
        IPipeLine pipeLine = GameManager.Instance.PipeLine;
        return Name(character, true) != null && (pipeLine == null || pipeLine is PlayPipeLine);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!CanDrag())
            return;
        ghost = new GameObject("TargetGhost", typeof(RectTransform), typeof(Image), typeof(Outline));
        ghost.transform.SetParent(UIManager.Instance.Canvas.transform, false);
        ((RectTransform)ghost.transform).sizeDelta = new Vector2(130, 56);
        Image image = ghost.GetComponent<Image>();
        image.color = ColorOf(character, true);
        image.raycastTarget = false;
        ghost.GetComponent<Outline>().effectColor = new Color(0, 0, 0, 0.8f);
        TextMeshProUGUI text = RuntimeUI.CreateText(ghost.transform, "Name", 30);
        RuntimeUI.Stretch(text.rectTransform, Vector2.zero, Vector2.one);
        text.fontStyle = FontStyles.Bold;
        text.text = Name(character, true);
        RuntimeUI.ToFront(ghost);
        ghost.transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (ghost != null)
            ghost.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (ghost == null)
            return;
        Destroy(ghost);
        ghost = null;
        //모험가의 플레이트 위에 놓았다면 그 플레이트의 대상으로
        if (CanDrag() && UIManager.Instance.TryGetGraphicRayFindParent(out Plate plate)
            && GameManager.Instance.Adventurer.Contains(plate.Self))
        {
            plate.SetTarget(character);
        }
    }

    private void OnDisable()
    {
        if (ghost != null)
        {
            Destroy(ghost);
            ghost = null;
        }
    }
}
