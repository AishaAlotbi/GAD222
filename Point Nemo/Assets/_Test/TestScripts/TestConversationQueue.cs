using DIALOGUE;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestConversationQueue : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Running());
    }

    IEnumerator Running()
    {
        List<string> lines = new List<string>()
        {
            "This is line 1",
            "This is line 2",
            "This is line 3",
        };

        yield return DialogueSystem.instance.Say(lines);

        //DialogueSystem.instance.Hide();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
