using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using Random = System.Random;
using Vector3 = UnityEngine.Vector3;

public class NecroOrb : IProjectile
{
    public override int Damage { get; set; } = 3;
}
