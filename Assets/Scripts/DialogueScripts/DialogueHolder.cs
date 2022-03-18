using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {

        [Header("Character Animations")]
        [SerializeField] private Animator tonhom;
        [SerializeField] private Animator chaam;

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
                Debug.Log(i);
                transform.GetChild(i).gameObject.SetActive(true);
                if (transform.GetChild(i).name.Contains("Tonhom"))
                {
                    tonhom.SetTrigger("Come_in");
                    chaam.SetTrigger("Come_out");
                    Debug.Log(transform.childCount);
                }
                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().isFinish || Input.GetMouseButtonDown(0));
            }
            gameObject.SetActive(false);
            LevelLoader.instance.LoadSpecificScene("TutorialScene");
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