using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IProjectile : MonoBehaviour
{
    public virtual int Damage { get; set; } = 1;
}
