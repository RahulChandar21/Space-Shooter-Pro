using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //Speed variable
    private float _speed = 8.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Laser to move north at specified speed upon instantiating.
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //Destroy laser clone, once it crosses the screen height
        if (transform.position.y >= 8.0f)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
            
    }
}
