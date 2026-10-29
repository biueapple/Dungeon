using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public abstract class Plate : MonoBehaviour , IPointerClickHandler
{
    //텍스트 컴포넌트
    protected TextLinkClickHandler textLinkClickHandler;
    public TextLinkClickHandler TextLinkClickHandler { get { return textLinkClickHandler; } set {  textLinkClickHandler = value; } }
    
    //입력된 카드를 정렬해주는 공간
    [SerializeField]
    protected Tool[] tool;
    public Tool[] Tool { get { return tool; } }

    //적용될 키워드들
    protected Keyword[] keywords;
    public Keyword[] Keyword { get { return keywords; } }

    ////적군
    //protected List<Character> foe;
    //public List<Character> Foe { get {  return foe; } set {  foe = value; } }
    ////아군
    //protected List<Character> ally;
    //public List<Character> Ally { get { return ally; } set { ally = value; } }
    //자신
    protected Character self;
    public Character Self { get { return self; } set {  self = value; } }

    //덱
    protected Dummy dummy;
    public Dummy Dummy { get { return dummy; } }

    //공격 대상을 정하는 플레이트인지 (Init에서 설정)
    protected bool useTarget = false;
    //공격 대상 (모험가의 플레이트는 캐릭터를 끌어다 놓아서 바꿀 수 있음)
    private Character target;
    //문장에 보이고 있는 대상 (대상이나 몇번째인지가 바뀌면 문장을 다시 만듬)
    private string targetText;

    //이 플레이트의 상대편
    protected List<Character> Opponents
    {
        get { return GameManager.Instance.Adventurer.Contains(self) ? GameManager.Instance.Enemys : GameManager.Instance.Adventurer; }
    }

    //공격할 대상 (정해진 대상이 없거나 죽었다면 기본 대상으로)
    protected Character Target
    {
        get
        {
            if (target == null || !Opponents.Contains(target))
                target = DefaultTarget();
            return target;
        }
    }

    //플레이트 문장에 넣을 대상 표시
    protected string TargetText { get { return CharacterTarget.Text(Target, self); } }

    //기본 대상 : 모험가는 가장 앞의 적, 적은 모험가 중 무작위
    protected virtual Character DefaultTarget()
    {
        List<Character> opponents = Opponents;
        if (opponents.Count == 0)
            return null;
        if (GameManager.Instance.Adventurer.Contains(self))
            return opponents[0];
        return opponents[UnityEngine.Random.Range(0, opponents.Count)];
    }

    //플레이트가 새로 생성될때 대상을 다시 정하도록
    public void ResetTarget()
    {
        target = null;
    }

    //캐릭터를 끌어다 놓았을때 대상으로 지정 (상대편만 가능)
    public bool SetTarget(Character character)
    {
        if (!useTarget || !Opponents.Contains(character))
            return false;
        target = character;
        Texting();
        return true;
    }

    private void Awake()
    {
        textLinkClickHandler = GetComponent<TextLinkClickHandler>();
    }

    private void Start()
    {
        dummy = GameManager.Instance.Dummy;
        //혹시 초기화 해야할 거 있으면 override해서 쓰라고
        Init();
        //키워드 설정
        Texting();
    }

    //
    private void LateUpdate()
    {
        //대상이 바뀌었거나 앞의 캐릭터가 죽어서 몇번째인지 바뀌었다면 문장 다시 만들기
        if (useTarget && keywords != null && TargetText != targetText)
        {
            targetText = TargetText;
            Texting();
        }

        //우클릭으로 모든 키워드에 있는 카드를 보여주지 않도록
        if(Mouse.current.rightButton.wasPressedThisFrame)
        {
            //SpaceView(-1, Vector2.zero);
            Zoom.Insatnce.ViewTool(this, -1, Vector2.zero);
        }
    }

    //혹시 초기화 해야할 거 있으면 override해서 쓰라고
    protected abstract void Init();

    //이 플레이트에 무슨 텍스트와 키워드가 있는지 설정
    protected abstract void Texting();
    //플레이트의 능력들
    public abstract void Defense();
    public abstract void Attack();
    public virtual void Function()
    {
        GameManager.Instance.CyclePipeLine.Function.Characters.Add(self);
    }

    //모든 적용된 카드들을 초기화시키는 작업
    public void KeywordInit()
    {
        //모든 키워드에 들어있는 카드들을 덱의 사용된 공간으로 옮기기
        for (int i = 0; i < keywords.Length; i++)
        {
            if (dummy != null)
            {
                for (int j = 0; j < keywords[i].Space.Count; j++)
                {
                    dummy.AfterCard(keywords[i].Space[j]);
                }
            }

            keywords[i].Clear();
        }
        Texting();
    }

    public void ToolDeactive()
    {
        //카드들이 보이지 않도록
        for (int i = 0; i < keywords.Length; i++)
        {
            if (dummy != null)
            {
                for (int j = 0; j < keywords[i].Space.Count; j++)
                {
                    keywords[i].Space[j].gameObject.SetActive(false);
                }
            }
        }
        //카드가 정렬되어 있던 툴을 비활성화 해서 보이지 않도록 (카드가 parent가 tool이 아님)
        for (int i = 0; i < tool.Length; i++)
        {
            tool[i].gameObject.SetActive(false);
        }
    }

    //card에서 호출하는 함수 카드가 어느 키워드에 들어가야 한다는 함수
    public bool InputKeyword(Card card)
    {
        //카드가 plate 위에서 up했으면서 keyword 위에서 up한 경우
        if(textLinkClickHandler.OnPointerClick(out int index, out Vector3 position))
        {
            //모험가인경우에만 적용
            if(card.Friendly && GameManager.Instance.Adventurer.Contains(self))
            {
                //키워드에 추가
                if (keywords[index].Add(card))
                {
                    //키워드에 들어가 있는 카드들 보여주기
                    Zoom.Insatnce.ViewTool(this, index, position);
                    //정렬
                    keywords[index].Tool.Sorting.Sort(null);
                    //키워드가 변화 했을테니까 다시 텍스트 로드
                    Texting();
                    return true;
                }
            }
            //적인 경우에만 적용
            else if(!card.Friendly && GameManager.Instance.Enemys.Contains(self))
            {
                if (keywords[index].Add(card))
                {
                    Zoom.Insatnce.ViewTool(this, index, position);
                    keywords[index].Tool.Sorting.Sort(null);
                    Texting();
                    return true;
                }
            }
        }
        return false;
    }

    //card에서 호출하는 함수 카드가 어느 키워드에서 빠져야 한다는 함수
    public void OutputKeyword(Card card)
    {
        for (int i = 0; i < keywords.Length; i++)
        {
            if (keywords[i].Space.Contains(card))
            {
                keywords[i].Remove(card);
                Texting();
                return;
            }
        }
    }

    //키워드를 클릭했을때 어느 카드들이 들어가 있는지 보여주는 함수 (이벤트)
    public void OnPointerClick(PointerEventData eventData)
    {
        if (textLinkClickHandler.OnPointerClick(out int index, out Vector3 position))
        {
            //SpaceView(index, position);
            Zoom.Insatnce.ViewTool(this, index, position);
        }
    }
}
