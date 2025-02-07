using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualCreate
{
    public IndividualCreate() { }
    public IndividualCreate(Character character)
    {
        GameManager.Instance.StartCoroutine(PlateCreate(character));
    }

    public IEnumerator PlateCreate(Character character)
    {
        //플레이트 완료
        yield return GameManager.Instance.StartCoroutine(character.CreateC());
        for (int i = character.PCB.Count - 1; i >= 0; i--)
        {
            character.PCB[i].Create();
        }
    }
}
