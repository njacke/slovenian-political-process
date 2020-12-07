using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    [SerializeField] int breakableBlocks;   // Serialized for debugging purposes
    [SerializeField] GameObject ballOne;
    [SerializeField] GameObject paddleOne;

    // config params
    Vector3 ballStartPos;

    // cached reference
    SceneLoader sceneloader;

    private void Start()
    {
        sceneloader = FindObjectOfType<SceneLoader>();
        ballStartPos = ballOne.transform.position;
        Cursor.visible = false;                             // disable cursor on level load

    }



    public void CountBlocks()
    {
        breakableBlocks++;
    }

    public void BlockDestroyed()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0)
        {
            sceneloader.LoadNextScene();
            Cursor.visible = true;                          // enable cursor on completed level 
        }
    }

    public void SpawnBall() // check to what can I set a fixed rotation
    {
        {
            GameObject newBall = Instantiate(ballOne,
                new Vector3(paddleOne.transform.position.x, ballStartPos.y, paddleOne.transform.position.z),
                transform.rotation);
        }
    }
}