using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipment
{
    public void Equip();

    public void Unequip();

    public bool IsEquipped { get; set; }
    public IEffect Effect { get; set; }
}

public abstract class Equipment : MonoBehaviour, IEquipment
{
    public virtual void Equip()
    {
        IsEquipped = true;
    }

    public virtual void Unequip()
    {
        IsEquipped = false;
    }

    public abstract bool IsEquipped { get; set; }
    public IEffect Effect { get; set; }
}
