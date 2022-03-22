using System;
using System.Collections.Generic;
using UnityEngine;
using BattleScene;

[RequireComponent(typeof(CharacterManager))]
public class Spawner : MonoBehaviour
{

    public CharacterManager characters;

    public List<GameObject> plants = new List<GameObject>();         // contains prefabs of all in-play plants
    public List<GameObject> enemies = new List<GameObject>();       // contains prefabs of all in-play enemies

    // Add supports
    public List<GameObject> supports = new List<GameObject>();


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

    void OnEnable()
    {
        characters.Intialize();
        ConvertPlantToGameObject(CharacterSelecter.instance?.GetCharacters());
        ConvertChaamToGameObject(CharacterSelecter.instance?.GetChaam());
        //ConvertSupportToGameObject(CharacterSelecter.instance?.GetSupports());
        Debug.Log(CharacterSelecter.instance?.GetSupports()[0]);

        try
        {

            for (int i = 0; i < plants.Count; i++)
            {
                GameObject p = Instantiate(plants[i], allySpawnPos[i].position, Quaternion.identity, allySpawnPos[i]);
                p.tag = allySpawnPos[i].gameObject.tag;
                characters.AddCharacter(p.tag, p, 0);
            }

            GameObject chaamObject = Instantiate(chaam, chaamSpawnPos.position, Quaternion.identity, chaamSpawnPos);
            chaamObject.tag = chaamSpawnPos.gameObject.tag;
            characters.AddCharacter(chaamObject.tag, chaamObject, 0);

        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
        finally
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                GameObject e = Instantiate(enemies[i], enemySpawnPos[i].position, Quaternion.identity, enemySpawnPos[i]);
                e.tag = enemySpawnPos[i].gameObject.tag;
                characters.AddCharacter(e.tag, e, 1);
            }

            if (boss != null)
            {
                GameObject bossObject = Instantiate(boss, bossSpawnPos.position, Quaternion.identity, bossSpawnPos);
                bossObject.tag = bossSpawnPos.gameObject.tag;
                characters.AddCharacter(bossObject.tag, bossObject, 1);
            }
        }
    }

    public void ConvertPlantToGameObject(List<ItemObject> itemObjects)
    {
        if (itemObjects == null)
            return;

        foreach (var item in itemObjects)
        {
            plants.Add(item.prefab);
        }
    }

    public void ConvertSupportToGameObject(List<ItemObject> itemObjects)
    {
        if (itemObjects == null)
        {
            Debug.Log("itemObjects is NULL");
            return;
        }

        Debug.Log("itemObjects is NOT NULL");
        foreach (var item in itemObjects)
        {
            supports.Add(item.prefab);
        }
    }

    public void ConvertChaamToGameObject(ItemObject itemObject)
    {
        if (itemObject != null)
            chaam = itemObject.prefab;
    }

}