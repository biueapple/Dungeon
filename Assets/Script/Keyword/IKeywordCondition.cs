using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKeywordCondition
{
    public bool AddCondition(Card card);
    public bool RemoveCondition(Card card);
}
