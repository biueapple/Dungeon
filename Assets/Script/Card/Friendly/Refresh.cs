using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refresh : Card
{
    private void Start()
    {
        value = 0;
        friendly = true;
        Description = "���谡�� �÷���Ʈ�� �μ��� ī�带 ���� �̴´�." +
            "���谡�� �÷���Ʈ�� �����Ѵ�.";
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
        //�÷���Ʈ�� �μ��� ����
        if (character.Plate != null)
        {
            IndividualBroken b = new();
            yield return b.PlateBroken(character);
            new IndividualDraw(1);
        }
        //�÷���Ʈ�� �����ϴ� ����
        new IndividualCreate(character);
    }
}
