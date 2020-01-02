﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Scheduler : MonoBehaviour
    {   
        IAction currentAction;
        public void StartAction(IAction action)
        {
            if(currentAction == action)return;
            if(currentAction != null)
            {
                currentAction.Cancel();
                print("Cancel current action: " + currentAction);
            }
            
            currentAction = action;
        }
    }
}
