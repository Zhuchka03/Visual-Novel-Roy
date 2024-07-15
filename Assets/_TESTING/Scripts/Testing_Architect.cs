using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using DIALOGUE;


namespace TESTING
{

    public class Testing_Architect : MonoBehaviour
    {
        DialogueSystem ds;
        TextArchitect architect;
        public TextMeshProUGUI  dT;
        string[] lines = new string[5]
        {
            "1Ewew ewewe.",
            "2Rrrr rrrrrr.",
            "3Rwww wwwwwww.",
            "4Trerer erererer.",
            "5Hddddd dddddddd."
        };
        // Start is called before the first frame update
        void Start()
        {
            ds = DialogueSystem.instance;
            dT = ds.dialogueContainer.dialogueText;
            // architect = new TextArchitect(ds.dialogueContainer.dialogueText); 
            //TextArchitect architect;
            //architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            // architect = Instantiate(TextArchitect(dT)) as TextArchitect;
            // TextArchitect
           // Debug.Log("===============begin1 ====================");
            architect = (TextArchitect)gameObject.AddComponent(typeof(TextArchitect)); //as TextArchitect;
            //architect =  FindObjectOfType<TextArchitect>();
            architect.buildMethod = TextArchitect.BuildMethod.typewriter;
            architect.tmpro_ui = dT;
            //Debug.Log("===============begin2 ====================");
            if (architect == null)
            {
                Debug.Log("x start i null + y");
            }

            if (ds.dialogueContainer.dialogueText == null) {

                Debug.Log("x ds is null+ y");
              }
            Debug.Log(ds.dialogueContainer.dialogueText);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("===============update 1 ====================");
                if (architect == null)
                 {
                
                //TextArchitect architect;
                //architect = gameObject.AddComponent(typeof(TextArchitect)) as TextArchitect;
                architect.buildMethod = TextArchitect.BuildMethod.typewriter;

               //architect = new TextArchitect(ds.dialogueContainer.dialogueText);
                    Debug.Log("x ‚‚‚‚‚‚‚‚‚‚‚‚‚‚‚‚ + y");
               }

                //   architect.Build(lines[Random.Range(0, lines.Length)]);

                if (architect == null)
                {
                    Debug.Log("architect is null");
                }
                architect.buildMethod = TextArchitect.BuildMethod.instant;
                string TN1;
                TN1 = lines[Random.Range(0, lines.Length)];
                Debug.Log("architect is null2="+ TN1);
                //architect.Build(lines[Random.Range(0, lines.Length-1)]);
                architect.targetText = TN1;
                architect.Build(TN1);

                //  architect.Build("ddedhudh uhd");
            }
           /*else if (Input.GetKeyDown(KeyCode.A)) //ÍÌÓÔÍË ÚÛÚ ÒÚ‡‚ËÚ¸
              
            {
                architect.Append(lines[Random.Range(0, lines.Length)]);
            }  */
        }
    }
}