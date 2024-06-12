using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "Character", menuName = "Character/Create new combat character")]
    public class CharBase : ScriptableObject
    {
        [SerializeField] new string name;
        
        [TextArea]
        [SerializeField] string description;
    
        [SerializeField] Sprite frontSprite;
        [SerializeField] Sprite backSprite;
    
        [SerializeField] CharClassType classType;
        
        //Base stats
        [SerializeField] int maxHp;
        [SerializeField] int attach;
        [SerializeField] int defense;
        [SerializeField] int spAttack;
        [SerializeField] int spDefense;
        [SerializeField] int speed;
    
        [SerializeField] List<LearnableMove> learnableMoves;
    
        public string Name => name;
        public string Description => description;
        public Sprite FrontSprite => frontSprite;
        public Sprite BackSprite => backSprite;
        public CharClassType CharClassType => classType;
        public int MaxHp => maxHp;
        public int Attack => attach;
        public int Defense => defense;
        public int SpAttack => spAttack;
        public int SpDefense => spDefense;
        public int Speed => speed;
        public List<LearnableMove> LearnableMoves => learnableMoves;
    }
    
    [System.Serializable]
    public class LearnableMove
    {
        [SerializeField] MoveBase moveBase;
        [SerializeField] int level;
    
        public MoveBase Base => moveBase;
        public int Level => level;
    }
    
    public enum CharClassType
    {
        Warrior,
        Mage,
        Animal,
    }

