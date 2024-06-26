using UnityEngine;

public class PossesionTrigger : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Possessable") {
            playerController.nearestPossessable = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(!playerController.nearestPossessable && collision.gameObject.tag == "Possessable") {
            playerController.nearestPossessable = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(playerController.nearestPossessable == collision.gameObject) {
            playerController.nearestPossessable = null;
        }
    }
}
