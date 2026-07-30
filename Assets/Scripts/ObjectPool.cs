using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private int poolSize = 5;

    private List<GameObject> pool = new List<GameObject>();

    void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Create();
        }
    }

    private GameObject Create()
    {
        var rand = Random.Range(0, prefabs.Length);
        GameObject obj = Instantiate(prefabs[rand]);
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }
    public GameObject GetFromPool()
    {
        foreach (var obj1 in pool)
        {
            if (!obj1.activeInHierarchy)
            {
                obj1.SetActive(true);
                return obj1;
            }
        }
        
        return Create();
    }
    public void RemoveFromPool(GameObject obj)
    {
        if (obj == null) return;

        pool.Remove(obj);
        Destroy(obj);
    }
}
