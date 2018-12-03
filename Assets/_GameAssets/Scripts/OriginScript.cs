using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginScript : MonoBehaviour {
    public GameObject prefabAsteriode;
    public int numAsteriodes;
	void Start () {
		for (int i = 0; i < numAsteriodes; i++)
        {
            int x = Random.Range(-50, 50);
            int y = Random.Range(-50, 50);
            int z = Random.Range(-50, 50);
            Instantiate(prefabAsteriode, new Vector3(x, y, z), Quaternion.identity);
        }
	}
	
	
}
