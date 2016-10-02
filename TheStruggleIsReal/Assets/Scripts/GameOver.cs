using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public Button gameoverButton;
    public BoardManager boardManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            boardManager.GetComponent<BoardManager>().backgroundImage.gameObject.SetActive(true);
            gameoverButton.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }



}
