using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace DIALOGUE
{


    public class DialogueSystem : MonoBehaviour
    {
       [SerializeField] private DialogueSystemConfigurationSO _config;
        public DialogueSystemConfigurationSO config => _config;

        DialogueSystem ds;
        public TextMeshProUGUI dT;
        public DialogueContainer dialogueContainer;// = new DialogueContainer();
        private ConversationManager conversationManager;
        private TextArchitect architect;

        public static DialogueSystem instance { get; private set; }

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
            if ((speakerName.ToLower() != "рассказчик") && (speakerName.ToLower() != "narrator"))
            {       
                dialogueContainer.nameContainer.Show(speakerName);
            }
            else
            {        
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