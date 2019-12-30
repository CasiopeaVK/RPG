using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform targetToMove;

    void Update()
    {
        transform.position = targetToMove.position;
    }
}
