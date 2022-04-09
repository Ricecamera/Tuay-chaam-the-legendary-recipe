using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pair;
using UnityEngine.SceneManagement;

namespace Pair
{
    public class Pair<T, U>
    {
        public Pair()
        {
        }

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }

        public T First { get; set; }
        public U Second { get; set; }
    };
}

public class MapToEnemy : MonoBehaviour
{
    private Image bg;
    private Dictionary<int, Pair<List<GameObject>, Image>> enemyDictionary = new Dictionary<int, Pair<List<GameObject>, Image>>();

    public static MapToEnemy instance;

    public static MapToEnemy Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("MapToEnemy");
                go.AddComponent<MapToEnemy>();
                go.GetComponent<MapToEnemy>().SetEnemyMap();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    public void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Battle" || SceneManager.GetActiveScene().name == "Battle1-2V2")
        {
            // bg = GameObject.FindGameObjectWithTag("battleBG").GetComponent<Image>();
            // SetBG();
        }
        else
        {
            bg = null;
        }
        instance = this;
    }

    public void SetEnemy(int key, Pair<List<GameObject>, Image> enemies)
    {
        enemyDictionary.Add(key, enemies);
    }

    public void SetEnemyMap()
    {
        //TODO 2 -> Eggplant, 3 -> Garlic, 1 -> Carrot
        SetEnemy(0, new Pair<List<GameObject>, Image>(new List<GameObject>(){
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("potato")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("potato")),
        }, Resources.Load("BattleBG/default", typeof(Image)) as Image));

        SetEnemy(1, new Pair<List<GameObject>, Image>(new List<GameObject>(){
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("potato")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("potato")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("carrot"))
        }, Resources.Load("BattleBG/default", typeof(Image)) as Image));

        SetEnemy(2, new Pair<List<GameObject>, Image>(new List<GameObject>(){
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("potato")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("potato")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("eggplant"))
        }, Resources.Load("BattleBG/default", typeof(Image)) as Image));

        SetEnemy(3, new Pair<List<GameObject>, Image>(new List<GameObject>(){
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("potato")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("potato")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("normalprik")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("garlic"))
        }, Resources.Load("BattleBG/default", typeof(Image)) as Image));

        SetEnemy(4, new Pair<List<GameObject>, Image>(new List<GameObject>(){
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("potato")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("normalprik")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("potato")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("kanah"))
        }, Resources.Load("BattleBG/default", typeof(Image)) as Image));

        SetEnemy(5, new Pair<List<GameObject>, Image>(new List<GameObject>(){
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("potato")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("normalprik")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("yuak")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("mund"))
        }, Resources.Load("BattleBG/default", typeof(Image)) as Image));

        SetEnemy(6, new Pair<List<GameObject>, Image>(new List<GameObject>(){
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("potato")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("prikthai")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("potato")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("brogli"))
        }, Resources.Load("BattleBG/default", typeof(Image)) as Image));

        SetEnemy(7, new Pair<List<GameObject>, Image>(new List<GameObject>(){
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("potato")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("yuak")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("khaopod")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("gluay"))
        }, Resources.Load("BattleBG/default", typeof(Image)) as Image));

        SetEnemy(8, new Pair<List<GameObject>, Image>(new List<GameObject>(){
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("prikthai")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("yuak")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("normalprik")),
            ConvertToGameObject(DatabaseManager.instance.GetItemFromGameDB("cheepha"))
        }, Resources.Load("BattleBG/default", typeof(Image)) as Image));
    }


    public void SetBG()
    {
        bg = enemyDictionary[LevelManager.instance.thislevel].Second;
    }

    private GameObject ConvertToGameObject(ItemObject item)
    {
        return item.prefab;
    }

    public List<GameObject> GetEnemies(int level)
    {
        return enemyDictionary[level].First;
    }
}