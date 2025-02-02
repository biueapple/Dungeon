using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limit_Keyword : IKeywordCondition
{
    //keyword�� �ּ� �ִ밪
    private readonly int min;
    private readonly int max;
    private readonly Keyword keyword;

    public Limit_Keyword(Keyword keyword, int min, int max)
    {
        this.min = min;
        this.max = max;
        this.keyword = keyword;
    }

    //ī�尡 �߰��� ����
    public bool AddCondition(Card card)
    {
        if(keyword.Value + card.Value < min)
            return false;
        return true;
    }
    //ī�带 �� �����ε� �Ƹ� ������� ������ ī�带 ������ �����̶��� ���� ����־�
    public bool RemoveCondition(Card card)
    {
        return true;
    }
}
