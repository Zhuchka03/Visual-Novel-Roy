using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogueFiles : MonoBehaviour
{
    //[SerializeField] private TextAsset file;
    [SerializeField] private TextAsset fileToRead = null;
    void Start()
    {
        StartConversation();
    }


    void StartConversation()
    {
        List<string> lines = FileManager.ReadTextAsset(fileToRead);
        DialogueSystem.instance.Say(lines);
    }
}
