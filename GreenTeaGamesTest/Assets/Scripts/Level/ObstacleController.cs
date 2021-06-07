using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : Controller
{
    private float _minHeight = 0.0f;
    private float _maxHeight = 5.0f;

    private float _newXPos = 0.0f;

    protected override void SpawnObject()
    {
        // Gets an available object from the pool
        GameObject obstacle = ObjectPooler.Instance.GetPooledObject("Obstacle");
        if (obstacle != null)
        {
            // Copy the spawn position, here we change both the X value and the Y using a random range

            Vector3 spawnPosition = obstacle.transform.position;

            _newXPos += Random.Range(3.0f,9.0f);

            spawnPosition.x += _newXPos;
            spawnPosition.y = Random.Range(_minHeight, _maxHeight);

            obstacle.transform.position = spawnPosition;

            // Set the controller of the object
            obstacle.GetComponentInChildren<LevelObject>().SetController(this);
            obstacle.SetActive(true);

            // Recursivly call this function again, this preloads the level with all availavble objects in the pool
            SpawnObject();
        }
    }
}
