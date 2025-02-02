using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//기능단계를 당담
public class FuntionPipeLine : IPipeLine
{
    //다음단계
    private IPipeLine next;
    public IPipeLine Next { get { return next; } set { next = value; } }
    //이 단계를 수행한 모든 캐릭터들
    private readonly List<Character> characters;
    public List<Character> Characters { get { return characters; } }

    public FuntionPipeLine(IPipeLine next)
    {
        this.next = next;
        characters = new();
    }

    public void Start()
    {
        //기능을 수행한 애들은 리스트에 추가
        List<Character> list = GameManager.Instance.Characters;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Plate != null)
                list[i].Plate.Function();
        }
        GameManager.Instance.StartCoroutine(Wait());
    }

    public void Update()
    {

    }

    public void End()
    {
        //콜백 호출
        for (int i = 0; i < characters.Count; i++)
        {
            for (int p = characters[i].PCB.Count - 1; p >= 0; p--)
            {
                characters[i].PCB[p].Function();
            }
        }
        characters.Clear();
    }

    private IEnumerator Wait()
    {
        //1초의 텀을두고 다음 단계로
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.PipeLine = next;
    }
}
