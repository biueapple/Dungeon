using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

//�ؽ�Ʈ������Ʈ�� ���� ������Ʈ�� �������ٰ� Ư�� �ؽ�Ʈ�� ������ ����Ʈ���� ������ �õ������� ������
public class TextAnimator : MonoBehaviour
{
    //public float fallSpeed = 3f;
    //public float startDelay = 0.5f;
    //public GameObject textPrefab;

    //private List<TextMeshProUGUI> letters = new List<TextMeshProUGUI>();

    ////[SerializeField]
    ////private TextMeshProUGUI textComponent; // TextMeshProUGUI ������Ʈ

    ////private void Start()
    ////{
    ////    textComponent.text = "������ <color=#C54646><link=example>" + 0.ToString() + "</link></color>" +
    ////        " ���ظ� <color=#C54646><link=word>" + 1.ToString() + "</link></color> ȸ ������.";

    ////    // �ؽ�Ʈ ������ ������Ʈ�մϴ�.
    ////    textComponent.ForceMeshUpdate();

    ////    // �ؽ�Ʈ ���� ��������
    ////    TMP_TextInfo textInfo = textComponent.textInfo;

    ////    // �� ���ڿ� ���� ó��
    ////    for (int i = 0; i < textComponent.textInfo.linkCount; i++)
    ////    {
    ////        TMP_LinkInfo linkInfo = textInfo.linkInfo[i];

    ////        // ��ũ �ؽ�Ʈ ��������
    ////        string linkText = GetLinkText(linkInfo, textInfo);
    ////        Debug.Log(linkText);
    ////        // ������ ���� ��ǥ ���
    ////        //Rect rect = GetLinkBounds(linkInfo);

    ////        //������Ʈ ����
    ////        GameObject letterObj = Instantiate(textPrefab, transform);
    ////        //TextMeshProUGUI letter = letterObj.GetComponent<TextMeshProUGUI>();

    ////        //letter.text = linkText;
    ////        //letter.transform.position = rect.center; // �� ������ �ʱ� ��ġ ����

    ////        // �ؽ�Ʈ�� char �迭�� ��ȯ
    ////        //char[] textArray = textComponent.text.ToCharArray();

    ////        // �ش� ���� ���� (�������� ��ü)
    ////        //textArray[i] = ' ';

    ////        // ������ �ؽ�Ʈ�� �ٽ� ����
    ////        //textComponent.text = new string(textArray);
    ////        //letters.Add(letter);
    ////        // ���� �������� ����
    ////        //StartCoroutine(FallLetters());
    ////    }
    ////}

    //[SerializeField]
    //private TextMeshProUGUI textComponent; // TextMeshProUGUI ������Ʈ

    //private void Start()
    //{
    //    textComponent.text = "������ <color=#C54646><link=example>0</link></color>" +
    //        " ���ظ� <color=#C54646><link=word>1</link></color> ȸ ������.";

    //    // �ؽ�Ʈ ������ ������Ʈ�մϴ�.
    //    textComponent.ForceMeshUpdate();

    //    // ��ũ �ؽ�Ʈ ����
    //    ReplaceLinkText("example", " ");
    //    ReplaceLinkText("word", "X");

    //    //// �ؽ�Ʈ ���� ��������
    //    //TMP_TextInfo textInfo = textComponent.textInfo;

    //    //// �� ��ũ�� ���� ó��
    //    //for (int i = 0; i < textInfo.linkCount; i++)
    //    //{
    //    //    TMP_LinkInfo linkInfo = textInfo.linkInfo[i];

    //    //    // ��ũ ���� ����� ���
    //    //    Debug.Log($"Link ID: {linkInfo.GetLinkID()}");
    //    //    Debug.Log($"Link Start Index: {linkInfo.linkTextfirstCharacterIndex}, Length: {linkInfo.linkTextLength}");

    //    //    // ��ũ �ؽ�Ʈ ��������
    //    //    string linkText = GetLinkText(linkInfo, textInfo);
    //    //    Debug.Log($"Link Text: {linkText}");

    //    //    //Rect rect = GetLinkBounds(linkInfo);


    //    //    ////������Ʈ ����
    //    //    //GameObject letterObj = Instantiate(textPrefab, transform);
    //    //    //TextMeshProUGUI letter = letterObj.GetComponent<TextMeshProUGUI>();

    //    //    //letter.text = linkText;
    //    //    //letter.transform.position = rect.center; // �� ������ �ʱ� ��ġ ����

    //    //    ////�ؽ�Ʈ�� char �迭�� ��ȯ
    //    //    //char[] textArray = textComponent.text.ToCharArray();

    //    //    ////�ش� ���� ����(�������� ��ü)
    //    //    //textArray[linkInfo.linkTextfirstCharacterIndex] = ' ';

    //    //    ////������ �ؽ�Ʈ�� �ٽ� ����
    //    //    //textComponent.text = new string(textArray);
    //    //    ReplaceLinkText(linkInfo.GetLinkID(), " ");
    //    //    //letters.Add(letter);
    //    //    ////���� �������� ����
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
    //    // ��ũ�� ���� ����
    //    int startIndex = linkInfo.linkTextfirstCharacterIndex;
    //    int length = linkInfo.linkTextLength;

    //    // ���ڵ�κ��� �ؽ�Ʈ ����
    //    char[] linkChars = new char[length];
    //    for (int i = 0; i < length; i++)
    //    {
    //        linkChars[i] = textInfo.characterInfo[startIndex + i].character;
    //    }

    //    return new string(linkChars);
    //}

    ////link�� ��ġ�� �������ִ� �Լ�
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

    //    // ��� ��ũ�� Ž��
    //    for (int i = 0; i < textInfo.linkCount; i++)
    //    {
    //        TMP_LinkInfo linkInfo = textInfo.linkInfo[i];

    //        // ��ũ ID ��
    //        if (linkInfo.GetLinkID() == linkID)
    //        {
    //            // ���� �ؽ�Ʈ ��������
    //            string originalText = textComponent.text;

    //            // ��ũ �ؽ�Ʈ ���� ��������
    //            int startIndex = linkInfo.linkTextfirstCharacterIndex;
    //            int length = linkInfo.linkTextLength;

    //            Debug.Log($"Link ID: {linkID}, Start Index: {startIndex}, Length: {length}");

    //            // ��ũ �ؽ�Ʈ ���� (�±׸� �����ϰ� ���� �ؽ�Ʈ�� ����)
    //            string beforeLink = originalText.Substring(0, startIndex);
    //            string afterLink = originalText.Substring(startIndex + length);
    //            string modifiedText = beforeLink + replacement + afterLink;

    //            // �ؽ�Ʈ ������Ʈ
    //            textComponent.text = modifiedText;
    //            textComponent.ForceMeshUpdate();
    //            Debug.Log($"Updated Text: {textComponent.text}");
    //            return; // �� ���� ó�� �� ����
    //        }
    //    }
    //}
}
