using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroideScript : MonoBehaviour {
    private float angularSpeed;
    private void Start()
    {
        angularSpeed = Random.Range(0, 2);
    }
    void Update () {
        transform.Rotate(0, angularSpeed, 0);
	}
}
