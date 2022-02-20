using System.Collections.Generic;
using UnityEngine;
using BattleScene;

[RequireComponent(typeof(CharacterManager))]
public class Spawner : MonoBehaviour
{

    private CharacterManager characterManager;

    public List<GameObject> plants = new List<GameObject>();         // contains prefabs of all in-play plants
    public List<GameObject> enemies = new List<GameObject>();       // contains prefabs of all in-play enemies
    public List<Transform> allySpawnPos = new List<Transform>();    // reference of allies' spawn positions in scene
    public List<Transform> enemySpawnPos = new List<Transform>();   // contains enemies' spawn positions in scene
    public List<GameObject> healthBar = new List<GameObject>();

    [Header("Chaam")]
    public GameObject chaam;                // contains the prefab of an in-play chaam
    public Transform chaamSpawnPos;         // reference of chaam's spawn positon

    [Header("Boss")]
    public GameObject boss;                 // contains the prefab of an in-play boss
    public Transform bossSpawnPos;          // reference of boss' spawn position

    [Header("Other scripts")]
    public PakSelection pakSelection;     // reference of pakSelection object in scene



    void Start()
    {
        ConvertToGameObjectList(CharacterSelecter.instance.GetCharacters());
        ConvertChaamToGameObject(CharacterSelecter.instance.GetChaam());
        characterManager = GetComponent<CharacterManager>();
        characterManager.Intialize();

        for (int i = 0; i < plants.Count; i++)
        {
            GameObject p = Instantiate(plants[i], allySpawnPos[i].position, Quaternion.identity, allySpawnPos[i]);
            p.tag = allySpawnPos[i].gameObject.tag;
            p.GetComponent<PakRender>().plantpos = allySpawnPos[i].gameObject; // need to fix this
            allySpawnPos[i].GetComponent<PakRender>().plantpos = allySpawnPos[i].gameObject; // need to fix this

            // Vector3 pPos = new Vector3(allySpawnPos[i].position.x , allySpawnPos[i].position.y+1 , allySpawnPos[i].position.z); 
            // HealthBar hp = Instantiate(p.GetComponent<PakRender>().hp, pPos, Quaternion.identity, allySpawnPos[i]); 
            // pr = p.GetComponent<PakRender>();
            // HealthBar hp = pr.hp;
            // hp.SetMaxHealth(pr.pak.Hp);
            // Debug.Log(p.tag);
            // Debug.Log(hp.getHealth());
            characterManager.SetCharacter(p.tag, p, true);
        }

        GameObject chaamObject = Instantiate(chaam, chaamSpawnPos.position, Quaternion.identity, chaamSpawnPos);
        chaamObject.tag = chaamSpawnPos.gameObject.tag;
        chaamObject.GetComponent<PakRender>().plantpos = chaamSpawnPos.gameObject; // need to fix this
        chaamSpawnPos.GetComponent<PakRender>().plantpos = chaamSpawnPos.gameObject; // need to fix this
        characterManager.SetCharacter(chaamObject.tag, chaamObject, true);


        for (int i = 0; i < enemies.Count; i++)
        {
            GameObject e = Instantiate(enemies[i], enemySpawnPos[i].position, Quaternion.identity, enemySpawnPos[i]);
            e.tag = enemySpawnPos[i].gameObject.tag;
            e.GetComponent<PakRender>().plantpos = enemySpawnPos[i].gameObject;
            enemySpawnPos[i].GetComponent<PakRender>().plantpos = enemySpawnPos[i].gameObject;
            Debug.Log(e.tag);
            characterManager.SetCharacter(e.tag, e);
        }

        GameObject bossObject = Instantiate(boss, bossSpawnPos.position, Quaternion.identity, bossSpawnPos);
        bossObject.tag = bossSpawnPos.gameObject.tag;
        bossObject.GetComponent<PakRender>().plantpos = bossSpawnPos.gameObject; // need to fix this
        bossSpawnPos.GetComponent<PakRender>().plantpos = bossSpawnPos.gameObject;  // need to fix this
        characterManager.SetCharacter(bossObject.tag, bossObject);

        // for(int i = 0 ; i < 1;i++){
        //     GameObject e = Instantiate(healthBar[i], allySpawnPos[i].position, Quaternion.identity, allySpawnPos[i]);
        // }

        // for(int i = 0 ; i < enemies.Count;i++){
        //     GameObject e = Instantiate(healthBar[i], enemySpawnPos[i].position, Quaternion.identity, enemySpawnPos[i]);

        // }


        pakSelection.reset();
    }

    public void ConvertToGameObjectList(List<ItemObject> itemObjects)
    {
        foreach (var item in itemObjects)
        {
            plants.Add(item.prefab);
        }
    }

    public void ConvertChaamToGameObject(ItemObject itemObject)
    {
        chaam = itemObject.prefab;
    }

}