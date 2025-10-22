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
            var rand = Random.Range(0, prefabs.Length);
            GameObject obj = Instantiate(prefabs[rand]);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetFromPool()
    {
        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        
        return null;
    }
}
