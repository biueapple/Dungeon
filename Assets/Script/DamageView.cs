using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DamageView : MonoBehaviour
{
    private static DamageView instance;
    public static DamageView Instance { get { return instance; } }

    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI;
    private readonly List<TextMeshProUGUI> texts = new ();

    private void Awake()
    {
        instance = this;
    }

    public void View(Character character, Color color, float value)
    {
        TextMeshProUGUI text = Instantiate(textMeshProUGUI, character.transform.position + new Vector3(Random.Range(-100,100), Random.Range(-100, 100), 0), Quaternion.identity, transform);
        text.color = color;
        text.text = value.ToString("F1");
        StartCoroutine(Delete(text));
    }

    private IEnumerator Delete(TextMeshProUGUI textMeshProUGUI)
    {
        texts.Add(textMeshProUGUI);
        yield return new WaitForSeconds(2);
        texts.Remove(textMeshProUGUI);
        Destroy(textMeshProUGUI);
    }
}
