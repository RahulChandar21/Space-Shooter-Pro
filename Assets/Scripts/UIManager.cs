using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Text _restartText;

    [SerializeField]
    private Sprite[] _livesSprite;

    [SerializeField]
    private Image _livesImage;

    [SerializeField]
    private GameManage _gameManager;

    
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;

        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManage>();

        if(_gameManager == null)
        {
            Debug.LogError("Game manager not found.");
        }
    }

    
    public void scoreUpdate(int updatedScore)
    {
        _scoreText.text = "Score: " + updatedScore.ToString();
    }

    public void livesUpdate(int bob)
    {
        _livesImage.sprite = _livesSprite[bob];

        if(bob <= 0)
        {
            _gameOverText.gameObject.SetActive(true);
            _restartText.gameObject.SetActive(true);

            StartCoroutine(flickerGameOverText());

            _gameManager.GameOver();
        }
        
    }

    IEnumerator flickerGameOverText()
    {

        while(true)
        {
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);

        }
            
    }
}
