using System.Collections;
using UnityEngine;
using DialogueSystem;
using UnityEngine.UI;
public class ModeSelectTutorialHolder : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        GameObject.Find("refrigerator").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("cupboard").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Door").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Door").GetComponent<Button>().enabled = false;
        StartCoroutine(tutorialSequence());
    }


    public IEnumerator tutorialSequence()
    {
        Deactivate();
        yield return new WaitForSeconds(1.55f);

        for (int i = 0; i < 5; i++)
        {

            Deactivate();
            Debug.Log(i);
            transform.GetChild(i).gameObject.SetActive(true);

            switch (i)
            {
                case 2:
                    GameObject.Find("refrigerator").transform.GetChild(0).gameObject.SetActive(true);
                    break;
                case 3:
                    GameObject.Find("refrigerator").transform.GetChild(0).gameObject.SetActive(false);
                    GameObject.Find("cupboard").transform.GetChild(0).gameObject.SetActive(true);
                    break;
                case 4:
                    GameObject.Find("refrigerator").transform.GetChild(0).gameObject.SetActive(false);
                    GameObject.Find("cupboard").transform.GetChild(0).gameObject.SetActive(false);
                    GameObject.Find("Door").transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("Door").GetComponent<Button>().enabled = true;
                    break;
                default:
                    break;
            }
            yield return new WaitUntil(() => transform.GetChild(i).GetComponent<TutorialLine>().isFinish);
        }
    }
    public void Deactivate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
