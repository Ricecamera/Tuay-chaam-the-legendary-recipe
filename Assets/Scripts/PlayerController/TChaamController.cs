using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DialogueSystem;

public class TChaamController : MonoBehaviour
{
    public static TChaamController instance;
    Rigidbody2D rigidbody2d;
    public float horizontal;
    public float vertical;

    public float JumpForce = 1;

    private float counter;
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

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        counter = counter + Time.deltaTime;



        position.x = position.x + 6.0f * horizontal * Time.deltaTime;
        position.y = position.y + 6.0f * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);

    }


}
