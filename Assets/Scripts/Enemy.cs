using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{    
    private float _enemySpeed = 4.0f;

    private Player variable;

    Animator _destroyAnim;

    AudioSource _explosionSource;

    // Start is called before the first frame update
    void Start()
    {
        variable = GameObject.Find("Player").GetComponent<Player>();

        //Null check ensures function is available.
        if (variable == null)
        {
            Debug.LogError("Player score not available");
        }

        _destroyAnim = GetComponent<Animator>();

        if(_destroyAnim == null)
        {
            Debug.LogError("Enemy animator component not available");
        }

        _explosionSource = GetComponent<AudioSource>();

        if(_explosionSource == null)
        {
            Debug.LogError("Explosing sound clip not found.");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y <= -5.5f)
        {
            transform.position = new Vector3(Random.Range(-9.5f, 9.5f), 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)  //Default functions within Monobehaviour script
    {
        
        //Assigning Player script component to a reference variable to avoid errors, when Player is not available at all.

        if (collider.transform.tag == "Laser")
        {
            //Award score to player.
            int randomPoints = Random.Range(5,11);
            variable.playerScore(randomPoints);
            
            //Destroy laser.
            Destroy(collider.gameObject);

            //Destroy Enemy
            _destroyAnim.SetTrigger("OnEnemyDestroy");
            _enemySpeed = 0; //To destroy enemy at instantaneous moment.

            _explosionSource.Play(); //Audio.

            Destroy(this.gameObject, 2.5f); //Delay time to let the animation play.
        }

        else if (collider.transform.tag == "Player")
        {
            _destroyAnim.SetTrigger("OnEnemyDestroy");
            _enemySpeed = 0;

            _explosionSource.Play(); //Audio.

            Destroy(this.gameObject, 2.5f);

            variable.playerDestroy();
            
        }
    }
}
