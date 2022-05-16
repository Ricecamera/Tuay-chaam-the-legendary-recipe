using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitManager : MonoBehaviour
{
    public void quit()
    {
        SceneLoader.Instance.LoadSceneByName("opened-map");
    }
}
