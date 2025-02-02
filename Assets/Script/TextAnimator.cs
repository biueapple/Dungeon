using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

//텍스트컴포넌트를 가진 오브젝트가 떨어지다가 특정 텍스트를 밑으로 떨어트리는 연출을 시도했지만 실패함
public class TextAnimator : MonoBehaviour
{
    //public float fallSpeed = 3f;
    //public float startDelay = 0.5f;
    //public GameObject textPrefab;

    //private List<TextMeshProUGUI> letters = new List<TextMeshProUGUI>();

    ////[SerializeField]
    ////private TextMeshProUGUI textComponent; // TextMeshProUGUI 컴포넌트

    ////private void Start()
    ////{
    ////    textComponent.text = "적에게 <color=#C54646><link=example>" + 0.ToString() + "</link></color>" +
    ////        " 피해를 <color=#C54646><link=word>" + 1.ToString() + "</link></color> 회 입힌다.";

    ////    // 텍스트 정보를 업데이트합니다.
    ////    textComponent.ForceMeshUpdate();

    ////    // 텍스트 정보 가져오기
    ////    TMP_TextInfo textInfo = textComponent.textInfo;

    ////    // 각 문자에 대해 처리
    ////    for (int i = 0; i < textComponent.textInfo.linkCount; i++)
    ////    {
    ////        TMP_LinkInfo linkInfo = textInfo.linkInfo[i];

    ////        // 링크 텍스트 가져오기
    ////        string linkText = GetLinkText(linkInfo, textInfo);
    ////        Debug.Log(linkText);
    ////        // 문자의 월드 좌표 계산
    ////        //Rect rect = GetLinkBounds(linkInfo);

    ////        //오브젝트 생성
    ////        GameObject letterObj = Instantiate(textPrefab, transform);
    ////        //TextMeshProUGUI letter = letterObj.GetComponent<TextMeshProUGUI>();

    ////        //letter.text = linkText;
    ////        //letter.transform.position = rect.center; // 각 글자의 초기 위치 조정

    ////        // 텍스트를 char 배열로 변환
    ////        //char[] textArray = textComponent.text.ToCharArray();

    ////        // 해당 문자 비우기 (공백으로 대체)
    ////        //textArray[i] = ' ';

    ////        // 수정된 텍스트를 다시 설정
    ////        //textComponent.text = new string(textArray);
    ////        //letters.Add(letter);
    ////        // 글자 떨어지기 시작
    ////        //StartCoroutine(FallLetters());
    ////    }
    ////}

    //[SerializeField]
    //private TextMeshProUGUI textComponent; // TextMeshProUGUI 컴포넌트

    //private void Start()
    //{
    //    textComponent.text = "적에게 <color=#C54646><link=example>0</link></color>" +
    //        " 피해를 <color=#C54646><link=word>1</link></color> 회 입힌다.";

    //    // 텍스트 정보를 업데이트합니다.
    //    textComponent.ForceMeshUpdate();

    //    // 링크 텍스트 변경
    //    ReplaceLinkText("example", " ");
    //    ReplaceLinkText("word", "X");

    //    //// 텍스트 정보 가져오기
    //    //TMP_TextInfo textInfo = textComponent.textInfo;

    //    //// 각 링크에 대해 처리
    //    //for (int i = 0; i < textInfo.linkCount; i++)
    //    //{
    //    //    TMP_LinkInfo linkInfo = textInfo.linkInfo[i];

    //    //    // 링크 정보 디버깅 출력
    //    //    Debug.Log($"Link ID: {linkInfo.GetLinkID()}");
    //    //    Debug.Log($"Link Start Index: {linkInfo.linkTextfirstCharacterIndex}, Length: {linkInfo.linkTextLength}");

    //    //    // 링크 텍스트 가져오기
    //    //    string linkText = GetLinkText(linkInfo, textInfo);
    //    //    Debug.Log($"Link Text: {linkText}");

    //    //    //Rect rect = GetLinkBounds(linkInfo);


    //    //    ////오브젝트 생성
    //    //    //GameObject letterObj = Instantiate(textPrefab, transform);
    //    //    //TextMeshProUGUI letter = letterObj.GetComponent<TextMeshProUGUI>();

