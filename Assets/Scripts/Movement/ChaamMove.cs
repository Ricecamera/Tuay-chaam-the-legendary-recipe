using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaamMove : MonoBehaviour
{
    // Start is called before the first frame update
    //move in x position
    private float counter;
    private float frequency;

    public float heightParams;


    void Start()
    {
        counter = 0;
        frequency = 2;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 position = gameObject.transform.position;
        counter = counter + Time.deltaTime;
        position.y = position.y + (heightParams * Mathf.Sin(counter * frequency));
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, position.y, gameObject.transform.position.z);
    }
}
