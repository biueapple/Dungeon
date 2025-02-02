using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour
{
    [SerializeField]
    CharacterSelect[] candidate;
    [SerializeField]
    CharacterSelect[] select;
    [SerializeField]
    Button next;

    public void Init()
    {
        for (int i = 0; i < select.Length; i++) 
        {
            select[i].Character = null;
        }
    }

    public void Select(int index)
    {
        for (int i = 0; i < select.Length; i++)
        {
            if (select[i].Character != null && select[i].Character == GameManager.Instance.Select)
            {
                select[i].Character = null;
            }
        }
        select[index].Character = GameManager.Instance.Select;
        Check();
    }

    public void Check()
    {
        for (int i = 0; i < select.Length; i++)
        {
            if (select[i].Character == null)
            {
                next.gameObject.SetActive(false);
                return;
            }
        }

        next.gameObject.SetActive(true);
    }

    public void GameStart()
    {
        GameManager.Instance.CharacterSelectFinish(select);
    }
}
