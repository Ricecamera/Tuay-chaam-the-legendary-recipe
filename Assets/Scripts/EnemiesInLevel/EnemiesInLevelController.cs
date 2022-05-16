using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EnemiesInLevelController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject[] allWells;
    [SerializeField] private GameObject[] enemiesImageHolder;
    public LevelSelection wannaPlayLevel;
    private Dictionary<int, List<string>> enemiesInEachLevel;

    private void Start() {
        enemiesInEachLevel = new Dictionary<int, List<string>>();
        enemiesInEachLevel.Add(0,new List<string> {"potato", "potato"});
        enemiesInEachLevel.Add(1,new List<string> {"potato", "potato", "carrot"});
        enemiesInEachLevel.Add(2,new List<string> {"potato", "potato", "eggplant"});
        enemiesInEachLevel.Add(3,new List<string> {"potato", "potato", "normalprik", "garlic"});
        enemiesInEachLevel.Add(4,new List<string> {"potato", "normalprik", "potato", "kanah"});
        enemiesInEachLevel.Add(5,new List<string> {"potato", "normalprik", "yuak", "mund"});
        enemiesInEachLevel.Add(6,new List<string> {"potato", "prikthai", "potato", "brogli"});
        enemiesInEachLevel.Add(7,new List<string> {"potato", "yuak", "khaopod", "gluay"});
        enemiesInEachLevel.Add(8,new List<string> {"prikthai", "yuak", "normalprik", "cheepha"});
    }

    // private void OnEnable() {
    //     Debug.Log(transform.GetChild(11).gameObject);
    //     Debug.Log(transform.GetChild(11).gameObject.transform.GetChild(0).gameObject);
    //     Debug.Log(transform.GetChild(11).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text);
    //     transform.GetChild(11).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "1-" + wannaPlayLevel.levelname.ToString();
    // }

    public void closeCanvas(){
        canvas.enabled=false;
        unlockAllWells();
    }

    public void goToBattle(){
        if (SaveManager.instance.playerDatabase.unlockStatus == 4 &&
            SaveManager.instance.playerDatabase.cookSystemStatus == PlayerProgress.ACQUIRED)
        {
            SaveManager.instance.playerDatabase.cookSystemStatus = PlayerProgress.UNLOCKED;
            wannaPlayLevel.PressSelection("CookSystemTutorial");
            return;
        }
        wannaPlayLevel.PressSelection("CharacterSelection");
    }

    public void setLevelText(){
        transform.GetChild(11).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "1-" + wannaPlayLevel.levelname.ToString();
    }
    public void lockAllWells(){
        foreach (GameObject well in allWells)
        {
            well.GetComponent<BoxCollider2D>().enabled=false;
        }
    }
    public void unlockAllWells(){
        foreach (GameObject well in allWells)
        {
            well.GetComponent<BoxCollider2D>().enabled=true;
        }
    }

    public void setEnemiesImage(){
        foreach (GameObject e in enemiesImageHolder)
        {
            e.GetComponent<Image>().sprite=null;
        }
        int level = wannaPlayLevel.levelname;
        List<string> enemiesInThisLevel = enemiesInEachLevel[level];
        int enemiesCount = enemiesInThisLevel.Count;
        Debug.Log(enemiesCount);
        for (int i=0; i<enemiesCount; i++){
            // Debug.Log(String.Format("Loop enter {0}",i));
            enemiesImageHolder[i].GetComponent<Image>().sprite = Resources.Load(String.Format("PakImages/{0}", enemiesInThisLevel[i]), typeof(Sprite)) as Sprite;
            enemiesImageHolder[i].GetComponent<Image>().preserveAspect = true;
            enemiesImageHolder[i].GetComponent<Image>().color = new Color32(255,255,255,255);
        }
        for (int i=3; i>enemiesCount-1; i--){
            // Debug.Log("Empty last slot.");
            enemiesImageHolder[i].GetComponent<Image>().color = new Color32(255,255,255,0);
            enemiesImageHolder[i].GetComponent<Image>().preserveAspect = true;
        }
    }
}