    //    //    //letter.text = linkText;
    //    //    //letter.transform.position = rect.center; // 각 글자의 초기 위치 조정

    //    //    ////텍스트를 char 배열로 변환
    //    //    //char[] textArray = textComponent.text.ToCharArray();

    //    //    ////해당 문자 비우기(공백으로 대체)
    //    //    //textArray[linkInfo.linkTextfirstCharacterIndex] = ' ';

    //    //    ////수정된 텍스트를 다시 설정
    //    //    //textComponent.text = new string(textArray);
    //    //    ReplaceLinkText(linkInfo.GetLinkID(), " ");
    //    //    //letters.Add(letter);
    //    //    ////글자 떨어지기 시작
    //    //    //StartCoroutine(FallLetters());
    //    //}
    //}

    //IEnumerator FallLetters()
    //{
    //    yield return new WaitForSeconds(startDelay);

    //    foreach (var letter in letters)
    //    {
    //        StartCoroutine(Fall(letter));
    //    }
    //}

    //IEnumerator Fall(TextMeshProUGUI letter)
    //{
    //    while (true)
    //    {
    //        letter.transform.localPosition += Vector3.down * fallSpeed * Time.deltaTime;
    //        yield return null;
    //        fallSpeed += 0.98f;
    //    }
    //}

    //private string GetLinkText(TMP_LinkInfo linkInfo, TMP_TextInfo textInfo)
    //{
    //    // 링크의 문자 범위
    //    int startIndex = linkInfo.linkTextfirstCharacterIndex;
    //    int length = linkInfo.linkTextLength;

    //    // 문자들로부터 텍스트 조합
    //    char[] linkChars = new char[length];
    //    for (int i = 0; i < length; i++)
    //    {
    //        linkChars[i] = textInfo.characterInfo[startIndex + i].character;
    //    }

    //    return new string(linkChars);
    //}

    ////link의 위치를 리턴해주는 함수
    //private Rect GetLinkBounds(TMP_LinkInfo linkInfo)
    //{
    //    TMP_CharacterInfo firstChar = textComponent.textInfo.characterInfo[linkInfo.linkTextfirstCharacterIndex];
    //    TMP_CharacterInfo lastChar = textComponent.textInfo.characterInfo[linkInfo.linkTextfirstCharacterIndex + linkInfo.linkTextLength - 1];

    //    Vector3 bottomLeft = textComponent.transform.TransformPoint(firstChar.bottomLeft);
    //    Vector3 topRight = textComponent.transform.TransformPoint(lastChar.topRight);

    //    Vector2 size = topRight - bottomLeft;
    //    return new Rect(bottomLeft, size);
    //}

    //private void ReplaceLinkText(string linkID, string replacement)
    //{
    //    TMP_TextInfo textInfo = textComponent.textInfo;

    //    // 모든 링크를 탐색
    //    for (int i = 0; i < textInfo.linkCount; i++)
    //    {
    //        TMP_LinkInfo linkInfo = textInfo.linkInfo[i];

    //        // 링크 ID 비교
    //        if (linkInfo.GetLinkID() == linkID)
    //        {
    //            // 원래 텍스트 가져오기
    //            string originalText = textComponent.text;

    //            // 링크 텍스트 범위 가져오기
    //            int startIndex = linkInfo.linkTextfirstCharacterIndex;
    //            int length = linkInfo.linkTextLength;

    //            Debug.Log($"Link ID: {linkID}, Start Index: {startIndex}, Length: {length}");

    //            // 링크 텍스트 수정 (태그를 유지하고 내부 텍스트만 변경)
    //            string beforeLink = originalText.Substring(0, startIndex);
    //            string afterLink = originalText.Substring(startIndex + length);
    //            string modifiedText = beforeLink + replacement + afterLink;

    //            // 텍스트 업데이트
    //            textComponent.text = modifiedText;
    //            textComponent.ForceMeshUpdate();
    //            Debug.Log($"Updated Text: {textComponent.text}");
    //            return; // 한 번만 처리 후 종료
    //        }
    //    }
    //}
}
