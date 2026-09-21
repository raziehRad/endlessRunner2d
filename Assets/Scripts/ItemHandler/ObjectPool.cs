using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private AssetReferenceGameObject[] prefabs;
    [SerializeField] private int poolSize = 2;

    private readonly List<GameObject> pool = new();
    private readonly Dictionary<GameObject, int> prefabIndexes = new();

    private Task poolReadyTask;
    public int PrefabCount => prefabs.Length;

    private void Awake()
    {
        poolReadyTask = InitializePool();
    }

    private async Task InitializePool()
    {
        // Create a separate pool for each prefab type.
        for (int prefabIndex = 0; prefabIndex < prefabs.Length; prefabIndex++)
        {
            for (int i = 0; i < poolSize; i++)
            {
                await Create(prefabIndex);
            }
        }
    }

    public Task WaitUntilReady()
    {
        return poolReadyTask;
    }

    private async Task Create(int prefabIndex)
    {
        GameObject obj = await prefabs[prefabIndex].InstantiateAsync().Task;

        obj.SetActive(false);

        pool.Add(obj);
        prefabIndexes[obj] = prefabIndex;
    }

    public GameObject GetFromPool(int prefabIndex)
    {
        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy &&
                prefabIndexes[obj] == prefabIndex)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        Debug.LogWarning(
            $"No inactive object available for prefab index {prefabIndex}."
        );

        return null;
    }

    public void ReturnToPool(GameObject obj)
    {
        if (obj == null)
            return;

        obj.SetActive(false);
    }
}