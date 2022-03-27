using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DialogueSystem
{
    public class TutorialHolder : MonoBehaviour
    {

        [Header("Character Animations")]
        [SerializeField] private Animator tonhom;
        [SerializeField] private Animator chaam;
        [SerializeField] private Animator mah;
        [SerializeField] private Animator pointer;
        [SerializeField] private Animator pointer2;
        [SerializeField] private GameObject textBox;
        [SerializeField] private GameObject skillBox;
        [SerializeField] private GameObject background;

        private void Awake()
        {
            StartCoroutine(tutorialSequence());
        }

        public IEnumerator tutorialSequence()
        {
            Deactivate();
            yield return new WaitForSeconds(1.55f);
            for (int i = 0; i < 8; i++)
            {
                Deactivate();
                Debug.Log(i);
                if (i < 2)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
                if (i == 1) //* Dialogue สอนผู้เล่น
                {
                    pointer.gameObject.SetActive(true);
                    pointer.SetTrigger("Pointer");
                    yield return new WaitUntil(() => WaitTrigger("Tonhom"));
                }
                else
                {
                    pointer.gameObject.SetActive(false);
                }

                if (i == 2) //* กดตัวต้นหอม
                {
                    pointer.gameObject.SetActive(false);
                    textBox.SetActive(false);
                    skillBox.SetActive(true);
                    yield return new WaitUntil(() => WaitTrigger("sk1"));
                }
                else if (i == 3) //* กดสกิล
                {
                    background.GetComponent<Image>().color = new Color32(173, 103, 0, 255);
                    skillBox.transform.GetChild(5).gameObject.SetActive(false);
                }
                else if (i == 4) //* กดเลือกศัตรู
                {
                    // background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    textBox.SetActive(true);
                    skillBox.SetActive(false);
                    pointer2.gameObject.SetActive(true);
                    yield return new WaitForSeconds(1.5f);
                    transform.GetChild(2).gameObject.SetActive(true);
                    yield return new WaitUntil(() => WaitTrigger("Mah_EMOT 1_0"));
                }
                else if (i == 5)
                { //* ตีหมา
                    background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    textBox.SetActive(false);
                    pointer2.gameObject.SetActive(false);
                    tonhom.SetTrigger("Attack");
                }
                else if (i == 6)
                { //* หมาหนี

                    yield return new WaitForSeconds(1.5f);
                    textBox.SetActive(true);
                    yield return new WaitForSeconds(0.5f);
                    transform.GetChild(3).gameObject.SetActive(true);
                }
                if (i < 2)
                {
                    yield return new WaitUntil(() => transform.GetChild(i).GetComponent<TutorialLine>().isFinish);
                }
                else if (i == 4)
                {
                    yield return new WaitUntil(() => transform.GetChild(2).GetComponent<TutorialLine>().isFinish);
                }
                else if (i == 6)
                {
                    yield return new WaitUntil(() => transform.GetChild(3).GetComponent<TutorialLine>().isFinish);
                }
                else if (i == 7)
                {
                    SceneLoader.Instance.LoadNextScene();
                }
            }
            gameObject.SetActive(false);
        }

        public void Deactivate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }


        public bool WaitTrigger(string name)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.name == name)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool AlwaysTrue()
        {
            return true;
        }
    }
}