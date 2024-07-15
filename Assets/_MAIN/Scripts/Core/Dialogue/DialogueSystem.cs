using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DIALOGUE;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;

namespace DIALOGUE
{
  

    public class DialogueSystem : MonoBehaviour
    {
        DialogueSystem ds;
        public TextMeshProUGUI dT;
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

            //architect = new TextArchitect(dialogueContainer.dialogueText); //?
            architect = (TextArchitect)gameObject.AddComponent(typeof(TextArchitect)); //as TextArchitect;
            architect.targetText = "uuuuuuuuuuuuuuu5";
            //architect.currentText = "";
            ds = instance;
            dT = ds.dialogueContainer.dialogueText;

             architect.tmpro_ui = dT;
             architect.buildMethod = TextArchitect.BuildMethod.instant;
           // architect.buildMethod = TextArchitect.BuildMethod.instant;
       
            conversationManager = new ConversationManager(architect);
        }


        public void OnUserPrompt_Next() 
        {
            onUserPrompt_Next?.Invoke();
        }


        
        public void ShowSpeakerName(string speakerName = "")
        {
            Debug.Log("It is wooooork0");
            if ((speakerName.ToLower() != "����������") && (speakerName.ToLower() != "narrator"))
            {
                Debug.Log("speakerName1 ="+ speakerName.ToLower() + "|||||||||||||");

                dialogueContainer.nameContainer.Show(speakerName);
            }
            else
            {
                Debug.Log("speakerName2 =" + speakerName.ToLower() + "}}}}}}}}}}}");
                dialogueContainer.nameContainer.Hide();
                
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