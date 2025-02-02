using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//어떠한 ui가 캔버스의 범위를 넘어갔는지 판단하고 다시 안으로 되돌리는 역할
public class CanvasRect : MonoBehaviour
{
    //캔버스의 크기
    Rect rect;
    // Start is called before the first frame update
    void Start()
    {
        rect.width = Screen.width;
        rect.height = Screen.height;
    }

    //입력한 recttransfrom이 캔버스의 범위를 넘어섰다면 다시 안으로 되돌림
    public void InSide(RectTransform rectTransform)
    {
        //rectTransform를 rect안으로 옮기기
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
