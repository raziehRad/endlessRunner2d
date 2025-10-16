using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 5;

    private List<GameObject> pool = new List<GameObject>();

    void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
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

        // در صورت نیاز می‌تونی بسازی، ولی بهتره اندازه‌ی Pool ثابت باشه
        return null;
    }
}
