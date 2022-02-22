using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBasesClass
    {
        [SerializeField] private string input; // The text that will be displayed in the dialogue box
        private Text dialogueHolder;

        [Header("Character Image")]
        [SerializeField] private Sprite characterSprite;
        [SerializeField] private Image imageHolder;

        private void Awake()
        {
            dialogueHolder = GetComponent<Text>();
            dialogueHolder.text = "";
            // Debug.Log(DialogueDatabase.instance.GetDialogue());
            imageHolder.sprite = characterSprite;
            imageHolder.preserveAspect = true;
        }

        private void Start()
        {
            StartCoroutine(WriteText(input, dialogueHolder));
        }
    }
}

