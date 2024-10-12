using System;
using System.Collections.Generic;

public class FisherYatesShufflerAlgorithm
{
    public static void Shuffle<T>(List<T> _list)
    {
        Random rng = new Random();
        for (int i = _list.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            T temp = _list[i];
            _list[i] = _list[j];
            _list[j] = temp;
        }
    }
}
