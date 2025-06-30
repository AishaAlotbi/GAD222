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
    [SerializeField] GameObject nextButton;
    [SerializeField] int eventPos = 0;

    [SerializeField] GameObject choicesPanel;
    [SerializeField] TMPro.TMP_Text choice1Text;
    [SerializeField] TMPro.TMP_Text choice2Text;
    [SerializeField] UnityEngine.UI.Button choice1Button;
    [SerializeField] UnityEngine.UI.Button choice2Button;

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
        // event 0
        yield return new WaitForSeconds(1);
        CharacterText("The woods of Nemo do not often welcome visitors. Yet it seems even the air here pities your frail form. A rare opportunity for one such as yourself, you'd do well not to squander it. \r\n");

        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength >= currentTextLength);
        yield return new WaitForSeconds(0.05f);
        eventPos = 1;
        nextButton.SetActive(true);

    }


    IEnumerator Event(int eventNumber)
    {
        if (eventNumber == 1)
        {
            CharacterText("The silence of the night engulfs you like crashing waves. A stark difference from the streets you called home, where sound mimicked a flowing waterfall.");
            yield return new WaitForSeconds(0.05f);
            yield return new WaitForSeconds(1);
            yield return new WaitUntil(() => textLength >= currentTextLength);
            yield return new WaitForSeconds(0.05f);

            nextButton.SetActive(true);
            eventPos = 2; 
            //nextButton.SetActive(true);
        }
        else if (eventNumber == 2)
        {
            CharacterText("You’re torn on whether you should feel comforted or unnerved. It's as if something waits there, lingering in the quiet. Your eyes are heavy, your diseased body aches, you fall asleep still cautious. \r\n");
            yield return new WaitForSeconds(1);
            yield return new WaitUntil(() => textLength >= currentTextLength);
            eventPos = 3;
            nextButton.SetActive(true);
        }
        else if (eventNumber == 3)
        {
            CharacterText("The convenience the city provides to those of your profession is something you greatly miss at this moment. Your research will not progress with an empty stock of herbs. Whilst your body detests the idea, you're required to venture forth and collect your own.\r\n \r\n");
            yield return new WaitForSeconds(0.05f);
            yield return new WaitForSeconds(1);
            yield return new WaitUntil(() => textLength >= currentTextLength);
            yield return new WaitForSeconds(0.05f);
            nextButton.SetActive(true);
            eventPos = 4;
        }
        else if (eventNumber == 4)
        {
            CharacterText("The vegetation present in the woods is something that your mind struggles to comprehend. You begin to collect your odd findings, careful of them making contact with your own bare skin. \r\n");
            yield return new WaitForSeconds(0.05f);
            yield return new WaitForSeconds(1);
            yield return new WaitUntil(() => textLength >= currentTextLength);
            yield return new WaitForSeconds(0.05f);
            nextButton.SetActive(true);
            eventPos = 5;
        }
        else if (eventNumber == 5)
        {
            CharacterText("Your satchel is full, but has space to hold more. Do you intend on collecting more before heading back?");
            yield return new WaitForSeconds(0.05f);
            yield return new WaitForSeconds(1);
            yield return new WaitUntil(() => textLength >= currentTextLength);
            ShowChoices("Venture further, collect more.\r\n", "Head back, stop for the day.");
            yield return new WaitForSeconds(0.05f);
            //nextButton.SetActive(true);
            //eventPos = 6;
        }
        else if (eventNumber == 6)
        {
            CharacterText("You continue deeper into the woods, collecting anything you spot. You don't quite know what they are exactly, but you intend to find out. ");
            yield return new WaitForSeconds(0.05f);
            yield return new WaitForSeconds(1);
            yield return new WaitUntil(() => textLength >= currentTextLength);
            yield return new WaitForSeconds(0.05f);
            nextButton.SetActive(true);
            eventPos = 7;
        }
        else if (eventNumber == 7)
        {
            CharacterText("A sweet scent catches you off guard. It primates through the air, thick like sap. \r\n");
            yield return new WaitForSeconds(0.05f);
            yield return new WaitForSeconds(1);
            yield return new WaitUntil(() => textLength >= currentTextLength);
            yield return new WaitForSeconds(0.05f);
            nextButton.SetActive(true);
            eventPos = 8;
        }

    }

    IEnumerator VentureFurther()
    {
        CharacterText("Leaves echo as the breeze ruffles them, rustling in an odd steccato. Plants sway in tandem with the wind, hypnotic in their movement. You struggle to shake off the paranoia clinging to you from last night. You remain vigilant, looking over your shoulder periodically. But nothing is there, except for the trees, bystanders standing silently.");
        yield return new WaitUntil(() => textLength >= currentTextLength);
        eventPos = 6;
        nextButton.SetActive(true);
    }

    IEnumerator HeadBack()
    {
        CharacterText("You return home for the night");
        yield return new WaitUntil(() => textLength >= currentTextLength);
        
        nextButton.SetActive(false);
    }

    public void NextButton()
    {
        StartCoroutine(Event(eventPos));
    }

    void CharacterText(string characterText)
    {
        mainTextBox.SetActive(true);
        nextButton.SetActive(false);
        mainText.SetActive(true);
        textToSpeak = characterText;
        mainText.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextLoader.runTextPrint = true;

    }

    void ShowChoices(string option1, string option2)
    {
        nextButton.SetActive(false);
        choicesPanel.SetActive(true);

        choice1Text.text = option1;
        choice2Text.text = option2;

        choice1Button.onClick.RemoveAllListeners();
        choice2Button.onClick.RemoveAllListeners();

        choice1Button.onClick.AddListener(() => OnChoiceSelected(1));
        choice2Button.onClick.AddListener(() => OnChoiceSelected(2));
    }

    public void OnChoiceSelected(int choice)
    {
        choicesPanel.SetActive(false);

        if (choice == 1)
        {
            StartCoroutine(VentureFurther());
        }
        else if (choice == 2)
        {
            StartCoroutine(HeadBack());
        }
    }
}
