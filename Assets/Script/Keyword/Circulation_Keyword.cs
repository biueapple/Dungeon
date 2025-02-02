using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//키워드의 min max를 정하고 min보다 작으면 max로 max보다 크면 min으로 값을 변경하는 방법으로 값을 제한하는 클래스
public class Circulation_Keyword : IKeywordAttribute
{
    //keyword의 최소 최대값
    private readonly int min;
    private readonly int max;

    public Circulation_Keyword(int min, int max)
    {
        this.min = min;
        this.max = max;
    }

    //이 클래스의 기능
    public void Attribute(ref int value)
    {
        //만약 value가 max 보다 크면 min으로 min보다 작으면 max로
        //혹시 무한반복하면 안되니까
        int count = 0;
        while (!(value >= min) || !(value <= max))
        {
            count++;
            if (count > 10)
                break;
            if (value < min)
            {
                value = max + (value - min);
            }
            else if (value > max)
            {
                value = min + (value - max);
            }
        }
    }
}
