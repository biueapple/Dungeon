using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Plate : MonoBehaviour , IPointerClickHandler
{
    //�ؽ�Ʈ ������Ʈ
    protected TextLinkClickHandler textLinkClickHandler;
    public TextLinkClickHandler TextLinkClickHandler { get { return textLinkClickHandler; } set {  textLinkClickHandler = value; } }
    
    //�Էµ� ī�带 �������ִ� ����
    [SerializeField]
    protected Tool[] tool;
    public Tool[] Tool { get { return tool; } }

    //����� Ű�����
    protected Keyword[] keywords;
    public Keyword[] Keyword { get { return keywords; } }

    ////����
    //protected List<Character> foe;
    //public List<Character> Foe { get {  return foe; } set {  foe = value; } }
    ////�Ʊ�
    //protected List<Character> ally;
    //public List<Character> Ally { get { return ally; } set { ally = value; } }
    //�ڽ�
    protected Character self;
    public Character Self { get { return self; } set {  self = value; } }

    //��
    protected Dummy dummy;
    public Dummy Dummy { get { return dummy; } }

    private void Awake()
    {
        textLinkClickHandler = GetComponent<TextLinkClickHandler>();
    }

    private void Start()
    {
        dummy = GameManager.Instance.Dummy;
        //Ȥ�� �ʱ�ȭ �ؾ��� �� ������ override�ؼ� �����
        Init();
        //Ű���� ����
        Texting();
    }

    //
    private void LateUpdate()
    {
        //��Ŭ������ ��� Ű���忡 �ִ� ī�带 �������� �ʵ���
        if(Input.GetMouseButtonDown(1))
        {
            //SpaceView(-1, Vector2.zero);
            Zoom.Insatnce.ViewTool(this, -1, Vector2.zero);
        }
    }

    //Ȥ�� �ʱ�ȭ �ؾ��� �� ������ override�ؼ� �����
    protected abstract void Init();

    //�� �÷���Ʈ�� ���� �ؽ�Ʈ�� Ű���尡 �ִ��� ����
    protected abstract void Texting();
    //�÷���Ʈ�� �ɷµ�
    public abstract void Defense();
    public abstract void Attack();
    public virtual void Function()
    {
        GameManager.Instance.CyclePipeLine.Function.Characters.Add(self);
    }

    //��� ����� ī����� �ʱ�ȭ��Ű�� �۾�
    public void KeywordInit()
    {
        //��� Ű���忡 ����ִ� ī����� ���� ���� �������� �ű��
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
        //ī����� ������ �ʵ���
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
        //ī�尡 ���ĵǾ� �ִ� ���� ��Ȱ��ȭ �ؼ� ������ �ʵ��� (ī�尡 parent�� tool�� �ƴ�)
        for (int i = 0; i < tool.Length; i++)
        {
            tool[i].gameObject.SetActive(false);
        }
    }

    //card���� ȣ���ϴ� �Լ� ī�尡 ��� Ű���忡 ���� �Ѵٴ� �Լ�
    public bool InputKeyword(Card card)
    {
        //ī�尡 plate ������ up�����鼭 keyword ������ up�� ���
        if(textLinkClickHandler.OnPointerClick(out int index, out Vector3 position))
        {
            //���谡�ΰ�쿡�� ����
            if(card.Friendly && GameManager.Instance.Adventurer.Contains(self))
            {
                //Ű���忡 �߰�
                if (keywords[index].Add(card))
                {
                    //Ű���忡 �� �ִ� ī��� �����ֱ�
                    Zoom.Insatnce.ViewTool(this, index, position);
                    //����
                    keywords[index].Tool.Sorting.Sort(null);
                    //Ű���尡 ��ȭ �����״ϱ� �ٽ� �ؽ�Ʈ �ε�
                    Texting();
                    return true;
                }
            }
            //���� ��쿡�� ����
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

    //card���� ȣ���ϴ� �Լ� ī�尡 ��� Ű���忡�� ������ �Ѵٴ� �Լ�
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

    //Ű���带 Ŭ�������� ��� ī����� �� �ִ��� �����ִ� �Լ� (�̺�Ʈ)
    public void OnPointerClick(PointerEventData eventData)
    {
        if (textLinkClickHandler.OnPointerClick(out int index, out Vector3 position))
        {
            //SpaceView(index, position);
            Zoom.Insatnce.ViewTool(this, index, position);
        }
    }
}
