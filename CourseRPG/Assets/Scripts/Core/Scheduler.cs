using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Scheduler : MonoBehaviour
    {   
        MonoBehaviour currentAction;
        public void StartAction(MonoBehaviour action)
        {
            if(currentAction == action)return;
            if(currentAction != null)
            {
                print("Cancelling cur action" + currentAction);
            }
            
            currentAction = action;
        }
    }
}
