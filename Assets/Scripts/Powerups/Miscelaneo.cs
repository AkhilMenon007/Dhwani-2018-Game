using UnityEngine;

[CreateAssetMenu(menuName = "BohemianRunaway/Miscelaneo")]

public class Miscelaneo : PowerUp {

    [SerializeField]
    PowerUp[] powerUps;

    public override void TriggerPowerUp()
    {
        int index = Random.Range(0, powerUps.Length);
        powerUps[index].TriggerPowerUp();
        Debug.Log(powerUps[index].name);
    }
}
