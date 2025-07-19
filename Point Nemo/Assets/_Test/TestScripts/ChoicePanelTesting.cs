using System.Collections;
using UnityEngine;

public class ChoicePanelTesting : MonoBehaviour
{
    ChoicePanel panel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        panel = ChoicePanel.instance;

        StartCoroutine(Running());
    }

   IEnumerator Running()
    {
        string[] choices = new string[]
        {
            "Interact with the creature.",
            "Go Home.",
            "Kill it.",
            "Pet it"
        };

        panel.Show("What do you intent to do?", choices);

        while(panel.isWaitingOnUserChoice)
            yield return null;
        var decision = panel.lastDecision;

        Debug.Log($"Made Choice {decision.answerIndex} '{decision.choices[decision.answerIndex]}");
    }

}
