using DIALOGUE;
using UnityEngine;
using System.Collections.Generic;

namespace TESTING
{
    public class TestParsing : MonoBehaviour
    {
        
       
        void Start()
        {
            SendFileToParse();

        }

        void SendFileToParse()
        {
            List<string> lines = FileManager.ReadTextAsset("testFile");

            foreach (string line in lines)
            {
                if(line == string.Empty)
                    continue;

                DIALOGUE_LINE dl = DialogueParser.Parse(line);
            }

        }

    }
}
