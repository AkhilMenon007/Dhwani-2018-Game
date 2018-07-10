using UnityEngine;

public class PowerUpSelection : MonoBehaviour {

    public void StartGame()
    {
        GameController.gameController.Player.GetComponent<Animator>().SetTrigger("Start");
    }

    public void Refresh()
    {
        GameController.gameController.RefreshPowerUpDesc();
    }

}
