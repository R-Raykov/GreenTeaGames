using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Visibility();

public class Controller : MonoBehaviour
{
    public event Visibility OnInvisible;

    private void Start()
    {
        StartGarbageCollection();
        SpawnObject();
    }

    /// <summary>
    /// Starts the garbage collector
    /// </summary>
    private void StartGarbageCollection()
    {
        StartCoroutine(GarbageCollector());
    }

    protected virtual void SpawnObject() { }

    /// <summary>
    /// Disables the objects that have gone out of sight on a 3 second interval, after which it calls the spawn object method, enabaling them ahead in the level
    /// </summary>
    /// <returns></returns>
    private IEnumerator GarbageCollector()
    {
        yield return new WaitForSeconds(3.0f);

        if(OnInvisible != null)
            OnInvisible.Invoke();
        
        StartCoroutine(GarbageCollector());
    }

    private void OnEnable()
    {
        OnInvisible += SpawnObject;
    }

    private void OnDisable()
    {
        OnInvisible -= SpawnObject;
    }
}
