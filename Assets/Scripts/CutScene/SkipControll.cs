using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipControll : MonoBehaviour
{
    private float holdDownStartTime;
    [SerializeField] private GameObject holdToSkip; //hold to skip text
    private float counter; //counter for hold to skip text 
    private bool stateSkip; //state of hold to skip text
    [SerializeField] private Image load; //load circle
    [SerializeField] private float fillDuration;
    private float fillRate;
    [Range(0, 1)] private float progress = 0f;
    private bool loading;

    private void Start()
    {
        Reset();
        SetFillRate(fillDuration);
    }
    private void Update()
    {
        if (Input.anyKey)
        {
            holdToSkip.SetActive(true);
            counter = Time.time;
            stateSkip = true;
        }
        if (Time.time - counter >= 2f)
        {
            holdToSkip.SetActive(false);
            stateSkip = false;
        }
        if (stateSkip && Input.GetMouseButtonDown(0))
        {
            StartLoad();
        }
        if (stateSkip)
        {
            if (loading)
            {
                progress += fillRate * Time.deltaTime;
                if (progress < 1f)
                {
                    load.fillAmount = progress;
                    if (Input.GetMouseButtonUp(0))
                    {
                        loading = false;
                        progress = 0f;
                        load.fillAmount = progress;
                    }
                }
                else
                {
                    SceneLoader.Instance.LoadNextScene();
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Reset();
        }
    }

    public void SetFillRate(float fillDuration)
    {
        fillRate = 1.0f / fillDuration;
    }

    public void Reset()
    {
        progress = 0;
        loading = false;
        load.fillAmount = progress;
        // gameObject.SetActive(false);
    }

    public void StartLoad()
    {
        loading = true;
        // gameObject.SetActive(true);
    }
}
