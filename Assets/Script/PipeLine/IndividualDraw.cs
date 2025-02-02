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
        //Ƚ����ŭ
        for (int i = 0; i < count; i++)
        {
            //��ο츦 �Ҷ� �������� �Ϸ�� �Ŀ� �۵��ϵ���
            yield return GameManager.Instance.Dummy.Draw();
        }
    }
}
