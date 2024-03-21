using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public GameObject player;
    public GameObject currentplayer;
    private int scoreToCreateCheckpoint = 10;
    public GameObject checkpointPrefab;
    private GameObject currentCheckpoint;

    public int currentScore = 0;

    private void Update()
    {
        InputManager();
    }

    private void Start()
    {
        SpawnerPlayerStart();
    }

    public void InputManager()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentScore >= scoreToCreateCheckpoint)
        {
            CreateCheckpoint();

            currentScore = 0;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnerPlayerToCheckPoint();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            AddScore(1);
        }
    }

    public void SpawnerPlayerStart()
    {
        var Player = Instantiate(player, gameObject.transform.position, Quaternion.identity);

        currentplayer = Player;
    }


    public void SpawnerPlayerToCheckPoint()
    {
        if(currentplayer == null)
        {
            var Player = Instantiate(player, currentCheckpoint.transform.position, Quaternion.identity);

            currentplayer = Player;

            if (currentCheckpoint != null)
            {
                Destroy(currentCheckpoint);
            }
        }
    }

    private void CreateCheckpoint()
    {
        if (currentCheckpoint != null)
        {
            Destroy(currentCheckpoint);
        }

        currentCheckpoint = Instantiate(checkpointPrefab, currentplayer.transform.position, Quaternion.identity);
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
    }
}
