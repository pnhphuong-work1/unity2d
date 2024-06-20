using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public CharBase Base { get; set; }
    public int Level { get; set; }
    
    public int HP { get; set; }
    public List<Move> Moves { get; set; }

    public Character(CharBase charBase, int pLevel)
    {
        Base = charBase;
        Level = pLevel;
        HP = PlayerPrefs.GetInt("CurrentHP", MaxHp);
        //Generated move
        Moves = new List<Move>();
        foreach (var move in Base.LearnableMoves)
        {
            if (move.Level <= Level)
                Moves.Add(new Move(move.Base));
            
            if (Moves.Count >= 4) break;
        }
    }

    public int Attack 
        => Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5;
    
    public int MaxHp => 
        Mathf.FloorToInt((Base.MaxHp * Level) / 100f) + 10;
    
    public int Defense => 
        Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5;
    
    public int SpAttack => 
        Mathf.FloorToInt((Base.SpAttack * Level) / 100f) + 5;
    
    public int SpDefense => 
        Mathf.FloorToInt((Base.SpDefense * Level) / 100f) + 5;
    
    public int Speed => 
        Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5;

    public bool TakeDame(Move move, Character attacker)
    {
        float modifiers = Random.Range(0.85f, 1f);
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * move.Base.Power * ((float)attacker.Attack / Defense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            return true;
        }

        return false;
    }

    public Move GetRandomMove()
    {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }
}

