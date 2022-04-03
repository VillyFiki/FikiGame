using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List<GameObject> Items;
    

    void Update()
    {

    }

    public void EquipAll()
    {
        foreach (var item in Items)
        {
            var equipment = item.GetComponent<IEquipment>();
            Debug.Log(equipment.IsEquipped);
            if (equipment.IsEquipped) equipment.Unequip();
            else equipment.Equip();
        }
        
    }
}
