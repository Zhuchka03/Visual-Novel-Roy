using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandTesting : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(Running());
    }


    IEnumerator Running()
    {
        yield return CommandManager.instance.Execute("print");
        yield return CommandManager.instance.Execute("print_1p", "Hello World!");
        yield return CommandManager.instance.Execute("print_mp", "Line1", "Line2", "Line3");

        yield return CommandManager.instance.Execute("lambda");
        yield return CommandManager.instance.Execute("lambda_1p", "Hello Lambdal!");
        yield return CommandManager.instance.Execute("lambda_mp", "Lambda1", "Lambda2", "Lambda3");

        yield return CommandManager.instance.Execute("process");
        yield return CommandManager.instance.Execute("process_1p", "3");
        yield return CommandManager.instance.Execute("process_mp", "Process.Line 1", "Process Line 2", "Process Line 3");

    }
}
