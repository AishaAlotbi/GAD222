using UnityEngine;

namespace DIALOGUE
{

    public class DIALOGUE_LINE 
    {
        public string speaker;
        public string dialogue;
        public string commands;

        public bool hasDialogue => dialogue != string.Empty;
        public bool hasCommands => commands != string.Empty;
        public bool hasSpeaker => speaker != string.Empty;
        public DIALOGUE_LINE(string speaker, string dialogue, string commands)
        {
            this.speaker = speaker;
            this.dialogue = dialogue;
            this.commands = commands;
        }
    }

}
