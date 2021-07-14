using UnityEngine;

public class ServicePool : PoolingService<GameObject>
{
    [SerializeField]
    private GameObject Chest;
  

    public GameObject GetObject()
    {
        return GetItem();
    }

    protected override GameObject CreateItem()
    {
        GameObject pooledItem = Instantiate(Chest,gameObject.transform);
        return pooledItem;
    }

  
}
