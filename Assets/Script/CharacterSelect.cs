using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField]
    private Character character;
    public Character Character { get { return character; } set { character = value; Apply(); } }

    private void Start()
    {
        Apply();
    }

    public void OnButtonSelect()
    {
        GameManager.Instance.Select = character;
    }

    private void Apply()
    {
        Image image = GetComponent<Image>();
        if (character != null)
            image.sprite = character.transform.GetChild(0).GetComponent<Image>().sprite;
        else
            image.sprite = null;
    }
}
