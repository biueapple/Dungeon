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
    //기본으로 뽑는 장수(모험가 덱마다 1장)보다 더 뽑거나 덜 뽑을 장수
    private int bonus;
    //몇장을 뽑을건지 (살아있는 모험가 수 + bonus)
    public int Count
    {
        get { return GameManager.Instance.Adventurer.Count + bonus; }
        set { bonus = value - GameManager.Instance.Adventurer.Count; }
    }

    public DrawPipeLine(IPipeLine next, Dummy dummy, int bonus)
    {
        this.next = next;
        this.dummy = dummy;
        this.bonus = bonus;
    }

    public void Start()
    {
        //드로우 시작
        GameManager.Instance.StartCoroutine(Draw(Count));
    }

    public void Update() { }

    private IEnumerator Draw(int count)
    {
        //횟수만큼 앞의 모험가부터 돌아가면서 그 모험가의 덱에서 1장씩
        List<Character> adventurer = GameManager.Instance.Adventurer;
        for(int i = 0; i < count && adventurer.Count > 0; i++)
        {
            //드로우를 할때 한장한장 완료된 후에 작동하도록
            yield return dummy.Draw(adventurer[i % adventurer.Count]);
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
