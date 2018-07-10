using UnityEngine;

[CreateAssetMenu(menuName = "BohemianRunaway/Khelotsav")]

public class Khelotsav : PowerUp
{
    [SerializeField]
    float noDamageDuration = 10f;
    
    public override void TriggerPowerUp()
    {
        GameController.gameController.Player.GetComponent<PlayerController>().TriggerNoDamage(noDamageDuration);
    }

}
