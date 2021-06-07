using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 newPos = transform.position;
        newPos.x = _player.position.x;
        //newPos.z = _player.position.z;

        transform.position = newPos;
    }
}
