using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    // cached reference
    GameSession gamesession;
    Level level;

    private void Start()
    {
        gamesession = FindObjectOfType<GameSession>();
        level = FindObjectOfType<Level>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gamesession.TakeLife();

        if (gamesession.GameOverCheck())                // check if more deaths than
        {
            SceneManager.LoadScene("Game Over");        // move to GameSession?
            Cursor.visible = true;                      // enable cursor on game over screen
        }
        else
        {
           level.SpawnBall();
        }
    }
}
