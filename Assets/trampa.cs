using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class trampa : MonoBehaviour
{
    [SerializeField] private GameObject[] tramps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            foreach (var tramp in tramps)
            {
                var rb = tramp.gameObject.GetComponent<Rigidbody>();
                rb.isKinematic = false;
            }
            transform.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
