using UnityEngine;

[CreateAssetMenu(menuName ="BohemianRunaway/Nadanta")]

public class Nadanta : PowerUp
{
    [SerializeField]
    float invulnerabilityDuration = 5f;

    public override void TriggerPowerUp()
    {
        GameController.gameController.Player.GetComponent<PlayerController>().TriggerInvulnerable(invulnerabilityDuration);
    }
}
