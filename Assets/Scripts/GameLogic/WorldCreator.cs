using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCreator : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Blocks;

    System.Random rnd = new System.Random();
    void Start()
    {
        Create();
    }

    void Create()
    {
        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                var pos = transform.position;
                pos.y += i;
                pos.x += j;

                var blockId = rnd.Next(0, Blocks.Count);
                var block = Instantiate(Blocks[blockId], pos, Quaternion.identity);
                block.transform.SetParent(transform);
            }
        }
    }
}
