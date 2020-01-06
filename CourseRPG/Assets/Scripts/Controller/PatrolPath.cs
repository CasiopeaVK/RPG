using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Controll
{
    public class PatrolPath : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                Transform wayPoint = transform.GetChild(i);
                Transform nextWayPoint = transform.GetChild((i + 1) % transform.childCount);
                
                Gizmos.DrawLine(wayPoint.position, nextWayPoint.position);
                Gizmos.DrawSphere(wayPoint.position, 0.1f);
            }
        }
    }
}
