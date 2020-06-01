using UnityEngine;

public class CarCamera : MonoBehaviour
{
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lTargetDir = transform.position - camera.transform.position;
        camera.transform.rotation = Quaternion.RotateTowards(camera.transform.rotation, Quaternion.LookRotation(lTargetDir), 30f*Time.deltaTime);
        camera.transform.position = Vector3.Lerp(camera.transform.position,
            transform.position + Vector3.up * 30 + Vector3.back * 30 + Vector3.left * 30, 10f * Time.deltaTime);
    }
}
