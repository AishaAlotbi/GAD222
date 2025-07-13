using UnityEngine;
using DIALOGUE;

namespace TESTING
{



    public class TestingArchitect : MonoBehaviour
    {
        DialogueSystem ds;
        TextArchitect architect;
        public TextArchitect.BuildMethod bm = TextArchitect.BuildMethod.instant;

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
            architect.buildMethod = TextArchitect.BuildMethod.fade;
            architect.speed = 0.5f;
           
        }

        // Update is called once per frame
        void Update()
        {
            if(bm != architect.buildMethod)
            {
                architect.buildMethod = bm;
                architect.Stop();
            }

            if (Input.GetKeyDown(KeyCode.S))
                architect.Stop();


            string longLine = "very very very long line needed to test this, its not supposed to make sense its just here to be loooooooooooooooooooooooong and test if this code is working. Adding more stuff to make it even longer, add more its very greedy.";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(architect.isBuilding)
                {
                    if(!architect.hurryUp)
                        architect.hurryUp = true;
                    else
                        architect.ForceComplete();
                }
                else
                    architect.Build(longLine);
                //architect.Build(lines[Random.Range(0, lines.Length)]);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                architect.Append(longLine);
                //architect.Append(lines[Random.Range(0, lines.Length)]);

            }
          
        }
    }
}
