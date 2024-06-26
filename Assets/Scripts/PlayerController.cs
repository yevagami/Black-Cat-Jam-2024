using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Rigidbody2D RbComponent;
    [SerializeField] float accelerationSpeed;
    [SerializeField] float declerationSpeed;
    [SerializeField] float maxSpeed;
    public int direction = 1;
    Vector2 moveInput = Vector2.zero;

    [Header("Animation")]
    [SerializeField] SpriteRenderer SRComponent;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(Mathf.Abs(moveInput.x) > 0) {
            direction = (int) moveInput.x;
        }

        RbComponent.AddForce(moveInput * accelerationSpeed);

        if(moveInput.magnitude <= 0) {
            RbComponent.AddForce((RbComponent.velocity.normalized * -1) * declerationSpeed);
        }

        if(RbComponent.velocity.magnitude > maxSpeed) {
            RbComponent.velocity = RbComponent.velocity.normalized * maxSpeed;
        }
    }
}
