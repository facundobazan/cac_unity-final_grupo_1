using UnityEngine;

public class Map : Singleton<Map>
{
    public GameObject map;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            map.SetActive(!map.activeSelf);
        }
    }
}
