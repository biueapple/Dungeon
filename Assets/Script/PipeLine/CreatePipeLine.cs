using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이트를 생성하는 단계
public class CreatePipeLine : IPipeLine
{
    //다음 단계
    private IPipeLine next;
    public IPipeLine Next { get { return next; } set { next = value; } }
    //몇개를 했는지
    private int count = 0;
    //몇개를 해야하는지
    private int listCount;

    public CreatePipeLine(IPipeLine next)
    {
        this.next = next;
    }

    public void Start()
    {
        //몇개를 해야하는지 입력
        listCount = GameManager.Instance.Characters.Count;
        //생성
        PlateSetting();
    }

    public void Update()
    {
        //완료한 갯수와 해야하는 갯수가 같다면
        if(count == listCount)
        {
            //다음단계로
            count = 0;
            GameManager.Instance.PipeLine = next;
        }
    }

    public void End()
    {
        //콜백 호출 (모든 캐릭터가 목표)
        List<Character> list = GameManager.Instance.Characters;
        for (int i = list.Count - 1; i >= 0; i--)
        {
            for (int p = list[i].PCB.Count - 1; p >= 0; p--)
            {
                list[i].PCB[p].Create();
            }
        }
    }

    //모든 캐릭터들이 판을 생성하기로
    private void PlateSetting()
    {
        List<Character> list = GameManager.Instance.Characters;
        for (int i = 0; i < list.Count; i++)
        {
            GameManager.Instance.StartCoroutine(PlateCreate(list[i]));
        }
    }

    private IEnumerator PlateCreate(Character character)
    {
        //캐릭터가 판 생성을 완료하면
        yield return GameManager.Instance.StartCoroutine(character.CreateC());
        //카운터 증가
        count++;
    }

}
