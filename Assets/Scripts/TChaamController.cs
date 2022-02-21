using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TChaamController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + 5.0f * horizontal * Time.deltaTime;
        position.y = position.y + 5.0f * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);

        if (transform.rotation.z != 0)
        {
            transform.Rotate(0, 0, -transform.rotation.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        collider.GetComponent<LevelSelection>().PressSelection("CharacterSelection");
        if (!collider.GetComponent<LevelSelection>().getUnlocked())
        {
            collider.isTrigger = false;
        }
    }

    // private void levelSelect(string name, int level)
    // {
    //     if (name == "wellLevel1")
    //     {
    //         SceneManager.LoadScene("CharacterSelection");
    //         LevelManager.instance.thislevel = level;
    //     }
    // }
}
