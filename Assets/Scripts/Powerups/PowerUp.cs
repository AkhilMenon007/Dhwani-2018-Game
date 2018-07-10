using UnityEngine;

public abstract class PowerUp : ScriptableObject
{
    public float powerUpCD;
    public new string name;
    public string description;
    
    public Sprite image;

    public abstract void TriggerPowerUp();

}
