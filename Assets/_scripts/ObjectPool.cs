using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using Controllers;

public interface IObjectPoolHolder {
    void AddPool(IObjectPool pool);
}

public interface IObjectPool {
    void CreatePool(int size);
    GameObject GetObject(bool random = false);
    GameObject GetObject(int index);
    GameObject GetObject(string searchName);
    void ReturnObject(GameObject obj);
    void Reset();
}

// This class contains methods for creating an object pool
// we can then re-use objects from the pool instead of instantiating new clones
// Multiple prefabs can be used, be aware that if the pool size is not divisible
// by the number of different prefabs then some prefabs will have one less clone
public class ObjectPool : BaseMonoBehaviour, IObjectPool {

    public enum Type { Card }

    #region Settings

    // The GameObjects we want to make a pool of
    [SerializeField] GameObject[] prefabs;
    [SerializeField] bool createOnAwake;
    [SerializeField] int defaultSize;
    [SerializeField] Type type;
    [SerializeField] Game game;
    private IObjectPoolHolder holder { get { return game; } }

    // Position that objects are kept at while in the pool
    [SerializeField] Vector3 homePosition;

    #endregion Settings

    #region Variables

    List<GameObject> inPool;
    List<GameObject> outOfPool;
    public int objectsRemaining { get { return inPool.Count; } }
    public bool isEmpty { get { return inPool.Count <= 0; } }

    #endregion Variables

    #region Creation

    protected override void Awake() {
        base.Awake();
        if (holder != null)
        {
            holder.AddPool(this);
        }

        if (createOnAwake)
            CreatePool(defaultSize);
    }

    // Make a new pool. Objects are set as inactive children of the pool. 
	public void CreatePool(int size){
        if (inPool == null)
        {
            inPool = new List<GameObject>();
            outOfPool = new List<GameObject>();

            for (int i = 0; i < size; i++)
            {
                // Create a new index based on i to make sure we don't exceed the prefabs array length
                var j = (int)Mathf.Repeat(i, prefabs.Length);

                var obj = Instantiate(prefabs[j], homePosition, Quaternion.identity, transform) as GameObject;
                obj.SetActive(false);
                inPool.Add(obj);
            }
        }
        else
        {
            // We don't want to call this on an existing pool as we would lose
            // track of the old objects
            Debug.Log("Pool already exists, preventing overwrite");
        }
    }

    #endregion Creation

    #region Getting and returning objects

    public GameObject GetObject(bool random = false)
    {
        // By default we take the first available object from the pool
        var index = random ? UnityEngine.Random.Range(0, objectsRemaining) : 0;
        return GetObject(index);
    }

    public GameObject GetObject(int index)
    {
        GameObject obj = null;
        try
        {
            obj = inPool[index];
            TakeObjectFromPool(obj);
        }
        catch(Exception e)
        {
            if (inPool == null)
                Debug.Log(e + ": Pool doesn't exist");
            else if (objectsRemaining < 1)
                Debug.Log(e + ": Pool is empty, all objects in use");
            else if (index < 0 || index >= objectsRemaining)
                Debug.Log(e + ": Index outside of valid range");
        }
        return obj;
    }

    public GameObject GetObject(string searchName)
    {
        var obj = inPool.FirstOrDefault(go => go.name == searchName);
        if (obj)
            TakeObjectFromPool(obj);
        return obj;
    }

    private void TakeObjectFromPool(GameObject obj)
    {
        // Move the object from the pool list to the in use list and make it active
        inPool.Remove(obj);
        outOfPool.Add(obj);
        obj.SetActive(true);
    }

    // Sends an object back to the pool and de-activates it
	public void ReturnObject(GameObject obj)
    {
		inPool.Add(obj);
        outOfPool.Remove(obj);
        obj.transform.SetParent(transform);
        obj.transform.position = homePosition;
        obj.transform.localScale = Vector3.one;
        obj.SetActive(false);
	}

    // Bring ALL objects back to the pool
    public void Reset()
    {
        while(outOfPool.Count > 0)
            ReturnObject(outOfPool[0]);
    }

    #endregion Getting and returning objects

#if UNITY_EDITOR
    public void Clear()
    {
        inPool = null;
        outOfPool = null;
    }
#endif
}
