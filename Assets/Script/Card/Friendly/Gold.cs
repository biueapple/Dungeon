using UnityEngine;

//아무 효과가 없는 카드 (전투 보상으로 얻을 수 있음)
public class Gold : Card
{
    //카드의 가치
    public override int Worth { get { return 1; } }
    //아무 효과가 없음
    public override bool NoEffect { get { return true; } }

    private void Awake()
    {
        //다른 카드와 구분되도록 금색 백그라운드
        BackColor = new Color(1f, 0.8f, 0.25f);
    }

    private void Start()
    {
        value = 0;
        friendly = true;
        Description = "골드\n아무 효과가 없다.\n사용할 수 없고 턴이 끝나면 버려진다.";
    }

    //사용할 수 없는 카드라 어디에 놓든 손패로 돌아감 (Card의 기본 동작)
}
