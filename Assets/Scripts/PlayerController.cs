using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour{
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
    [SerializeField] Color possessedColor;
    public float maxDist = 1.3f;
    [SerializeField] public bool isPossessing = false;
    [SerializeField] GameObject possessedObject;

    Rigidbody2D objectRbComponent;

    [Header("Chaos")]
    [SerializeField] ChaosManager chaos;
    [SerializeField] float dismountThreshold = 5.0f;
    [SerializeField] float collisionThreshold = 7.0f;
    [SerializeField] float crashThreshold = 10.0f;

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
            if (playerRbComponent.velocity.magnitude < 0.05f) {
                playerRbComponent.velocity = Vector2.zero;
            } else {
                playerRbComponent.AddForce((playerRbComponent.velocity.normalized * -1) * declerationSpeed);
            }
        }

        if(playerRbComponent.velocity.magnitude > maxSpeed) {
            playerRbComponent.velocity = playerRbComponent.velocity.normalized * maxSpeed;
            
        }

        gameObject.transform.localScale = new Vector3(0.1f * direction, 0.1f, 1.0f);

        //Possession
        if(nearestPossessable != null && !isPossessing) {
            //Debug.DrawRay(transform.position, (nearestPossessable.transform.position - transform.position).normalized * maxDist, Color.red);
            //Debug.Log((nearestPossessable.transform.position - transform.position).magnitude);

            if ((nearestPossessable.transform.position - transform.position).magnitude > maxDist) {
                nearestPossessable.GetComponent<Possessable>().UnPulse();
                nearestPossessable = null;
                maxDist = 1.3f;
            }
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

        if(isPossessing) {
            objectRbComponent.AddForce(moveInput * accelerationSpeed);
            transform.position = new Vector3(possessedObject.transform.position.x, possessedObject.transform.position.y, -1);
        }
    }

    void PossessNearest() {
        isPossessing = true;
        possessedObject = nearestPossessable;
        objectRbComponent = possessedObject.GetComponent<Rigidbody2D>();
        if (!objectRbComponent) {
            UnPossess();
            return;
        }
        possessedObject.GetComponent<Possessable>().UnPulse();
        possessedObject.GetComponent<Possessable>().collisionCallback.AddListener(PossessionCollisionPoints);
        possessedObject.GetComponent<SpriteRenderer>().color = possessedColor;
        SRComponent.enabled = false;
        
        trail.GetComponent<LineRenderer>().startWidth = 0.0f;
        AddChaos(1.0f, "Possession");
    }

    void UnPossess() {
        isPossessing = false;
        possessedObject.GetComponent<Possessable>().Pulse();
        possessedObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
        possessedObject.GetComponent<Possessable>().collisionCallback.RemoveAllListeners();
        AddChaos(2.0f, "DePossession");
        if (objectRbComponent.velocity.magnitude > dismountThreshold) {
            AddChaos(10.0f, "Dismount");
        }

        objectRbComponent = null;
        possessedObject = null;
        SRComponent.enabled = true;

        trail.GetComponent<Trail>().Reset();
        trail.GetComponent<LineRenderer>().startWidth = 0.66f;
    }

    public void AddChaos(float amount, string reason) {
        chaos.AddChaos(amount);
        chaos.addPointMessages(string.Format("+{0} {1}", (int)amount, reason));
    }

    public void PossessionCollisionPoints() {
        if(objectRbComponent.velocity.magnitude > collisionThreshold) {;
            AddChaos(5.0f, "Collision 2D");
        }

        if (objectRbComponent.velocity.magnitude > crashThreshold) {
            AddChaos(7.0f, "Crash");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Solid") {
            Vector3 calculatedForce = playerRbComponent.mass * -1.0f * (playerRbComponent.velocity / Time.deltaTime);
            playerRbComponent.AddForce(calculatedForce);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Solid") {
            Vector3 calculatedForce = playerRbComponent.mass * -1.0f * (playerRbComponent.velocity / Time.deltaTime);
            playerRbComponent.AddForce(calculatedForce);
        }
    }
}
