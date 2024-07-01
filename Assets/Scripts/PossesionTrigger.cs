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
            playerController.maxDist = new Vector2( collision.bounds.size.x / 2, collision.bounds.size.y / 2).magnitude;
            if(playerController.maxDist <= 1.0f) {
                playerController.maxDist = 1.3f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        ProcessHit(collision);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        ProcessHit(collision);
    }
}
