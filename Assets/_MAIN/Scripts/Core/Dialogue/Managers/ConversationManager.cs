
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static UnityEngine.Rendering.HableCurve;


namespace DIALOGUE
{
    public class ConversationManager
    {

        private DialogueSystem dialogueSystem => DialogueSystem.instance;

        private Coroutine process = null;

        public bool isRunning => process != null;
        private TextArchitect architect = null;

        private bool userPrompt = false;

        public ConversationManager(TextArchitect architect)
        {
            this.architect = architect;
            this.architect.targetText = "eeeeee";
            dialogueSystem.onUserPrompt_Next += OnUserPrompt_Next;
        }

        private void OnUserPrompt_Next()
        {
            userPrompt = true;

        }


        public void StartConversation(List<string> conversation)
        {

            StopConversation();
            process = dialogueSystem.StartCoroutine(RunningConversation(conversation));
        }
        public void StopConversation()
        {

            if (!isRunning)
                return;

            dialogueSystem.StopCoroutine(process);
            process = null;
        }



        IEnumerator RunningConversation(List<string> conversation)
        {

            for (int i = 0; i < conversation.Count; i++)
            {
                //this.architect.targetText = conversation[i];
                //this.architect.Build(conversation[i]);
                //Dont-show any blank lines or try to run any logic on-them.
                if (string.IsNullOrWhiteSpace(conversation[i]))
                    continue;

                DIALOGUE_LINE line = DialogueParser.Parse(conversation[i]);

                //Show dialogue
                if (line.hasDialogue)
                    yield return Line_RunDialogue(line);

                //Run any commands
                if (line.hasCommands)
                    yield return Line_RunCommands(line);

            }
        }

        IEnumerator Line_RunDialogue(DIALOGUE_LINE line)
        {

            //Show or hide the speaker name if there is one present.
            if (line.hasSpeaker)
                dialogueSystem.ShowSpeakerName(line.speakerData.displayname);


            //build dialogue
            yield return BuildLineSegments(line.dialogueData);
            //wait for user input
            yield return WaitForUserInput();
        }

        IEnumerator Line_RunCommands(DIALOGUE_LINE line)    ////��� ���� ��������� ������ 18.07 30� ������ ���-��
        {
            Debug.Log(line.commandsData);
            yield return null;

        }
        IEnumerator BuildLineSegments(DL_DIALOGUE_DATA line)
        {
            for (int i = 0; i < line.segments.Count; i++)
            { 
                DL_DIALOGUE_DATA.DIALOGUE_SEGMENT segment = line.segments[i];

                yield return WaitForDialogueSegmentSignalToBeTriggered(segment);

                yield return BuildDialogue(segment.dialogue, segment.appendText);
        }
    }
        IEnumerator WaitForDialogueSegmentSignalToBeTriggered(DL_DIALOGUE_DATA.DIALOGUE_SEGMENT segment) 
        { 

            switch(segment.startSignal)
            {
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.C:
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.A:
                    yield return WaitForUserInput();
                    break;
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.WC:
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.WA:
                    yield return new WaitForSeconds(segment.signalDelay); 
                    break;
                   default:
                break;
            }
        }


       /*
       public void ForceComplete(){
           isBuilding  = false;
           }
       */


       IEnumerator BuildDialogue(string dialogue, bool append = false)
        {
            //Build the dialogue
            if (!append)
                architect.Build(dialogue);
            else
                architect.Append(dialogue);
           
            //Wait for the dialogue to complete
             while (architect.isBuilding)
             {
                 if (userPrompt)
                 {
                     if (!architect.hurryUp)
                         architect.hurryUp = true; //false or true
                     else
                        // architect.ForceComplete();    ///!!!!!!
                     userPrompt = false;
                 }
                 yield return null;
             }

        }
        IEnumerator WaitForUserInput()
        {
            while (!userPrompt)
                yield return null;
            userPrompt = false;
        }
    }
}