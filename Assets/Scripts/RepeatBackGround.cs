using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackGround : MonoBehaviour
{

    private Vector3 startPos;
    private float repeatHeight;
    // Start is called before the first frame update
    void Start()
    {
        //startPos = transform.position; // Get initial position
        
        //Vector3 colliderSize = GetComponent<BoxCollider>().size;
        //Debug.Log("Box Collider Size: " + colliderSize); // Log the collider size
        //repeatHeight = colliderSize.y / 2; // Now divide by 2
        //Debug.Log("Repeat Height: " + repeatHeight); // Log the repeat height
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > 39.86) {
            transform.position = new Vector3(-0.94f, 0.371f, -39.08f);
        }
    }
}
