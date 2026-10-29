using System.Collections;
using UnityEngine;

//아군 캐릭터에게 사용하면 플레이트를 새로 만드는 카드 (이미 있다면 부수고 새로 만듬)
public class Rebuild : Card
{
    //카드의 가치
    public override int Worth { get { return 1; } }

    private void Start()
    {
        value = 0;
        friendly = true;
        Description = "재건\n모험가의 플레이트를 부수고 새 플레이트를 생성한다.\n아군에게만 사용할 수 있다.";
    }

    public override void UpCharacter(Character character)
    {
        //아군(모험가)에게만 사용 가능
        if (character == null || !GameManager.Instance.Adventurer.Contains(character))
        {
            UpEmpty();
            return;
        }
        GameManager.Instance.Hand.RemoveCard(this);
        GameManager.Instance.Dummy.AfterCard(this);
        Zoom.Insatnce.Active = true;
        GameManager.Instance.StartCoroutine(Process(character));
    }

    private IEnumerator Process(Character character)
    {
        //이미 플레이트가 있다면 부수기 (안에 있던 카드는 손패로 돌아감)
        if (character.Plate != null)
        {
            IndividualBroken b = new();
            yield return b.PlateBroken(character);
        }
        //새 플레이트 생성
        new IndividualCreate(character);
    }
}
