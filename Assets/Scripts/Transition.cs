using System.Collections;
using UnityEngine;

public class Transition : MonoBehaviour {
    SpriteRenderer rend;
    // Start is called before the first frame update
    [SerializeField]
    private float delayTime;


    void Start() {
        rend = GetComponentInChildren<SpriteRenderer>();

        if (rend == null) return;
        
        Debug.Log(rend);
        Color c = rend.material.color;
        c.a = 0f;
        rend.material.color = c;

        startFadeIn();
    }

    IEnumerator FadeIn() {
        yield return new WaitForSeconds(delayTime);
        float f;
        for (f = 0.05f; f <= 1; f += 0.05f) {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.005f);
        }
    }

    public void startFadeIn() {


        StartCoroutine("FadeIn");
    }

}
