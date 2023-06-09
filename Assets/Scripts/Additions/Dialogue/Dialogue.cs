using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CASP.SoundManager;
public class Dialogue : MonoBehaviour
{
    public TextMeshPro _text;
    public string[] _lines;
    public float _textSpeed;
    private int _index;

    private void Start() {
        _text.text = string.Empty;
        StartDialogue();
    }
    private void Update() {
        if(InputReader.Instance.isInteractionPressedOneTime)
        {
            if(_text.text == _lines[_index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                _text.text = _lines[_index];
            }
        }
    }
    private void StartDialogue()
    {
        _index = 0;
         SoundManager.Instance.Play("AngelVoice");
        StartCoroutine(TypeLine());
        UIManager.Instance.AngleQuestionMark.GetComponent<ParticleSystem>().Stop();
    }

    private void NextLine()
    {
        if(_index < _lines.Length -1)
        {
            _index++;
            _text.text = string.Empty;
             StartCoroutine(TypeLine());
            
        }

        else
        {
            gameObject.SetActive(false);
            // UIManager.Instance.QuestManager(true);
            
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char l in _lines[_index].ToCharArray())
        {
            _text.text += l;
            yield return new WaitForSeconds(_textSpeed);
        }
    }
}
