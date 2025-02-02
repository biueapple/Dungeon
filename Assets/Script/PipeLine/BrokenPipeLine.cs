using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

//플레이트가 부셔지는 단계를 당담
public class BrokenPipeLine : IPipeLine
{
    //다음 단계
    private IPipeLine next;
    public IPipeLine Next { get { return next; } set { next = value; } }
    //몇개의 캐릭터가 완료했는지
    private int count = 0;
    //몇개의 캐릭터가 완료 해야하는지
    private int listCount;

    public BrokenPipeLine(IPipeLine next)
    {
        this.next = next;
    }

    public void Start()
    {
        //지금 캐릭터가 몇개인지 넣어놓고
        listCount = GameManager.Instance.Characters.Count;
        //플레이트가 부셔지는 기능 수행
        PlateSetting();
    }

    public void Update()
    {
        //수행을 완료한 수와 캐릭터의 수가 똑같다면 전부 다 수행한거임
        if (count == listCount)
        {
            //다음 단계로
            count = 0;
            GameManager.Instance.PipeLine = next;
        }
    }

    public void End() 
    {
        //콜백을 수행 (모든 캐릭터들이 플레이트가 부셔질테니 모든 캐릭터들이 수행 (나중엔 수정해야 할듯 ))
        List<Character> list = GameManager.Instance.Characters;
        for (int i = list.Count - 1; i >= 0; i--)
        {
            for (int p = list[i].PCB.Count - 1; p >= 0; p--)
            {
                list[i].PCB[p].Broken();
            }
        }
    }

    public void PlateSetting()
    {
        //모든 캐릭터들에게 플레이트를 부수라고 명령
        List<Character> list = GameManager.Instance.Characters;
        for (int i = 0; i < list.Count; i++)
        {
            GameManager.Instance.StartCoroutine(PlateBroken(list[i]));
        }
    }

    private IEnumerator PlateBroken(Character character)
    {
        //캐릭터가 플레이트를 부시는걸 완료하면 
        Plate plate = character.Plate;
        yield return GameManager.Instance.StartCoroutine(character.BrokenC());
        //플레이트에 들어있던 카드들 효과 초기화 후 텍스팅
        if (plate != null)
        {
            plate.KeywordInit();
        }
        //카운터 증가
        count++;
    }
}
