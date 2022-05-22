using TMPro;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Game _game;

    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private TextMeshProUGUI _scoreInGameText, _scoreInGameOverText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cube cube))
        {
            if (cube.IsPlayed)
            {
                _game.GameOver();
                _gameOverPanel.SetActive(true);
                _scoreInGameOverText.text = _scoreInGameText.text;
                _scoreInGameText.gameObject.SetActive(false);
                _gamePanel.SetActive(false);
            }
            else
            {
                cube.IsPlayed = true;
            }
        }
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
