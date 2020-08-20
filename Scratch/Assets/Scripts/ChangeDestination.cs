using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDestination : MonoBehaviour
{
    public GameObject Terrain;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            var xPosition = Random.Range(0, 100);
            var zPosition = Random.Range(0, 100);
            gameObject.transform.position = new Vector3(xPosition, 1.5f, zPosition);
        }
    }
}
