using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TextLoader : MonoBehaviour
{
    public static TMPro.TMP_Text viewText;
    public static bool runTextPrint;
    public static int charCount;

    [SerializeField] string transferText;
    [SerializeField] int internalCount;

    
    void Update()
    {
        internalCount = charCount;

        charCount = GetComponent<TMPro.TMP_Text>().text.Length;

        if(runTextPrint == true)
        {
            runTextPrint = false;   
            viewText = GetComponent<TMPro.TMP_Text>();
            transferText = viewText.text;
            viewText.text = "";
            StartCoroutine(RollText());
        }
    }

    IEnumerator RollText()
    {
        foreach(char c in transferText)
        {
            viewText.text += c;
            yield return new WaitForSeconds(0.03f);
        }
    }
}
