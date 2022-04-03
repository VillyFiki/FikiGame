using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Relationships
{
    public static LayerMask GetEnemyLayerByCurrentLayer(LayerMask layer)
    {
        switch (layer)
        {
            case (10): //friendly
                return 11; //return enemy
            case (11): //enemy
                return 10; //return friendly
            default: return 0;
        }
    }
}
