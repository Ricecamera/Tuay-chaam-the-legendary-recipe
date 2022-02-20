using System.Collections.Generic;
using UnityEngine;
using BattleScene;

public class Spawner : MonoBehaviour {
    public CharacterManager characters;

    public List<GameObject> plants = new List<GameObject>();        // contains prefabs of all in-play plants
    public List<GameObject> enemies = new List<GameObject>();       // contains prefabs of all in-play enemies
    public List<Transform> allySpawnPos = new List<Transform>();    // reference of allies' spawn positions in scene
    public List<Transform> enemySpawnPos = new List<Transform>();   // contains enemies' spawn positions in scene 

    [Header("Chaam")]
    public GameObject chaam;                // contains the prefab of an in-play chaam
    public Transform chaamSpawnPos;         // reference of chaam's spawn positon

    [Header("Boss")]
    public GameObject boss;                 // contains the prefab of an in-play boss
    public Transform bossSpawnPos;          // reference of boss' spawn position

    [Header("Other scripts")]
    public PakSelection pakSelection;     // reference of pakSelection object in scene

        
    void OnEnable() {
        characters.Intialize();

        for (int i = 0; i < plants.Count; i++) {
            GameObject p = Instantiate(plants[i], allySpawnPos[i].position, Quaternion.identity, allySpawnPos[i]);
            p.tag = allySpawnPos[i].gameObject.tag;
            characters.AddCharacter(p.tag, p, 0);
        }

        GameObject chaamObject = Instantiate(chaam, chaamSpawnPos.position, Quaternion.identity, chaamSpawnPos);
        chaamObject.tag = chaamSpawnPos.gameObject.tag;
        characters.AddCharacter(chaamObject.tag, chaamObject, 0);
            

        for (int i = 0; i < enemies.Count; i++) {
            GameObject e = Instantiate(enemies[i], enemySpawnPos[i].position, Quaternion.identity, enemySpawnPos[i]);
            e.tag = enemySpawnPos[i].gameObject.tag;
            characters.AddCharacter(e.tag, e, 1);
        }

        GameObject bossObject = Instantiate(boss, bossSpawnPos.position, Quaternion.identity, bossSpawnPos);
        bossObject.tag = bossSpawnPos.gameObject.tag;
        characters.AddCharacter(bossObject.tag, bossObject, 1);
    }
}
