using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    private Controller _controller;
    // Start is called before the first frame update

    private void OnDisable()
    {
        if(_controller != null)
            _controller.OnInvisible -= DisableThisObject;

    }

    /// <summary>
    /// Gets called by unity when the object goes out of sight of all the cameras. 
    /// Note: if you are observing the scene in the editor this does not get called as the scene camera also counts as an observer
    /// </summary>
    private void OnBecameInvisible()
    {
        if (gameObject == null || GameManager.Instance == null || GameManager.Instance.Player == null)
            return;


        if (transform.position.x < GameManager.Instance.Player.transform.position.x)
            _controller.OnInvisible += DisableThisObject;
        
    }

    /// <summary>
    /// Sets the controller of this object, we use this to subscirbe to the OnInvisible Event
    /// </summary>
    /// <param name="pController"></param>
    public void SetController(Controller pController)
    {
        _controller = pController;
    }

    /// <summary>
    /// Disables the gameobject
    /// </summary>
    private void DisableThisObject()
    {
        // If the gameobject is a child, such is the case with the pipes, then it disables the parent

        if (transform.parent == null)
            gameObject.SetActive(false);
        else
            transform.parent.gameObject.SetActive(false);        
    }
}
