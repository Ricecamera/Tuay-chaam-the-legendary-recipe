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
        [SerializeField] private GameObject textBox;
        [SerializeField] private GameObject Backdrop;
        [SerializeField] private PlayerDatabase playerDB;
        [SerializeField] private GameObject[] lockedWell;
        [SerializeField] private GameObject unlockedWell;
        [SerializeField] private GameObject arrowButtonsUp;
        [SerializeField] private GameObject arrowButtonsDown;
        [SerializeField] private GameObject settingButton;
        [SerializeField] private GameObject homeButton;


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
                                transform.GetChild(6).gameObject.GetComponent<Text>().text="Now let's begin your adventure! Click on the well to enter battle.";
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