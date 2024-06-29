using UnityEngine;

public class TransformAnimations : MonoBehaviour
{
    public Transform target;
    public float speed = 1.0f;
    float t = 0.0f;
    


    public void EaseOut() {
        t += Time.deltaTime * speed;
        float deltaT = Mathf.Clamp01(2 * t * t);
        Vector3 pos = Vector3.Lerp(target.position, transform.position, deltaT);
        Vector3 scale = Vector3.Lerp(target.localScale, transform.localScale, deltaT);
        gameObject.transform.position = pos;
        gameObject.transform.localScale = scale;    
    }
}
