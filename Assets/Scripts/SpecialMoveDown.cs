using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMoveDown : MonoBehaviour
{

    private float speed = 1.4f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed); // moves attatched component down 
    }
}
