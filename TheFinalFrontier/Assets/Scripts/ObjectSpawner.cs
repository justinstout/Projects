using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    public GameObject AmmoCrate;
    

    /*
    IEnumerator SpawnAnObject()
    {
        yield return new WaitForSeconds(3f);
        Vector2 spawnPoint = new Vector2(  35.5f, -35f  );
        Instantiate(AmmoCrate, spawnPoint, Quaternion.identity);
    }
    
*/

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine( SpawnAnObject() );
    }

  
}
