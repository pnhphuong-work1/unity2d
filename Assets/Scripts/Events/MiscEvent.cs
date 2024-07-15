using System;

public class MiscEvents
{
    public event Action onCoinCollected;
    public void CoinCollected() 
    {
        if (onCoinCollected != null) 
        {
            onCoinCollected();
        }
    }

    public event Action onGemCollected;
    public void GemCollected() 
    {
        if (onGemCollected != null) 
        {
            onGemCollected();
        }
    }
    
    public event Action<string> onEnemyDefeated;
    public void EnemyDefeated(string enemyName)
    {
        if (onEnemyDefeated != null) 
        {
            onEnemyDefeated(enemyName);
        }
    }
    
}