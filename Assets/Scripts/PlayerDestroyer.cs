using UnityEngine;

public class PlayerDestroyer : MonoBehaviour {

    PlayerController player;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        player = other.GetComponent<PlayerController>();
        if (player != null)
        {

            Handheld.Vibrate();

            if (!player.isKillable)
                return;
            Debug.Log("Hit");
            other.GetComponent<PlayerMover>().enabled = false;
            player.DestroyPlayer();
            player = null;
        }

    }

}
