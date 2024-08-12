using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_AnimParameter 
{
    const string IDLE_PARAM = "idleDone";
    const string EAT_PARAM = "eatDone";

    public static readonly int IDLE_DONE = Animator.StringToHash(IDLE_PARAM);
    public static readonly int EAT_DONE = Animator.StringToHash(EAT_PARAM);
}
