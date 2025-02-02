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
    //헌재 값
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
    //이 키워드에 영향을 끼치고 있는 카드들
    private readonly List<Card> space;
    public List<Card> Space { get { return space; } }
    //그 카드들을 정렬해주는 툴
    private readonly Tool tool;
    public Tool Tool { get { return tool; } /*set {  tool = value; } */}
    //밸류가 변할때 콜백
    private readonly List<IKeywordAttribute> attributes = new ();
    public List<IKeywordAttribute> Attributes { get {  return attributes; } }
    //카드가 추가될 수 있는 조건
    private readonly List<IKeywordCondition> conditions = new ();
    public List<IKeywordCondition> Conditions { get { return conditions; } }

    //키워드에 카드를 추가하고 정렬
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
    //키워드에 카드를 빼고 정렬
    public void Remove(Card card)
    {
        value -= card.Value;
        space.Remove(card);
        tool.Sorting.Remove(card.transform);
    }
    //모든 카드를 빼고 정렬
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
