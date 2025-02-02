using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualCreate
{
    public IndividualCreate(Character character)
    {
        GameManager.Instance.StartCoroutine(PlateCreate(character));
    }

    private IEnumerator PlateCreate(Character character)
    {
        //�÷���Ʈ �Ϸ�
        yield return GameManager.Instance.StartCoroutine(character.CreateC());
        for (int i = character.PCB.Count - 1; i >= 0; i--)
        {
            character.PCB[i].Create();
        }
    }
}
