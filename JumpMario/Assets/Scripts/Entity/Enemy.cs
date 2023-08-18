using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runningboy.Entity
{
    public class Enemy : Entity
    {
        private void Awake()
        {
            tag = "Enemy";
        }
    }
}