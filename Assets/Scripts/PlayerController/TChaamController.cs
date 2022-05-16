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
    [SerializeField] private Canvas enemiesInLevelCanvas;
    [SerializeField] private PlayerDatabase playerDB;
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
            enemiesInLevelCanvas.enabled = false;
        }
        else
        {
            Destroy(gameObject);
        }

        // set map unlock
        LevelManager.instance.resetUnlockStatus();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            //* Press well and goes to level
            // if (hit.collider != null && hit.collider.name.Contains("well")) {
            //     Debug.Log("Click on well");
            //     hit.collider.GetComponent<LevelSelection>().PressSelection("CharacterSelection");
            //     if (!hit.collider.GetComponent<LevelSelection>().getUnlocked())
            //     {
            //         hit.collider.isTrigger = false;
            //     }        
            // }
            if (hit.collider != null && hit.collider.name.Contains("well") && hit.collider.GetComponent<LevelSelection>().getUnlocked() && playerDB.unlockStatus > 1)
            {
                Debug.Log("Click on well");
                enemiesInLevelCanvas.enabled = true;
                // GameObject.Find("enemyInLevelCanvas").GetComponent<EnemiesInLevelController>().level=hit.collider.GetComponent<LevelSelection>().Level;
                enemiesInLevelCanvas.GetComponent<EnemiesInLevelController>().wannaPlayLevel = hit.collider.GetComponent<LevelSelection>();
                enemiesInLevelCanvas.GetComponent<EnemiesInLevelController>().setLevelText();
                enemiesInLevelCanvas.GetComponent<EnemiesInLevelController>().lockAllWells();
                enemiesInLevelCanvas.GetComponent<EnemiesInLevelController>().setEnemiesImage();
            }
            else if (hit.collider != null && hit.collider.name.Contains("well") && playerDB.unlockStatus == 1)
            {
                Debug.Log("Click on well");
                hit.collider.GetComponent<LevelSelection>().PressSelection("CharacterSelectionTutorial");
                if (!hit.collider.GetComponent<LevelSelection>().getUnlocked())
                {
                    hit.collider.isTrigger = false;
                }
            }
        }
    }

    // void FixedUpdate()
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
        // Debug.Log(position.y);

        rigidbody2d.MovePosition(position);

    }

    // private void OnTriggerEnter2D(Collider2D collider)
    // {
    //     // if (collider.name.Contains("Square"))
    //     // {
    //     //     Debug.Log(collider.name);
    //     //     if (collider.name.CompareTo("SquareUp") == 0)
    //     //     {

    //     //         Debug.Log("Go into if ");
    //     //         rigidbody2d.MovePosition(new Vector2(0, 0));


    //     //     }
    //     // }
    //     Debug.Log("Hit smth");
    //     if (collider.name.Contains("well"))
    //     {
    //         Debug.Log("Hit well");
    //         collider.GetComponent<LevelSelection>().PressSelection("CharacterSelection");
    //         if (!collider.GetComponent<LevelSelection>().getUnlocked())
    //         {
    //             collider.isTrigger = false;
    //         }
    //     }

    // }
}
