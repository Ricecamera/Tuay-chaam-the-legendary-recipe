using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{

    public class DialogueBasesClass : MonoBehaviour
    {
        public bool isFinish = false;
        protected IEnumerator WriteText(string text, Text dialogueHolder)
        {
            for (int i = 0; i < text.Length; i++)
            {
                dialogueHolder.text += text[i];
                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            isFinish = true;
        }
    }
}


