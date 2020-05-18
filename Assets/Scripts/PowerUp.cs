using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;

    [SerializeField]
    private int _powerUpID; //0 = Triple up, 1 = Speed boost, 2 = Shield.

    private Player _playerObject;
    
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -6.3f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D Bob)
    {
        if (Bob.tag == "Player")
        {
            Destroy(this.gameObject);

            //Inform Player script to activate Triple shot.
            _playerObject = GameObject.Find("Player").GetComponent<Player>();

            if(_playerObject != null)
            {
                switch(_powerUpID)
                {
                    case 0:
                        _playerObject.TripleShotActive();
                        Debug.Log("Triple shot power up obtained");
                        break;
                    case 1:
                        _playerObject.playerSpeedUp();
                        Debug.Log("Speed power up obtained");
                        break;
                    case 2:
                        _playerObject.shieldPowerUp();
                        Debug.Log("Shield obtained");
                        break;
                    default:
                        Debug.Log("Not valid");
                        break;
                }
                
            }
            
        }
    }
}
