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

    //plate�� ���빰�� ���� ���з� �ǵ������ ��
    public IEnumerator PlateBroken(Character character)
    {
        Plate plate = character.Plate;
        for(int i = 0; i < plate.Keyword.Length; i++)
        {
            for (int j = 0; j < plate.Keyword[i].Space.Count; j++)
                plate.Keyword[i].Space[j].UpEmpty();
        }
        
        //ĳ���Ͱ� �÷���Ʈ�� �νô°� �Ϸ��ϸ� 
        yield return GameManager.Instance.StartCoroutine(character.BrokenC());
        //�÷���Ʈ�� ����ִ� ī��� ȿ�� �ʱ�ȭ �� �ؽ���
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
