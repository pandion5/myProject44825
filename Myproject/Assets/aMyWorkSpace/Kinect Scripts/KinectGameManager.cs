using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinectGameManager : MonoBehaviour
{
    public static KinectGameManager instance;

    void Awake()
    {
        instance = this;
    }
    [Header("Charactor")]
    public AvatarController avatarController;

    [Header("Target")]
    public GameObject Target;

    [Header("DollyCart")]
    public GameObject player;
    public float moveSpeed = 3.4f;

    [Header("Kinect")]
    public KinectManager kinectManager;
    public GameObject feetCollider;
    public GameObject neck;

    [Header("Feets")]
    public GameObject leftFeet;
    public GameObject rightFeet;

    [Header("Colliders")]
    public GameObject leftFeetCollider;
    public GameObject rightFeetCollider;

    [Space]
    [Header("Current Steps")]
    public bool leftStep;
    public bool rightStep;

    private bool prevLeftStep;
    private bool prevRightStep;

    [Header("Walking Rate")]
    public int deltaStep;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(deltaStepInit());
    }

    private void FixedUpdate()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    private void LateUpdate()
    {
        if (kinectManager.avatarControllers.Count >= 1)
        {
            if (!avatarController)
            {
                if (leftFeet == null && rightFeet == null)
                {
                    avatarController = kinectManager.avatarControllers[0];
                    Setup(avatarController);
                    return;
                }
            }

            Walk();

            if (deltaStep > 0)
                player.GetComponent<slow>().inputY = 1.0f;
            else
                player.GetComponent<slow>().inputY = 0.0f;



            float angle =transform.eulerAngles.y+ neck.transform.eulerAngles.y;
            Debug.Log(angle);
            if (angle > 10)
            {
                player.GetComponent<slow>().inputX = 1.0f;
            }
            else if (angle < -10)
            {
                player.GetComponent<slow>().inputX = -1.0f;
            }
            else
            {
                player.GetComponent<slow>().inputX = 0;
            }


        }
    }

    void Walk()
    {
        bool state = false;

        if (prevRightStep == rightStep && prevLeftStep == leftStep)
        {
            return;
        }

        if (prevRightStep == prevLeftStep)
        {
            prevLeftStep = leftStep;
            prevRightStep = rightStep;
            state = false;
        }
        else
        {
            prevLeftStep = leftStep;
            prevRightStep = rightStep;
            state = true;
        }

        if (state)
        {
            deltaStep++;
        }
    }

    IEnumerator deltaStepInit()
    {
        if (player)
            while (true)
            {
                deltaStep = 0;

                yield return new WaitForSeconds(2f);

            }
    }

    void Setup(AvatarController avatarControllers)
    {
        if (leftFeet = GameObject.Find("Left_Ankle_Joint_01"))
        {
            if (rightFeet = GameObject.Find("Right_Ankle_Joint_01"))
            {
                leftFeetCollider = GameObject.Instantiate(feetCollider, leftFeet.transform);
                rightFeetCollider = GameObject.Instantiate(feetCollider, rightFeet.transform);

                leftFeetCollider.transform.SetParent(leftFeet.transform);
                rightFeetCollider.transform.SetParent(rightFeet.transform);
            }
        }

        if (neck = GameObject.Find("Neck"))
        {
            return;
        }
    }
}
