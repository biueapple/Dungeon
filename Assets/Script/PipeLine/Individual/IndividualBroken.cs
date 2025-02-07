using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualBroken
{
    public IndividualBroken() { }
    public IndividualBroken(Character character) 
    {
        GameManager.Instance.StartCoroutine(PlateBroken(character));
    }

    //plate의 내용물을 전부 손패로 되돌려줘야 함
    public IEnumerator PlateBroken(Character character)
    {
        Plate plate = character.Plate;
        for(int i = 0; i < plate.Keyword.Length; i++)
        {
            for (int j = 0; j < plate.Keyword[i].Space.Count; j++)
                plate.Keyword[i].Space[j].UpEmpty();
        }
        
        //캐릭터가 플레이트를 부시는걸 완료하면 
        yield return GameManager.Instance.StartCoroutine(character.BrokenC());
        //플레이트에 들어있던 카드들 효과 초기화 후 텍스팅
        if (plate != null)
        {
            plate.KeywordInit();
        }
        for (int i = character.PCB.Count - 1; i >= 0; i--)
        {
            character.PCB[i].Broken();
        }
    }
}
