﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform targetToMove;

        void LateUpdate()
        {
            transform.position = targetToMove.position;
        }
    }
}
