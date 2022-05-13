using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace DialogueSystem
{
    public class MapTutorialHolder : MonoBehaviour
    {

        [Header("Character Animations")]
        // [SerializeField] private GameObject pointer;
        // [SerializeField] private GameObject pointer2;
        [SerializeField] private GameObject textBox;
        [SerializeField] private GameObject Backdrop;
        [SerializeField] private PlayerDatabase playerDB;
        [SerializeField] private GameObject[] lockedWell;
        [SerializeField] private GameObject unlockedWell;
        [SerializeField] private GameObject arrowButtonsUp;
        [SerializeField] private GameObject arrowButtonsDown;
        [SerializeField] private GameObject settingButton;
        [SerializeField] private GameObject homeButton;

        // [SerializeField] private GameObject well1;
        // [SerializeField] private GameObject well2;
        // [SerializeField] private GameObject well3;
        // [SerializeField] private GameObject homeArrow;
        // [SerializeField] private GameObject well3Arrow;
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

            // yield return new WaitForSeconds(1.55f);
            
            switch (playerDB.unlockStatus)
            {
                case 1: //first time entered map
                    Debug.Log("first time");
                    // LevelManager.instance.mapArrived+=1;
                    for (int i = 0; i<8; i++){
                        Deactivate();
                        Debug.Log(i);
                        // if(i>=7) i=7;
                        switch (i)
                        {
                            case 0:
                                arrowButtonsUp.SetActive(false);
                                arrowButtonsDown.SetActive(false);
                                settingButton.SetActive(false);
                                homeButton.SetActive(false);
                                textBox.SetActive(true);
                                unlockedWell.GetComponent<BoxCollider2D>().enabled=false;
                                transform.GetChild(i).gameObject.SetActive(true);
                                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<TutorialLine>().isFinish);
                                break;

                            case 1:
                                transform.GetChild(i).gameObject.SetActive(true);
                                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<TutorialLine>().isFinish);
                                break;
                            
                            case 2:
                                transform.GetChild(i).gameObject.SetActive(true);
                                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<TutorialLine>().isFinish);
                                break;

                            case 3:
                                Backdrop.SetActive(true);
                                bringNonUIToFront(unlockedWell,"VeryFront");
                                // bringNonUIToFront(unlockedWell.transform.GetChild(1).);
                                foreach (GameObject well in lockedWell)
                                {
                                    bringNonUIToFront(well,"VeryFront");
                                    bringNonUIToFront(well.transform.GetChild(0).gameObject, "MoreThanVeryFront");
                                    // bringNonUIToFront(well.transform.GetChild(1).);
                                }
                                transform.GetChild(i).gameObject.SetActive(true);
                                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<TutorialLine>().isFinish);
                                break;

                            case 4:
                                foreach (GameObject well in lockedWell)
                                {
                                    bringNonUIToBack(well,"Default");
                                    bringNonUIToBack(well.transform.GetChild(0).gameObject,"Front");
                                    // bringNonUIToBack(well.transform.GetChild(1).);
                                }   
                                transform.GetChild(i).gameObject.SetActive(true);
                                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<TutorialLine>().isFinish);
                                break;

                            case 5:
                                bringNonUIToBack(unlockedWell,"Default");
                                // bringNonUIToBack(unlockedWell.transform.GetChild(1).);
                                foreach (GameObject well in lockedWell)
                                {
                                    bringNonUIToFront(well,"VeryFront");
                                    bringNonUIToFront(well.transform.GetChild(0).gameObject, "MoreThanVeryFront");
                                    // bringNonUIToFront(well.transform.GetChild(1).);
                                }
                                transform.GetChild(i).gameObject.SetActive(true);
                                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<TutorialLine>().isFinish);
                                break;

                            case 6:
                                bringNonUIToFront(unlockedWell,"VeryFront"); 
                                // bringNonUIToFront(unlockedWell.transform.GetChild(1).);
                                unlockedWell.GetComponent<BoxCollider2D>().enabled=true;
                                foreach (GameObject well in lockedWell)
                                {
                                    bringNonUIToBack(well,"Default");
                                    bringNonUIToBack(well.transform.GetChild(0).gameObject,"Front");
                                    // bringNonUIToBack(well.transform.GetChild(1).);
                                }
                                transform.GetChild(i).gameObject.SetActive(true);
                                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<TutorialLine>().isFinish);
                                break;

                            case 7://stay at case 6
                                unlockedWell.GetComponent<BoxCollider2D>().enabled=true;
                                transform.GetChild(6).gameObject.SetActive(true);
                                transform.GetChild(6).gameObject.GetComponent<Text>().text="Now let's begin your adventure! Click on the well to enter the well.";
                                break;
                            default:
                                Debug.LogError("wrong message index");
                                break;
                        }
                    }
                    break;
                
                case 2: //second time entered map
                    Debug.Log("second time");
                    // LevelManager.instance.mapArrived+=1;
                    for(int i=0; i<6; i++){
                        Deactivate();
                        Debug.Log(i);
                        switch (i)
                        {
                            case 0:
                                Backdrop.SetActive(true);
                                arrowButtonsUp.SetActive(false);
                                arrowButtonsDown.SetActive(false);
                                settingButton.SetActive(false);
                                homeButton.SetActive(false);
                                textBox.SetActive(true);
                                unlockedWell.GetComponent<BoxCollider2D>().enabled=false;
                                foreach (GameObject well in lockedWell)
                                {
                                    well.GetComponent<BoxCollider2D>().enabled=false;
                                }
                                transform.GetChild(7).gameObject.SetActive(true);
                                yield return new WaitUntil(() => transform.GetChild(7).GetComponent<TutorialLine>().isFinish);
                                break;
                            case 1:
                                arrowButtonsUp.SetActive(true);
                                arrowButtonsDown.SetActive(true);
                                arrowButtonsUp.GetComponent<Button>().enabled = false;
                                arrowButtonsDown.GetComponent<Button>().enabled = false;
                                transform.GetChild(8).gameObject.SetActive(true);
                                yield return new WaitUntil(() => transform.GetChild(8).GetComponent<TutorialLine>().isFinish);
                                break;

                            case 2:
                                homeButton.SetActive(true);
                                homeButton.GetComponent<Button>().enabled = false;
                                transform.GetChild(9).gameObject.SetActive(true);
                                yield return new WaitUntil(() => transform.GetChild(9).GetComponent<TutorialLine>().isFinish);
                                break;

                            case 3:
                                settingButton.SetActive(true);
                                settingButton.GetComponent<Button>().enabled = false;  
                                transform.GetChild(10).gameObject.SetActive(true);
                                yield return new WaitUntil(() => transform.GetChild(10).GetComponent<TutorialLine>().isFinish);
                                break;

                            case 4: 
                                transform.GetChild(11).gameObject.SetActive(true);
                                yield return new WaitUntil(() => transform.GetChild(11).GetComponent<TutorialLine>().isFinish);
                                break;

                            case 5://disable tutorial UI
                                unlockedWell.GetComponent<BoxCollider2D>().enabled=true;
                                foreach (GameObject well in lockedWell)
                                {
                                    well.GetComponent<BoxCollider2D>().enabled=true;
                                }
                                arrowButtonsUp.GetComponent<Button>().enabled = true;
                                arrowButtonsDown.GetComponent<Button>().enabled = true;
                                homeButton.GetComponent<Button>().enabled = true;
                                settingButton.GetComponent<Button>().enabled = true;                                                                
                                Backdrop.SetActive(false);
                                textBox.SetActive(false);
                                break;

                            default:
                                Debug.LogError("wrong message index");
                                break;
                        }
                    }
                    break;

                default:
                    Debug.Log("No more tuorial.");
                    unlockedWell.GetComponent<BoxCollider2D>().enabled=true;
                    foreach (GameObject well in lockedWell)
                    {
                        well.GetComponent<BoxCollider2D>().enabled=true;
                    }
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
            // gameObject.SetActive(false);
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

        private void bringNonUIToFront(GameObject theObject, string layer){
            SpriteRenderer theObjectSprite = theObject.GetComponent<SpriteRenderer>();
            theObjectSprite.sortingLayerName = layer;
        }

        private void bringNonUIToBack(GameObject theObject, string layer){
            SpriteRenderer theObjectSprite = theObject.GetComponent<SpriteRenderer>();
            theObjectSprite.sortingLayerName = layer;
        }
    }
}