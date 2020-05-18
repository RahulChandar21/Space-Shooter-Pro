using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    private int _speedMultiplier = 2;
    [SerializeField]
    private Transform _laserInstance;
    [SerializeField]
    private float _fireRate = 0.5f;

    private float _nextFire = 0.0f;

    [SerializeField]
    private int _playerLife = 3;

    private SpawnManager _spawnObject;

    [SerializeField]
    private GameObject _laserTripleShot;
    private bool _isTripleShotActive = false;
        
    private bool _isSpeedUpActive = false;

    private bool _isShieldActive = false;

    [SerializeField]
    private GameObject _shieldVisualizer;

    private int _score = 0;
    private UIManager _uiManager;

    [SerializeField]
    private GameObject _hurtRight;
    [SerializeField]
    private GameObject _hurtLeft;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        //Call spawn manager and stop enemy spawning upon player death.
        _spawnObject = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if(_spawnObject == null)
        {
            Debug.LogError("Enemy component not found");
        }

        if(_uiManager == null)
        {
            Debug.LogError("UI manager not found");
        }

        _hurtRight.SetActive(false);
        _hurtLeft.SetActive(false);
    }

    
    void Update()
    {
        calculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && _isTripleShotActive == true)
        {
            Instantiate(_laserTripleShot, transform.position + new Vector3(-0.45f, 0.62f, 0), Quaternion.identity);

        }
        
        //Time.time indicates the time for which the code has been running.
        else if (Input.GetKeyDown(KeyCode.Space) && (Time.time > _nextFire))
        {
            firing();
        }
  
    }

    void calculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        /*
        transform.Translate(new Vector3(1,0,0) * horizontalInput* _speed * Time.deltaTime);
        transform.Translate(new Vector3(0, 1, 0) * verticalInput * _speed * Time.deltaTime);
        */
                
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);
                
        //To clamp in vertical direction, could also use: Mathf.Clamp(transform.position.y, lower extreme, higher extreme)
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.0f, 6.0f), 0);

        if (transform.position.x > 10.0f)
        {
            transform.position = new Vector3(-10.0f, transform.position.y, 0);
        }
        else if (transform.position.x < -10.0f)
        {
            transform.position = new Vector3(10.0f, transform.position.y, 0);
        }
    }

    void firing()
    {
        _nextFire = Time.time + _fireRate;
        //Ensures that player does not fire until some time is passed after 1st firing.


        //To instantiate triple laser shot
        Instantiate(_laserInstance, transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
        //Quaternion.Identity means 0 rotation.
        //Offset position of firing by 0.2 from center of cube.

    }

    public void playerDestroy()
    {
        if(_isShieldActive)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _playerLife--;
        _uiManager.livesUpdate(_playerLife);

        if (_playerLife == 2)
        {
            _hurtRight.SetActive(true);
        }
        else if (_playerLife == 1)
        {
            _hurtLeft.SetActive(true);
        }

        if(_playerLife < 1)
        {
            Destroy(this.gameObject);

            //If player life is 0, then enemy spawn is stopped.
            _spawnObject.playerDead();
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;

        StartCoroutine(TripleShotDeactivate());
    }

    //Coroutine to stop triple shot after 5 seconds
    public IEnumerator TripleShotDeactivate()
    {
        while(_isTripleShotActive == true)
        {
            yield return new WaitForSeconds(5);
            _isTripleShotActive = false;
        }

    }

    public void playerSpeedUp()
    {
        _isSpeedUpActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(playerSpeedUpDeactivate());
    }

    IEnumerator playerSpeedUpDeactivate()
    {
        yield return new WaitForSeconds(5);
        _speed /= _speedMultiplier;
        _isSpeedUpActive = false;
    }

    public void shieldPowerUp()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void playerScore(int points)
    {
        _score += points;
        _uiManager.scoreUpdate(_score);
    }
}
