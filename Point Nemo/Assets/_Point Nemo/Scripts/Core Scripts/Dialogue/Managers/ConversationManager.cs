using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE
{
    public class ConversationManager 
    {
        private DialogueSystem dialogueSystem => DialogueSystem.instance;
        private Coroutine process = null;
      
        public bool isRunning => process != null;

        private TextArchitect architect = null;
        private bool userPrompt = false;

        public Conversation conversation => (conversationQueue.IsEmpty() ? null : conversationQueue.top);

        private ConversationQueue conversationQueue;
        public ConversationManager(TextArchitect architect)
        {
            this.architect = architect;
            dialogueSystem.onUserPrompt_Next += OnUserPrompt_Next;

            conversationQueue = new ConversationQueue();
        }

        public void Enqueue(Conversation conversation) => conversationQueue.Enqueue(conversation);
        public void EnqueuePriority(Conversation conversation) => conversationQueue.EnqueuePriority(conversation);

        private void OnUserPrompt_Next()
        {
            userPrompt = true;
        }

        public Coroutine StartConversation(Conversation conversation)
        {
            StopConversation();

            Enqueue(conversation);

            process = dialogueSystem.StartCoroutine(RunningConversation());

            return process;
        }
       
        public void StopConversation()
        {
            if(!isRunning)
                return;

            dialogueSystem.StopCoroutine(process);
            process = null;

        }
        IEnumerator RunningConversation()
        {
            while(!conversationQueue.IsEmpty())
            {
                Conversation currentConversation = conversation;
                string rawLine = currentConversation.CurrentLine();

                if (string.IsNullOrWhiteSpace(rawLine)) //dont run logic on or show blank lines
                {
                    TryAdvanceConversation(currentConversation);
                    continue;
                }  

                DIALOGUE_LINE line = DialogueParser.Parse(rawLine);

                //Show Dialogue
                if (line.hasDialogue)
                    yield return Line_RunDialogue(line);
                
                //Run any Commands
                if (line.hasCommands)
                    yield return Line_RunCommands(line);

                TryAdvanceConversation(currentConversation);
                
            }

            process = null;
        }

        private void TryAdvanceConversation(Conversation conversation)
        {
            conversation.IncrementProgress();
            if (conversation.HadReachedEnd())
                conversationQueue.Dequeue();

        }

        IEnumerator Line_RunDialogue(DIALOGUE_LINE line)
        {
            if (line.hasSpeaker) //Show or hide speaker if present
                dialogueSystem.ShowSpeakerName(line.speaker);
            else
                dialogueSystem.HideSpeakerName();

            //Build Dialogue
            yield return BuildDialogue(line.dialogue);

            //Wait for user input
            yield return WaitForUserInput();
                
        }

        IEnumerator Line_RunCommands(DIALOGUE_LINE line)
        {
            Debug.Log(line.commands);
            yield return null;
        }

        IEnumerator BuildDialogue(string dialogue)
        {
            architect.Build(dialogue);

            while (architect.isBuilding)
            {
                if (userPrompt)
                {
                    if (!architect.hurryUp)
                        architect.hurryUp = true;
                    else
                        architect.ForceComplete();

                    userPrompt = false;
                }

                yield return null;
            }
        }

        IEnumerator WaitForUserInput()
        {
            while(!userPrompt)
                yield return null;

            userPrompt = false;
        }
    }
}
