using System.Collections;
using System.Collections.Generic;
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
