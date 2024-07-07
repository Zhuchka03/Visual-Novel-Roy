using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TESTING
{


    public class TestParsing : MonoBehaviour
    {
        [SerializeField] private TextAsset file;
        void Start()
        {
            SendFileToParse();
        }


        void SendFileToParse()
        {
            List<string> lines = FileManager.ReadTextAsset(file);
            foreach (string line in lines)
            {
                if (line == string.Empty)
                    continue;
                DIALOGUE_LINE dl = DialogueParser.Parse(line);
            }
        }
    }
}
       /*
    public class TestParsing : MonoBehaviour
    {

        void Start()
        {
            string line = "Speaker \"Dialogue \\\"Goes In\\\"Here!\" Command(arguments here)";

            DialogueParser.Parse(line);
        }
        void Update()
        {

        }
    }
}*/