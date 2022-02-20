using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public InventoryObject inventory;

    // private void OnApplicationQuit()
    // {
    //     inventory.Container.Items = new InventorySlot[6];
    // }

    private void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        //     if (hit.collider != null)
        //     {
        //         Debug.Log("Hit Object");
        //         // inventory.AddItem(hit.collider.gameObject.GetComponent<Item>().item, 1);
        //         // Destroy(hit.collider.gameObject);
        //     }
        // }
    }

    public void SaveItem()
    {
        inventory.Save();
    }

    public void LoadItem()
    {
        // inventory.Load();
    }
}
