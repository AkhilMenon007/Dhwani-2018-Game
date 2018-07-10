using UnityEngine;

[CreateAssetMenu(menuName = "BohemianRunaway/Dionysia")]

public class Dionysia : PowerUp
{
    [SerializeField]
    float range = 200f;

    public override void TriggerPowerUp()
    {
        GameController.gameController.DestroyEnemyInRange(range);
    }
}
