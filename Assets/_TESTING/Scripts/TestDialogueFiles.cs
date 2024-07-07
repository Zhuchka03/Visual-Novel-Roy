using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogueFiles : MonoBehaviour
{
    [SerializeField] private TextAsset file;
    void Start()
    {
        StartConversation();
    }


    void StartConversation()
    {
        List<string> lines = FileManager.ReadTextAsset(file);
        DialogueSystem.instance.Say(lines);
       /* foreach (string line in lines)
        {
            if (line == string.Empty)
                continue;
            DIALOGUE_LINE dl = DialogueParser.Parse(line);
        }*/
    }
}
