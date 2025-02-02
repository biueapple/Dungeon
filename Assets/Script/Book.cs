using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//지금은 페이지에 수동으로 카드팩을 놓고 페이즈를 on off 하지만 나중에는 카드팩을 순서대로 정렬해서 보여주도록 해야할지도 안해도 될지도
public class Book : MonoBehaviour
{
    //지금 몇페이지 인지
    [SerializeField]
    private int page;

    //페이지에 해당하는 객체들
    [SerializeField]
    private List<Transform> pages = new ();

    //초기설정
    private void Start()
    {
        //첫 페이지만 활성화 나머지는 비활성화
        page = 0;
        pages[0].gameObject.SetActive(true);
        for(int i = 1; i < pages.Count; i++)
        {
            pages[i].gameObject.SetActive(false);    
        }
    }

    //다음 페이지로 넘기기
    public void OnButtonNextPage()
    {
        //다음 페이지가 없다면 리턴
        if (page + 1 >= pages.Count)
        {
            return;
        }
        //목표 페이지만 활성화 나머지는 비활성화
        page++;
        for(int i = 0; i < pages.Count; i++)
        {
            if(i == page)
                pages[i].gameObject.SetActive(true);
            else
                pages[i].gameObject.SetActive(false);
        }
    }

    //이전 페이지로 넘기기
    public void OnButtonPrePage()
    {
        //이전 페이지가 없다면 리턴
        if (page - 1 < 0)
        {
            return;
        }
        //목표 페이지만 활성화 나머지는 비활성화
        page--;
        for (int i = 0; i < pages.Count; i++)
        {
            if (i == page)
                pages[i].gameObject.SetActive(true);
            else
                pages[i].gameObject.SetActive(false);
        }
    }
}
