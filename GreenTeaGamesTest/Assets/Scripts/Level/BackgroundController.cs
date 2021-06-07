using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : Controller
{
    private float _nextBackgroundSpawnPosition = 0;

    protected override void SpawnObject()
    {
        // Gets an available object from the pool
        GameObject backGroundPannel = ObjectPooler.Instance.GetPooledObject("Background");
        if (backGroundPannel != null)
        {
            // Copy the spawn position, and only change the value on the X axis
            Vector3 spawnPosition = backGroundPannel.transform.position;
            spawnPosition.x = _nextBackgroundSpawnPosition;
            backGroundPannel.transform.position = spawnPosition;

            // Set the controller of the object
            backGroundPannel.GetComponent<LevelObject>().SetController(this);
            backGroundPannel.SetActive(true);

            // Calculate the next X position by taking the bounds of the renderer
            _nextBackgroundSpawnPosition = backGroundPannel.GetComponent<MeshRenderer>().bounds.max.x +
                                            backGroundPannel.GetComponent<MeshRenderer>().bounds.extents.x;

            // Recursivly call this function again, this preloads the level with all availavble objects in the pool
            SpawnObject();
        }
    }
}
