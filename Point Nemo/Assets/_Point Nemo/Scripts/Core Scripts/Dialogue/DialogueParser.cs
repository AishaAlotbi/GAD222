using UnityEngine;
using System.Text.RegularExpressions;

namespace DIALOGUE
{
    public class DialogueParser 
    {
        private const string commandRegexPattern = "\\w*[^\\s]\\("; //word of any length as long as its not preceded by a white space, then we're looking for ( 
        public static DIALOGUE_LINE Parse(string rawLine)
        {
            Debug.Log($"Parsing Line - '{rawLine}'");

            (string speaker, string dialogue, string comands) = RipContent(rawLine);

            Debug.Log($"Speaker = '{speaker}' \nDialogue = '{dialogue}' \nCommands = '{comands}'");

            return new DIALOGUE_LINE(speaker, dialogue, comands);
           
        }

        private static (string,string,string) RipContent(string rawLine)
        {
            string speaker = "", dialogue = "", commands = "";

            int dialogueStart = -1;
            int dialogueEnd = -1;
            bool isEscaped = false;

            for(int i = 0; i< rawLine.Length; i++)
            {
                char current = rawLine[i];
                if (current == '\\')
                    isEscaped = !isEscaped;
                else if (current == '"' && !isEscaped)
                {
                    if (dialogueStart == -1)
                        dialogueStart = i;
                    else if (dialogueEnd == -1)
                        dialogueEnd = i;
                }
                else
                    isEscaped = false;

            }

            //Identify Command Pattern
            Regex commandRegex = new Regex(commandRegexPattern);
            Match match = commandRegex.Match(rawLine);
            int commandStart = -1;
            if(match.Success)
            {
                commandStart = match.Index;

                if(dialogueStart == -1 && dialogueEnd == -1)
                    return ("", "", rawLine.Trim());
            }

            //we have dialogue or multi word argument in a command. Find out if this is dialogue or not

            if (dialogueStart != -1 && dialogueEnd != -1 && (commandStart == -1 || commandStart > dialogueEnd)) // if this is true then dialogue is found
            {
                //valid dialogue is available

                speaker = rawLine.Substring(0, dialogueStart).Trim();
                dialogue = rawLine.Substring(dialogueStart + 1, dialogueEnd - dialogueStart - 1).Replace("\\\"", "\"");
                if(commandStart != -1) 
                    commands = rawLine.Substring(commandStart).Trim();
            }
            else if (commandStart != -1 && dialogueStart > commandStart) //command line found
                commands = rawLine;
            else // if its niether of the two above then its the speaker alone
                speaker = rawLine;

                return (speaker, dialogue, commands);
        }
    }
}
