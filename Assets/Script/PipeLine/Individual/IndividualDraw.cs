using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualDraw
{
    public IndividualDraw(int count)
    {
        GameManager.Instance.StartCoroutine(Draw(count));
    }

    private IEnumerator Draw(int count)
    {
        //횟수만큼
        for (int i = 0; i < count; i++)
        {
            //드로우를 할때 한장한장 완료된 후에 작동하도록
            yield return GameManager.Instance.Dummy.Draw();
        }
    }
}
