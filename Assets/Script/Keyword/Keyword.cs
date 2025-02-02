using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyword
{
    public Keyword(int word, Tool tool)
    {
        value = word;
        this.tool = tool;
        space = new();
    }
    //���� ��
    private int value;
    public int Value { get { return value;} }
    public int Word
    {
        get
        {
            int v = value;
            attributes.ForEach(x => x.Attribute(ref v));
            return v;
        }
    }
    //�� Ű���忡 ������ ��ġ�� �ִ� ī���
    private readonly List<Card> space;
    public List<Card> Space { get { return space; } }
    //�� ī����� �������ִ� ��
    private readonly Tool tool;
    public Tool Tool { get { return tool; } /*set {  tool = value; } */}
    //����� ���Ҷ� �ݹ�
    private readonly List<IKeywordAttribute> attributes = new ();
    public List<IKeywordAttribute> Attributes { get {  return attributes; } }
    //ī�尡 �߰��� �� �ִ� ����
    private readonly List<IKeywordCondition> conditions = new ();
    public List<IKeywordCondition> Conditions { get { return conditions; } }

    //Ű���忡 ī�带 �߰��ϰ� ����
    public bool Add(Card card)
    {
        for(int i = 0; i < conditions.Count; i++)
        {
            if (!conditions[i].AddCondition(card))
                return false;
        }
        value += card.Value;
        space.Add(card);
        tool.Sorting.Add(card.transform);
        return true;
    }
    //Ű���忡 ī�带 ���� ����
    public void Remove(Card card)
    {
        value -= card.Value;
        space.Remove(card);
        tool.Sorting.Remove(card.transform);
    }
    //��� ī�带 ���� ����
    public void Clear()
    {
        for(int i = 0; i < space.Count; i++)
        {
            value -= space[i].Value;
            tool.Sorting.Remove(space[i].transform);
        }
        space.Clear();
    }
}
