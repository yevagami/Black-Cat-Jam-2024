using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Rigidbody2D playerRbComponent;
    [SerializeField] float accelerationSpeed;
    [SerializeField] float declerationSpeed;
    [SerializeField] float maxSpeed;
    public int direction = 1;
    Vector2 moveInput = Vector2.zero;

    [Header("Animation")]
    [SerializeField] SpriteRenderer SRComponent;
    [SerializeField] GameObject trail;

    [Header("Possesion")]
    public GameObject nearestPossessable;
    [SerializeField] bool isPossessing = false;
    [SerializeField] GameObject possessedObject;
    Rigidbody2D objectRbComponent;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Move Input
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(Mathf.Abs(moveInput.x) > 0) {
            direction = (int) moveInput.x;
        }

        //Player movement input
        playerRbComponent.AddForce(moveInput * accelerationSpeed);

        if(moveInput.magnitude <= 0) {
            playerRbComponent.AddForce((playerRbComponent.velocity.normalized * -1) * declerationSpeed);

            if (playerRbComponent.velocity.magnitude < 0.01f) {
                playerRbComponent.velocity = Vector2.zero;
            }
        }

        if(playerRbComponent.velocity.magnitude > maxSpeed) {
            playerRbComponent.velocity = playerRbComponent.velocity.normalized * maxSpeed;
            
        }

        gameObject.transform.localScale = new Vector3(0.1f * direction, 0.1f, 1.0f);

        //Possession
        if(nearestPossessable != null && !isPossessing) {
            Debug.DrawRay(transform.position, nearestPossessable.transform.position - transform.position, Color.red);
        }

        if(Input.GetKeyDown(KeyCode.J)) {
            if (isPossessing) {
                UnPossess();
            } else {

               if(nearestPossessable) {
                    PossessNearest();
                }
            }
        }

        if(!isPossessing) {
            return;
        }

        objectRbComponent.AddForce(moveInput * accelerationSpeed);
       
    }

    void PossessNearest() {
        isPossessing = true;
        possessedObject = nearestPossessable;
        objectRbComponent = possessedObject.GetComponent<Rigidbody2D>();
        if (!objectRbComponent) {
            UnPossess();
            return;
        }
        SRComponent.enabled = false;
        trail.SetActive(false);

    }

    void UnPossess() {
        isPossessing = false;
        transform.position = possessedObject.transform.position;
        objectRbComponent = null;
        possessedObject = null;
        SRComponent.enabled = true;
        trail.SetActive(true);
    }
}
