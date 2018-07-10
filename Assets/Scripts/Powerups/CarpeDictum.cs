using UnityEngine;

[CreateAssetMenu(menuName = "BohemianRunaway/CarpeDictum")]

public class CarpeDictum : PowerUp
{
    [SerializeField]
    float freezeDuration = 10f;

    public override void TriggerPowerUp()
    {
        GameController.gameController.FreezeEnemies(freezeDuration);
    }

}
