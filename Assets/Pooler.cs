using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pooler
{
    private static Dictionary<string,Pool> pools = new Dictionary<string, Pool>();

    public static void Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject obj;
        var key = prefab.name.Replace("(Clone)", "");

        if (pools.ContainsKey(key))
        {
            if (pools[key].inactive.Count == 0)
            {
                Object.Instantiate(prefab, position, rotation,pools[key].parent.transform);
            }
            else
            {
                obj = pools[key].inactive.Pop();
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
            }
        }
        else
        {
            GameObject newParent = new GameObject($"{key}_POOL");
            Object.Instantiate(prefab, position, rotation, newParent.transform);
            Pool newPool = new Pool(newParent);
            pools.Add(key,newPool);
        }
    }
    
    public static void Despawn(GameObject prefab)
    {
        var key = prefab.name.Replace("(Clone)", "");
        if (pools.ContainsKey(key))
        {
            pools[key].inactive.Push(prefab);
            prefab.transform.position = pools[key].parent.transform.position;
            prefab.SetActive(false);
        }
        else
        {
            GameObject newParent = new GameObject($"{key}_POOL");
            Pool newPool = new Pool(newParent);
            
            prefab.transform.SetParent(newParent.transform);
            
            pools.Add(key,newPool);
            pools[key].inactive.Push(prefab);
            prefab.SetActive(false);
        }
    }
}
