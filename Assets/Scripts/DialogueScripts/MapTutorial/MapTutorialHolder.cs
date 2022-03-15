using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DialogueSystem
{
    public class MapTutorialHolder : MonoBehaviour
    {

        [Header("Character Animations")]
        // [SerializeField] private GameObject pointer;
        // [SerializeField] private GameObject pointer2;
        [SerializeField] private GameObject textBox;
        [SerializeField] private GameObject homeButton;
        [SerializeField] private GameObject well1;
        [SerializeField] private GameObject well2;
        [SerializeField] private GameObject well3;
        [SerializeField] private GameObject homeArrow;
        [SerializeField] private GameObject well3Arrow;
        // [SerializeField] private GameObject background;

        private void Awake()
        {
            StartCoroutine(tutorialSequence());
        }

        /*
            flow
            first time: show i=0 to i=4
                        disable home button
                        disable all colider except the one to enter level 1-3.
            second time: show i==5;
            third time or more: show nothing

        */
        public IEnumerator tutorialSequence()
        {
            Deactivate();
            yield return new WaitForSeconds(1.55f);
            switch (LevelManager.instance.mapArrived)
            {
                case 0: //first time entered map
                    Debug.Log("first time");
                    LevelManager.instance.mapArrived+=1;
                    for (int i = 0; i<6; i++){
                        Deactivate();
                        Debug.Log(i);
                        switch (i)
                        {
                            case 0:
                                textBox.SetActive(true);
                                homeButton.GetComponent<Button>().enabled = false;
                                well1.GetComponent<BoxCollider2D>().isTrigger=false;
                                well2.GetComponent<BoxCollider2D>().isTrigger=false;
                                well3.GetComponent<BoxCollider2D>().isTrigger=false;
                                homeArrow.SetActive(false);
                                well3Arrow.SetActive(false);
                                textBox.SetActive(true);
                                transform.GetChild(i).gameObject.SetActive(true);
                                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<TutorialLine>().isFinish);
                                break;

                            case 1:
                                transform.GetChild(i).gameObject.SetActive(true);
                                homeArrow.SetActive(true);
                                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<TutorialLine>().isFinish);
                                break;
                            
                            case 2:
                                homeArrow.SetActive(false);
                                transform.GetChild(i).gameObject.SetActive(true);
                                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<TutorialLine>().isFinish);
                                break;

                            case 3:
                                transform.GetChild(i).gameObject.SetActive(true);
                                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<TutorialLine>().isFinish);
                                break;

                            case 4:
                                well3Arrow.SetActive(true);
                                transform.GetChild(i).gameObject.SetActive(true);
                                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<TutorialLine>().isFinish);
                                break;
                            case 5:
                                well3.GetComponent<BoxCollider2D>().isTrigger=true;
                                textBox.SetActive(false);
                                break;

                            default:
                                Debug.LogError("wrong message index");
                                break;
                        }
                    }
                    break;
                
                case 1: //second time entered map
                    Debug.Log("second time");
                    LevelManager.instance.mapArrived+=1;
                    for(int i=0; i<2; i++){
                        switch (i)
                        {
                            case 0:
                                homeButton.GetComponent<Button>().enabled = false;
                                well1.GetComponent<BoxCollider2D>().isTrigger=false;
                                well2.GetComponent<BoxCollider2D>().isTrigger=false;
                                well3.GetComponent<BoxCollider2D>().isTrigger=false;
                                textBox.SetActive(true);
                                transform.GetChild(5).gameObject.SetActive(true);
                                yield return new WaitUntil(() => transform.GetChild(5).GetComponent<TutorialLine>().isFinish);
                                break;
                            case 1:
                                homeButton.GetComponent<Button>().enabled = true;
                                well1.GetComponent<BoxCollider2D>().isTrigger=true;
                                well2.GetComponent<BoxCollider2D>().isTrigger=true;
                                well3.GetComponent<BoxCollider2D>().isTrigger=true;
                                textBox.SetActive(false);
                                break;

                            default:
                                Debug.LogError("wrong message index");
                                break;
                        }
                    }
                    break;

                default:
                    Debug.Log("third time");
                    break;
            }
            // for (int i = 0; i < 7; i++)
            // {
            //     Deactivate();
            //     Debug.Log(i);
            //     if (i < 2)
            //     {
            //         transform.GetChild(i).gameObject.SetActive(true);
            //     }
            //     if (i == 1) //* Dialogue สอนผู้เล่น
            //     {
            //         pointer.gameObject.SetActive(true);
            //         pointer.SetTrigger("Pointer");
            //         yield return new WaitUntil(() => WaitTrigger("Tonhom"));
            //     }
            //     else
            //     {
            //         pointer.gameObject.SetActive(false);
            //     }

            //     if (i == 2) //* กดตัวต้นหอม
            //     {
            //         pointer.gameObject.SetActive(false);
            //         textBox.SetActive(false);
            //         skillBox.SetActive(true);
            //         yield return new WaitUntil(() => WaitTrigger("sk1"));
            //     }
            //     else if (i == 3) //* กดสกิล
            //     {
            //         background.GetComponent<Image>().color = new Color32(173, 103, 0, 255);
            //         skillBox.transform.GetChild(5).gameObject.SetActive(false);
            //     }
            //     else if (i == 4) //* กดเลือกศัตรู
            //     {
            //         // background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            //         textBox.SetActive(true);
            //         skillBox.SetActive(false);
            //         pointer2.gameObject.SetActive(true);
            //         yield return new WaitForSeconds(1.5f);
            //         transform.GetChild(2).gameObject.SetActive(true);
            //         yield return new WaitUntil(() => WaitTrigger("Mah_EMOT 1_0"));
            //     }
            //     else if (i == 5)
            //     { //* ตีหมา
            //         background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            //         textBox.SetActive(false);
            //         pointer2.gameObject.SetActive(false);
            //     }
            //     else if (i == 6)
            //     { //* หมาหนี

            //         yield return new WaitForSeconds(1.5f);
            //         textBox.SetActive(true);
            //         yield return new WaitForSeconds(0.5f);
            //         transform.GetChild(3).gameObject.SetActive(true);
            //     }
            //     if (i < 2)
            //     {
            //         yield return new WaitUntil(() => transform.GetChild(i).GetComponent<TutorialLine>().isFinish);
            //     }
            //     else if (i == 4)
            //     {
            //         yield return new WaitUntil(() => transform.GetChild(2).GetComponent<TutorialLine>().isFinish);
            //     }
            //     else if (i == 6)
            //     {
            //         yield return new WaitUntil(() => transform.GetChild(3).GetComponent<TutorialLine>().isFinish);
            //     }

            // }
            gameObject.SetActive(false);
            // LevelLoader.instance.LoadSpecificScene("VictoryScene");
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