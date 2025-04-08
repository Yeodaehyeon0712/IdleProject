using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SOAnimatorContainer", menuName = "ScriptableObject/SOAnimatorContainer", order = int.MinValue)]
public class SOAnimatorContainer : ScriptableObject
{
    public RuntimeAnimatorController Animator;
}
