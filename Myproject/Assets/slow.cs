using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slow : MonoBehaviour
{
    public float speedTreshold = 0.1f;

    public float inputY;//전진 입력 ( 키넥트 )
    public float inputX;//회전 입력 ( 키넥트)

    [Range(0, 1)]
    public float smoothing = 1;
    public GameObject player;
    public Animator animator;
    public Vector3 previousPos;
    public Quaternion previousRotation;
    public Vector3 angularVelocity;

    public Vector3 headsetLocalSpeed;
    public bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        previousPos = transform.position;
        previousRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = inputX;//Input.GetAxis("Horizontal");
        float vertical = inputY;//Input.GetAxis("Vertical");

        Vector3 position = transform.position;



        player.transform.Translate(Vector3.forward * Time.deltaTime * vertical * 2);
        player.transform.Rotate(Vector3.up * Time.deltaTime * horizontal * 45);

        //Compute the speed
        Vector3 headsetSpeed = (player.transform.position - previousPos) / Time.deltaTime;
        headsetSpeed.y = 0;

        Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(previousRotation);

        //Local Speed
        headsetLocalSpeed = transform.InverseTransformDirection(headsetSpeed);
        previousPos = player.transform.position;
        previousRotation = transform.rotation;

        deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);

        angle *= Mathf.Deg2Rad;


        angularVelocity = ((1.0f / Time.deltaTime) * angle * axis);

        //Set Animator Values
        float previousDirectionX = animator.GetFloat("DirectionX");
        float previousDirectionY = animator.GetFloat("DirectionY");
        isMoving = headsetLocalSpeed.magnitude > speedTreshold || angularVelocity.magnitude > speedTreshold;


        animator.SetBool("isMoving", isMoving);
        animator.SetFloat("DirectionX", Mathf.Lerp(previousDirectionX, Mathf.Clamp(angularVelocity.y, -1, 1), smoothing));
        animator.SetFloat("DirectionY", Mathf.Lerp(previousDirectionY, Mathf.Clamp(headsetLocalSpeed.z, -1, 1), smoothing));
    }
}
