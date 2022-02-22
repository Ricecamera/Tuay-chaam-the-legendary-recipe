using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class TutorialLine : DialogueBasesClass
    {
        [SerializeField] private string input; // The text that will be displayed in the dialogue box
        private Text dialogueHolder;

        private void Awake()
        {
            dialogueHolder = GetComponent<Text>();
            dialogueHolder.text = "";
            // Debug.Log(DialogueDatabase.instance.GetDialogue());
        }

        private void Start()
        {
            StartCoroutine(WriteText(input, dialogueHolder));
        }
    }
}
