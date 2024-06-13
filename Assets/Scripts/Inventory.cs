using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    public GameObject inventory;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.SetActive(!inventory.activeSelf);
        }
    }
}
