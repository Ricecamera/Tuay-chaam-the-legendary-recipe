using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TChaamController : MonoBehaviour
{
    public static TChaamController instance;
    Rigidbody2D rigidbody2d;
    public float horizontal;
    public float vertical;

    public float JumpForce = 1;

    private float counter;
    private float frequency;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            rigidbody2d = GetComponent<Rigidbody2D>();
            counter = 0;
            frequency = 2;
            horizontal = 0;
            vertical = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {        

    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        counter = counter + Time.deltaTime;
        // if (vertical == 0)
        // {
        //     position.y = position.y + (0.01f * Mathf.Sin(counter * frequency));
        // }
        // else
        // {
        //     counter = 0;
        // }


        position.x = position.x + 6.0f * horizontal * Time.deltaTime;
        position.y = position.y + 6.0f * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // if (collider.name.Contains("Square"))
        // {
        //     Debug.Log(collider.name);
        //     if (collider.name.CompareTo("SquareUp") == 0)
        //     {

        //         Debug.Log("Go into if ");
        //         rigidbody2d.MovePosition(new Vector2(0, 0));


        //     }
        // }
        Debug.Log("Hit smth");
        if (collider.name.Contains("well"))
        {
            Debug.Log("Hit well");
            collider.GetComponent<LevelSelection>().PressSelection("CharacterSelection");
            if (!collider.GetComponent<LevelSelection>().getUnlocked())
            {
                collider.isTrigger = false;
            }
        }

    }
}
