using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Character/Create new move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] new string name;
    
    [TextArea]
    [SerializeField] string description;

    [SerializeField] CharClassType classType;
    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] int pp;

    public string Name => name;
    public string Description => description;
    public CharClassType ClassType => classType;
    public int Power => power;
    public int Accuracy => accuracy;
    public int PP => pp;
}
