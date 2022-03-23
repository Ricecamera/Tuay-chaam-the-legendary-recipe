using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(dialogueSequence());
        }

        public IEnumerator dialogueSequence()
        {
            Deactivate();
            yield return new WaitForSeconds(1.55f);
            for (int i = 0; i < transform.childCount; i++)
            {
                Deactivate();
                Debug.Log(transform.GetChild(i).name);
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().isFinish);
            }
            gameObject.SetActive(false);

            SceneLoader.Instance.LoadNextScene();
        }

        public void Deactivate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        public bool isEndDialogue(int index)
        {
            return index == transform.childCount;
        }
    }
}