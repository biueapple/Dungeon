using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limit_Keyword : IKeywordCondition
{
    //keyword의 최소 최대값
    private readonly int min;
    private readonly int max;
    private readonly Keyword keyword;

    public Limit_Keyword(Keyword keyword, int min, int max)
    {
        this.min = min;
        this.max = max;
        this.keyword = keyword;
    }

    //카드가 추가될 조건
    public bool AddCondition(Card card)
    {
        if(keyword.Value + card.Value < min)
            return false;
        return true;
    }
    //카드를 뺄 조건인데 아마 사용하지 않을듯 카드를 못빼는 조건이란게 세상에 어디있어
    public bool RemoveCondition(Card card)
    {
        return true;
    }
}
