using UnityEngine;
using System.Collections;
using System;


public class SceneEvents : MonoBehaviour
{
    public GameObject mainText;

    [SerializeField] string textToSpeak;
    [SerializeField] int currentTextLength;
    [SerializeField] int textLength;
    [SerializeField] GameObject mainTextBox;

    private void Update()
    {
        textLength = TextLoader.charCount;
    }

    void Start()
    {
        StartCoroutine(EventStarter());
    }

    IEnumerator EventStarter()
    {
        yield return new WaitForSeconds(1);
        mainTextBox.SetActive(true);
        textToSpeak = "The creature stares at you blankly.";
        mainText.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextLoader.runTextPrint = true;

        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength > currentTextLength);
        yield return new WaitForSeconds(0.05f);

        mainText.SetActive(true);
    }
}
