using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class TextLinkClickHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent; // 텍스트 컴포넌트

    public void Texting(string text)
    {
        textComponent.text = text;
    }

    //원래는 직접 이벤트로 호출되던 함수 지금은 호출 즉시 마우스 위치를 토대로 link text를 찾아 out해줌
    public bool OnPointerClick(out string word, out Vector3 position)
    {
        //마우스 위치에 따른 링크의 인덱스를 리턴
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(textComponent, Input.mousePosition, null);

        //링크가 있었다면
        if (linkIndex != -1)
        {
            //링크의 텍스트를 가져와
            TMP_LinkInfo linkInfo = textComponent.textInfo.linkInfo[linkIndex];
            string linkText = linkInfo.GetLinkText();
            //word에 입력해서 out해줌
            word = linkText;
            //위치도 가져와서 out해줌
            Rect rect = GetLinkBounds(linkInfo);
            position = rect.center;
            //성공적
            return true;
        }
        //마우스 위치에 어떤 링크도 없었다면
        else
        {
            word = null;
            position = Vector3.zero;
            //실패
            return false;
        }
    }

    //원래는 직접 이벤트로 호출되던 함수 지금은 호출 즉시 마우스 위치를 토대로 link index 찾아 out해줌
    public bool OnPointerClick(out int index, out Vector3 position)
    {
        //마우스 위치에 따른 링크의 인덱스를 리턴
        index = TMP_TextUtilities.FindIntersectingLink(textComponent, Input.mousePosition, null);

        //링크가 있었다면
        if (index != -1)
        { 
            //위치도 가져와서 out해줌
            TMP_LinkInfo linkInfo = textComponent.textInfo.linkInfo[index];
            Rect rect = GetLinkBounds(linkInfo);
            position = rect.center;
            //성공적
            return true;
        }
        else
        {
            position = Vector3.zero;
            //실패
            return false;
        }
    }

    //link의 위치를 리턴해주는 함수
    private Rect GetLinkBounds(TMP_LinkInfo linkInfo)
    {
        TMP_CharacterInfo firstChar = textComponent.textInfo.characterInfo[linkInfo.linkTextfirstCharacterIndex];
        TMP_CharacterInfo lastChar = textComponent.textInfo.characterInfo[linkInfo.linkTextfirstCharacterIndex + linkInfo.linkTextLength - 1];

        Vector3 bottomLeft = textComponent.transform.TransformPoint(firstChar.bottomLeft);
        Vector3 topRight = textComponent.transform.TransformPoint(lastChar.topRight);

        Vector2 size = topRight - bottomLeft;
        return new Rect(bottomLeft, size);
    }
}