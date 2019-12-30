using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform targetToMove;

    void LateUpdate()
    {
        transform.position = targetToMove.position;
    }
}
