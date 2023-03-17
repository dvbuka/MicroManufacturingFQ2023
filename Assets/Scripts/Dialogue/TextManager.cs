using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    //Singleton
    public static TextManager instance {get; private set;}
    public bool holdFlag;
    GameObject TextBox;
    
    private void Awake() {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            TextBox = GameObject.Find("Textbox");
            holdFlag = false;
        }
    }
    private bool isPlayingText = false;
    private KeyCode advanceTextKeycode = KeyCode.Space;

    public void EvokeText(TextParent[] _Text)
    {
        if (!isPlayingText)
        {
            isPlayingText = true;
            StartCoroutine(PlayText(_Text));
        }
        isPlayingText = false;
    }

    private IEnumerator PlayText(TextParent[] _Text)
    {
        foreach (TextParent _text in _Text)
        {
            _text.Display();
            yield return new WaitForSeconds(0.1f);
            yield return StartCoroutine(WaitForPlayerInput(advanceTextKeycode));
            yield return new WaitUntil(() => holdFlag == false);

        }
        yield return null;
    }

    public GameObject GetTextBox()
    {
        return TextBox;
    }

    private IEnumerator WaitForPlayerInput(KeyCode _keycode)
    {
        while ((!Input.GetKeyDown(_keycode) && holdFlag == false) || control.isPaused() == control.pauseStates.menuPaused)
        {
            yield return null;
        }
    }
}
