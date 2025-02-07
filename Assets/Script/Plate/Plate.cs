using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        //우클릭으로 모든 키워드에 있는 카드를 보여주지 않도록
        if(Input.GetMouseButtonDown(1))
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
