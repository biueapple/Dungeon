using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ű������ min max�� ���ϰ� min���� ������ max�� max���� ũ�� min���� ���� �����ϴ� ������� ���� �����ϴ� Ŭ����
public class Circulation_Keyword : IKeywordAttribute
{
    //keyword�� �ּ� �ִ밪
    private readonly int min;
    private readonly int max;

    public Circulation_Keyword(int min, int max)
    {
        this.min = min;
        this.max = max;
    }

    //�� Ŭ������ ���
    public void Attribute(ref int value)
    {
        //���� value�� max ���� ũ�� min���� min���� ������ max��
        //Ȥ�� ���ѹݺ��ϸ� �ȵǴϱ�
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
