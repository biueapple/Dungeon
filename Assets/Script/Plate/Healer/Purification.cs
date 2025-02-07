using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purification : Plate
{
    protected override void Init()
    {
        //키워드가 몇개고 기본값이 얼마인지
        keywords = new Keyword[0];
    }

    //텍스트
    protected override void Texting()
    {
        textLinkClickHandler.Texting
            ("모든 모험가들의 상태이상을 제거한다.");
    }

    //방어기능
    public override void Defense()
    {
        List<DefenceInfo> defenceInfos = new();

        for (int i = 0; i < GameManager.Instance.Adventurer.Count; i++)
        {
            for (int p = GameManager.Instance.Adventurer[i].PCB.Count - 1; p >= 0; p--)
            {
                if (GameManager.Instance.Adventurer[i].PCB[p] is IDebuff)
                {
                    GameManager.Instance.Adventurer[i].PCB.RemoveAt(p);
                    defenceInfos.Add(new DefenceInfo(GameManager.Instance.Adventurer[i], 0, 0));
                }
            } 
        }

        //방어를 수행한 캐릭터에 속함
        if (defenceInfos.Count > 0)
            GameManager.Instance.CyclePipeLine.Defence.Fulfillment.Add(new PL_ShieldNHeal(self, defenceInfos, SoundManager.Instance.Recovery));
    }

    //공격기능
    public override void Attack() { }
}
