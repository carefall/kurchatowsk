using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestUi : MonoBehaviour

{
    [SerializeField] TextMeshProUGUI  texteds;
    [SerializeField] TMP_InputField inf,inf2,inf3;
    [SerializeField] Slider sl1;
    [SerializeField] Slider sl;
    [SerializeField] GameObject capsule;
    [SerializeField] TextMeshProUGUI texted;
    public void Button()
    {
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        for (int i = 0; i < sl1.value; i++)
        {
            yield return new WaitForSeconds(sl.value);
            float.TryParse(inf.text, out float x);
            float.TryParse(inf2.text, out float y);
            float.TryParse(inf3.text, out float z);
            capsule.transform.Translate(x, y, z);
        }
    }
    public void Textedr(Single s)
    {
        texted.text = $"Задержка: {s}";
    }
    public void Texteds(Single s)
    {
        texteds.text = $"Кол-во повторений: {s}";
    }
}
