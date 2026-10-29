using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualDraw
{
    //from : 이 캐릭터의 덱에서 뽑음 (null이면 돌아가면서)
    public IndividualDraw(int count, Character from = null)
    {
        GameManager.Instance.StartCoroutine(Draw(count, from));
    }

    private IEnumerator Draw(int count, Character from)
    {
        //횟수만큼
        for (int i = 0; i < count; i++)
        {
            //드로우를 할때 한장한장 완료된 후에 작동하도록
            yield return GameManager.Instance.Dummy.Draw(from);
        }
    }
}
