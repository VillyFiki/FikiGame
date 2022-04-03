using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromanticRing : Equipment
{
    public AuraProjectiles Aura;

    private GameObject auraGameObject;

    public override bool IsEquipped { get; set; }

    public override void Equip()
    {
        var player = GameObject.FindWithTag("Player").GetComponent<Player>();

        auraGameObject = Instantiate(Aura, player.Effects).gameObject;

        IsEquipped = true;
    }

    public override void Unequip()
    {
        IsEquipped = false;
        Destroy(auraGameObject);
    }
}
