using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refresh : Card
{
    private void Start()
    {
        value = 0;
        friendly = true;
        Description = "모험가의 플레이트를 부수고 카드를 한장 뽑는다." +
            "모험가의 플레이트를 생성한다.";
    }

    public override void UpCharacter(Character character)
    {
        GameManager.Instance.Dummy.Before.Remove(this);
        GameManager.Instance.Hand.RemoveCard(this);
        GameManager.Instance.Dummy.AfterCard(this);
        Zoom.Insatnce.Active = true;
        GameManager.Instance.StartCoroutine(Process(character));
    }

    private IEnumerator Process(Character character)
    {
        //플레이트를 부수는 과정
        if (character.Plate != null)
        {
            IndividualBroken b = new();
            yield return b.PlateBroken(character);
            new IndividualDraw(1);
        }
        //플레이트를 생성하는 과정
        new IndividualCreate(character);
    }
}
