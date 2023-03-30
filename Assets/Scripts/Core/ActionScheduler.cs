using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /* "If I'm working on a new IAction, then I need to cancel whatever IAction may be going on". */

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        public void StartAction(IAction action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                currentAction.Cancel();
            }
            currentAction = action;
        }
        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}
