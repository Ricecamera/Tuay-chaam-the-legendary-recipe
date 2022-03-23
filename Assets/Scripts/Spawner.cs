using System;
using System.Collections.Generic;
using UnityEngine;
using BattleScene;

[RequireComponent(typeof(CharacterManager))]
public class Spawner : MonoBehaviour
{

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
    void OnEnable()
    {
        ConvertToGameObjectList(CharacterSelecter.instance?.GetCharacters());
        ConvertChaamToGameObject(CharacterSelecter.instance?.GetChaam());

        try {

            for (int i = 0; i < plants.Count; i++)
            {
                GameObject p = Instantiate(plants[i], allySpawnPos[i].position, Quaternion.identity, allySpawnPos[i]);
                p.tag = allySpawnPos[i].gameObject.tag;
                CharacterManager.instance.AddCharacter(p.tag, p);
            }

            GameObject chaamObject = Instantiate(chaam, chaamSpawnPos.position, Quaternion.identity, chaamSpawnPos);
            chaamObject.tag = chaamSpawnPos.gameObject.tag;
            CharacterManager.instance.AddCharacter(chaamObject.tag, chaamObject);

        }
        catch (Exception e) {
            Debug.LogError(e.Message);
        }
        finally {
            for (int i = 0; i < enemies.Count; i++) {
                GameObject e = Instantiate(enemies[i], enemySpawnPos[i].position, Quaternion.identity, enemySpawnPos[i]);
                e.tag = enemySpawnPos[i].gameObject.tag;
                CharacterManager.instance.AddCharacter(e.tag, e);
            }

            if (boss != null) {
                GameObject bossObject = Instantiate(boss, bossSpawnPos.position, Quaternion.identity, bossSpawnPos);
                bossObject.tag = bossSpawnPos.gameObject.tag;
                CharacterManager.instance.AddCharacter(bossObject.tag, bossObject);
            }
        }     
    }

    public void ConvertToGameObjectList(List<ItemObject> itemObjects)
    {
        if (itemObjects == null)
            return;

        foreach (var item in itemObjects)
        {
            plants.Add(item.prefab);
        }
    }

    public void ConvertChaamToGameObject(ItemObject itemObject)
    {
        if(itemObject != null)
            chaam = itemObject.prefab;
    }

}