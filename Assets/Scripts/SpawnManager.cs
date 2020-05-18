using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//To control position on Enemy and Power up.
//Also to stop spawning when needed
public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnEnemy;

    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false; //To control enemy object spawning.

    [SerializeField]
    private GameObject[] _PowerUps;

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        //Could use StartCoroutine("SpawnRoutine") also. Each has +ves and -ves.

        StartCoroutine(SpawnPowerUpRoutine());
    }

    public void playerDead()
    {
        _stopSpawning = true;
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while(_stopSpawning == false)
        {
            Vector3 spawnPositionEnemy = new Vector3(Random.Range(-9.5f, 9.5f), 7, 0);

            //New game object created to hold the instantiated enemy
            GameObject enemyObject = Instantiate(_spawnEnemy, spawnPositionEnemy, Quaternion.identity);

            //Assigning empty game object EnemyContainer to be parent for all the enemy objects instantiated.
            enemyObject.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(5);
            
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while(_stopSpawning == false)
        {
            Vector3 spawnPostionPowerUp = new Vector3(Random.Range(-9.5f, 9.5f), 7, 0);

            int randomPowerUp = Random.Range(0, _PowerUps.Length);
            Instantiate(_PowerUps[randomPowerUp], spawnPostionPowerUp, Quaternion.identity);
            
            yield return new WaitForSeconds(10);
        }

    }
}
