using UnityEngine;

[CreateAssetMenu(menuName ="BohemianRunaway/Antara")]

public class Antara : PowerUp
{
    public override void TriggerPowerUp()
    {
        GameController.gameController.Player.GetComponent<PlayerController>().health = 100;
        GameController.gameController.Player.GetComponent<PlayerController>().SetColor();
    }


}
