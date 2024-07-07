using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DIALOGUE;

namespace DIALOGUE
{

    public class DialogueSystem : MonoBehaviour
    {
        public DialogueContainer dialogueContainer;// = new DialogueContainer();
        private ConversationManager conversationManager;
        private TextArchitect architect;

        public static DialogueSystem instance;

        public delegate void DialogueSystemEvent(); 
        public event DialogueSystemEvent onUserPrompt_Next; 
        public bool isRunningConversation => conversationManager.isRunning;

        private void Awake() {

            if (instance == null)
            { 
                instance = this;
                Initialize();
            }
            else
            
                DestroyImmediate(gameObject);
        }

        bool _initialized = false;
        private void Initialize()
        {

            if (_initialized)
                return;

            architect = new TextArchitect(dialogueContainer.dialogueText);
            conversationManager = new ConversationManager(architect);
        }


        public void OnUserPrompt_Next() 
        {
            onUserPrompt_Next?.Invoke();
        }


        //Problem now
        public void ShowSpeakerName(string speakerName = "")
        {
            Debug.Log("It is wooooork0");
            if ((speakerName.ToLower() != "Рассказчик") || (speakerName.ToLower() != "Narrator"))
            {
                Debug.Log("It is wooooork1");
                dialogueContainer.nameContainer.Show(speakerName);
            }
            else
            {
                Debug.Log("It is wooooork2");
                HideSpeakerName();
            }
        }
        public void HideSpeakerName() => dialogueContainer.nameContainer.Hide();


        public void Say(string speaker, string dialogue) {

            List<string> conversation = new List<string>() { $"{speaker} \"{dialogue}\"" };
            Say(conversation);
        }
        public void Say(List<string> conversation)
        {

            conversationManager.StartConversation(conversation);
        }

    }
}