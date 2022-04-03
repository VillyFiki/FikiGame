using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public static class RandomExtension
{
    private static System.Random rnd;
    public static IList<int> ShuffleRange(int min, int max)
    {
        rnd = new System.Random();

        int[] perm = Enumerable.Range(min, max+1).ToArray();
        
        int n = perm.Length;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            var value = perm[k];
            perm[k] = perm[n];
            perm[n] = value;
        }

        foreach (var i in perm)
        {
        }
        return perm;
    }
}
