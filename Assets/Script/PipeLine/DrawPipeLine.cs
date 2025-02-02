using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//카드를 뽑는 단계
public class DrawPipeLine : IPipeLine
{
    //다음 진행될 파이프라인
    private  IPipeLine next;
    public IPipeLine Next { get { return next; } set { next = value; } }
    //덱
    private readonly Dummy dummy;
    //몇장을 뽑을건지
    private int count;
    public int Count { get { return count; } set { count = value; } }

    public DrawPipeLine(IPipeLine next, Dummy dummy, int count)
    {
        this.next = next;
        this.dummy = dummy;
        this.count = count;
    }

    public void Start()
    {
        //드로우 시작
        GameManager.Instance.StartCoroutine(Draw(count));
    }

    public void Update() { }

    private IEnumerator Draw(int count)
    {
        //횟수만큼
        for(int i = 0; i < count; i++)
        {
            //드로우를 할때 한장한장 완료된 후에 작동하도록
            yield return dummy.Draw();
        }
        //다음단계로
        GameManager.Instance.PipeLine = next;
    }

    public void End()
    {
        //draw단계 콜백 호출 (드로우는 캐릭터와는 상관없음 )
        for (int i = GameManager.Instance.PCB.Count - 1; i >= 0; i--)
        {
            GameManager.Instance.PCB[i].Draw();
        }
        //GameManager.Instance.Characters.ForEach(c => c.PCB.ForEach(p => p.Draw()));
    }
}
