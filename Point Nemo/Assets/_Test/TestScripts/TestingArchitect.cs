using UnityEngine;

namespace TESTING
{



    public class TestingArchitect : MonoBehaviour
    {
        DialogueSystem ds;
        TextArchitect architect;
        string[] lines = new string[5]
        {
            "Good hunter of the church, have you seen the thread of light?",
            "Three cords of the eye.",
            "We are born of the blood, made men by the blood.",
            "Ah, you were at my side all along. My true mentor, my guiding moonlight.",
            "Majestic! A hunter is a hunter even in a dream!"
        };

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            ds = DialogueSystem.instance;
            architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            architect.buildMethod = TextArchitect.BuildMethod.instant;
           
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                architect.Build(lines[Random.Range(0,lines.Length)]);
          
        }
    }
}
