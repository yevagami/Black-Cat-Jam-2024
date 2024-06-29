using UnityEngine;
 
public class CameraFollow : MonoBehaviour
{

    [SerializeField] GameObject objToFollow;
    public float dampTime;
    Vector3 vel = Vector3.zero;

    // Update is called once per frame
    void Update(){
        Vector3 newPos = Vector3.SmoothDamp(transform.position, objToFollow.transform.position, ref vel, dampTime);
        Vector3 Pos = new Vector3(newPos.x, newPos.y, transform.position.z);
        transform.position = Pos;
    }
}
