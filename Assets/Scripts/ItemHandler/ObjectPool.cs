using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class ObjectPool : MonoBehaviour
{
    [SerializeField] private AssetReferenceGameObject[] prefabs;
    [SerializeField] private int poolSize = 5;

    private List<GameObject> pool = new List<GameObject>();
    private Task poolReadyTask;
    private async void Awake()
    {
        poolReadyTask = InitializePool();
    }

    private async Task InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            await Create();
        }
    }
    public Task WaitUntilReady()
    {
        return poolReadyTask;
    }
    // Instantiate an Addressable object and add it to the pool
    private async Task Create()
    {
        int rand = Random.Range(0, prefabs.Length);

        GameObject obj = await prefabs[rand].InstantiateAsync().Task;

        obj.SetActive(false);
        pool.Add(obj);
    }
    // Get an inactive object from the pool
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

        ExpandPool();
        Debug.LogWarning($"Pool {gameObject.name} is empty!");
        return null;
    }
    private async void ExpandPool()
    {
        await Create();
    }
    public void ReturnToPool(GameObject obj)
    {
        if (obj == null) return;

        obj.SetActive(false);
    }
}
