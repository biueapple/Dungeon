using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��� ui�� ĵ������ ������ �Ѿ���� �Ǵ��ϰ� �ٽ� ������ �ǵ����� ����
public class CanvasRect : MonoBehaviour
{
    //ĵ������ ũ��
    Rect rect;
    // Start is called before the first frame update
    void Start()
    {
        rect.width = Screen.width;
        rect.height = Screen.height;
    }

    //�Է��� recttransfrom�� ĵ������ ������ �Ѿ�ٸ� �ٽ� ������ �ǵ���
    public void InSide(RectTransform rectTransform)
    {
        //rectTransform�� rect������ �ű��
        if (rectTransform.position.x + rectTransform.rect.xMin * rectTransform.localScale.x < rect.xMin)
        {
            rectTransform.position += new Vector3(-(rectTransform.position.x + rectTransform.rect.xMin * rectTransform.localScale.x), 0, 0);
        }
        else if (rectTransform.position.x + rectTransform.rect.xMax * rectTransform.localScale.x > rect.xMax)
        {
            rectTransform.position += new Vector3(-(rectTransform.position.x + rectTransform.rect.xMax * rectTransform.localScale.x - rect.width), 0, 0);
        }
        else if(rectTransform.position.y + rectTransform.rect.yMin * rectTransform.localScale.y < rect.yMin )
        {
            rectTransform.position += new Vector3(0, -(rectTransform.position.y + rectTransform.rect.yMin * rectTransform.localScale.y), 0);
        }
        else if(rectTransform.position.y + rectTransform.rect.yMax * rectTransform.localScale.y > rect.yMax )
        {
            rectTransform.position += new Vector3(0, -(rectTransform.position.y + rectTransform.rect.yMax * rectTransform.localScale.y - rect.height), 0);
        }
    }
}
