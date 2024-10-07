using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatfolkAxe : AEnemy
{
    protected override void Awake()
    {
        tag = ETag.RatfolkAxe.ToString();
        base.Awake();
    }
}
