using UnityEngine;

public class PossesionTrigger : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    void ProcessHit(Collider2D collision) {
        if (playerController.isPossessing) { return; }
        if (playerController.nearestPossessable) { return; }

        if (collision.gameObject.tag == "Possessable") {
            playerController.nearestPossessable = collision.gameObject;
            playerController.nearestPossessable.GetComponent<Possessable>().Pulse();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        ProcessHit(collision);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        ProcessHit(collision);
    }
}
