using System;
using System.Collections.Generic;
using UnityEngine;
using BattleScene;

public class Spawner : MonoBehaviour
{

    public List<GameObject> plants = new List<GameObject>();         // contains prefabs of all in-play plants
    private List<GameObject> enemies = new List<GameObject>();       // contains prefabs of all in-play enemies

    // Add supports
    public List<GameObject> supports = new List<GameObject>();


    public List<Transform> allySpawnPos = new List<Transform>();    // reference of allies' spawn positions in scene
    public List<Transform> enemySpawnPos = new List<Transform>();   // contains enemies' spawn positions in scene
    public List<GameObject> healthBar = new List<GameObject>();

    [Header("Chaam")]
    public GameObject chaam;                // contains the prefab of an in-play chaam
    public Transform chaamSpawnPos;

    public bool havePreset;     // reference of chaam's spawn positon

    void OnEnable()
    {
        if (!havePreset)
        {
            Debug.Log("*********************************************************1");
            ConvertPlantToGameObject(CharacterSelecter.instance?.GetCharacters());
            ConvertChaamToGameObject(CharacterSelecter.instance?.GetChaam());
            ConvertSupportToGameObject(CharacterSelecter.instance?.GetSupports());
        }


        enemies = MapToEnemy.Instance.GetEnemies(LevelManager.instance.thislevel);

        try
        {

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
                CharacterManager.instance.AddCharacter(e.tag, e);
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
        {
            Debug.Log(itemObject.prefab.name);
            chaam = itemObject.prefab;
        }
        else
        {
            Debug.Log("Chaam not found");
        }
    }

    public void ClearCharacter()
    {

    }
}