using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : Card
{
    private void Start()
    {
        value = 0;
        friendly = true;
        Description = "모험가의 플레이트를 하나 부수고 카드 한장을 뽑고 휘발성인 모험가 +1 카드를 하나 생성한다.";
    }

    public override void UpPlate(Plate plate)
    {
        //아군의 플레이트라면
        if(GameManager.Instance.Adventurer.Contains(plate.Self))
        {
            //드로우와 생성
            new IndividualDraw(1);
            new IndividualCard("Adventurer/VolatilityPlus");
            //부시는 플레이트에 있던 카드를 손패에 추가해줘야 하는데
            for(int i  = 0; i < plate.Keyword.Length; i++)
            {
                for (int j = plate.Keyword[i].Space.Count - 1; j >= 0; j--)
                {
                    //카드가 지금 손패에 있는 상태임을 넣고
                    plate.Keyword[i].Space[j].State = new HandState_Card(plate.Keyword[i].Space[j]);
                    //활성화
                    plate.Keyword[i].Space[j].gameObject.SetActive(true);
                    //손패에 추가
                    GameManager.Instance.Hand.AddCard(plate.Keyword[i].Space[j]);
                    plate.Keyword[i].Space.RemoveAt(j);
                }
            }
            //자신은 사용되었으니 사용한 덱으로 돌아가기
            plate.Dummy.Before.Remove(this);
            GameManager.Instance.Hand.RemoveCard(this);
            plate.Dummy.AfterCard(this);
            //플레이트 부수기
            new IndividualBroken(plate.Self);
        }
        Zoom.Insatnce.Active = true;
    }
}
