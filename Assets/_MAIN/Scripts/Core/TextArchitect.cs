using System.Collections;
using UnityEngine;
using TMPro;
using UnityEditor;
//using static TextArchitect;

public class TextArchitect: MonoBehaviour
{
    //private TextMeshProUGUI tmpro_ui;
    public TextMeshProUGUI tmpro_ui;
    private TextMeshPro tmpro_world;

    public TMP_Text tmpro => tmpro_ui != null ? tmpro_ui : tmpro_world;

    public string currentText => tmpro.text;
    //public string targetText { get; private set; } = "";
    public string targetText { get; set; } = "";
    public string preText { get; private set; } = "";
    //private int preTextLength = 0;

    public string fullTargetText => preText + targetText;

    public enum BuildMethod { instant, typewriter, fade }
    public BuildMethod buildMethod = BuildMethod.typewriter;

    public Color textColor { get { return tmpro.color; } set { tmpro.color = value; } }


    public float speed { get { return baseSpeed * speedMultiplier; } set { speedMultiplier = value; } }
    private const float baseSpeed = 1;
    private float speedMultiplier = 1;

    public int charactersPerCycle { get { return speed <= 2f ? characterMultiplier : speed <= 2.5f ? characterMultiplier * 2 : characterMultiplier * 3; } }
    private int characterMultiplier = 1;

    public bool hurryUp = false;


    public TextArchitect(TextMeshProUGUI tmpro_ui)
    {
        this.tmpro_ui = tmpro_ui;
    }


    private Coroutine buildProcess = null;

    public bool isBuilding => buildProcess != null;
    public void Stop()
    {
        if (!isBuilding)
            return;
        tmpro.StopCoroutine(buildProcess);
        buildProcess = null;
    }

    public Coroutine Build(string text)
    {
        preText = "";
        targetText = text;
        if (buildProcess != null)
        {
            Debug.Log("=======StopCoroutine===="+ targetText + "============");
            tmpro.StopCoroutine(buildProcess);
            buildProcess = tmpro.StartCoroutine(Building());
        }
        else
        {
            Debug.Log(" buildProcess=" + targetText +"======2========");
            //buildMethod = BuildMethod.typewriter;
            buildProcess = tmpro.StartCoroutine(Building());
        }
        
        // tmpro.StopCoroutine(buildProcess);
        //Stop();
        //Debug.Log("targetText=" + targetText);
        //buildMethod = BuildMethod.typewriter;
       // buildProcess = tmpro.StartCoroutine(Building());
        //buildProcess = tmpro.StartCoroutine(Building());
        //Debug.Log(" buildProcess=" + buildProcess);

        return buildProcess;
    }

    public Coroutine Append(string text)
    {
        preText = tmpro.text;
        targetText = text;

        Stop();
        Debug.Log("targetText_Append=" + targetText);
        buildProcess = tmpro.StartCoroutine(Building());
        return buildProcess;
    }


    IEnumerator Building()
    {
        Prepare();

        switch (buildMethod)
        {

            case BuildMethod.typewriter:
                yield return Build_Typewriter();
                break;
            case BuildMethod.fade:
                yield return Build_Fade();
                break;
        }
        OnComplete();
    }

    private void OnComplete()
    {
        buildProcess = null;
    }


    private void Prepare()
    {

        switch (buildMethod)
        {
            case BuildMethod.instant:
                Prepare_Instant();
                break;
            case BuildMethod.typewriter:
                Prepare_Typewriter();
                break;
            case BuildMethod.fade:
                Prepare_Fade();
                break;
        }
    }


    private void Prepare_Instant()
    { 
        tmpro.color = tmpro.color;
        tmpro.text = fullTargetText;
        tmpro.ForceMeshUpdate();
        tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
    
        }


    private void Prepare_Typewriter()
    {
        tmpro.color = tmpro.color;
        tmpro.maxVisibleCharacters = 0;
        tmpro.text = preText;
        if (preText != "")
        {
            tmpro.ForceMeshUpdate();
            tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
        }
        tmpro.text += targetText;
        tmpro.ForceMeshUpdate();
    }

    private void Prepare_Fade()
    {

    }
    private IEnumerator Build_Typewriter()
    {
        while (tmpro.maxVisibleCharacters < tmpro.textInfo.characterCount)
        {
            tmpro.maxVisibleCharacters += hurryUp ? charactersPerCycle * 5 : charactersPerCycle;
            yield return new WaitForSeconds(0.015f / speed);

        }
      // ForceComplete();???/

    }
    private IEnumerator Build_Fade()
    {
        yield return null;
    }

}














