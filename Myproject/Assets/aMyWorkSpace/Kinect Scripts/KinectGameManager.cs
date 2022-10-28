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

    public float fixFront = 0;
    public int rotationLimit = 5;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.kinectGameManagerScript = this;
        StartCoroutine(deltaStepInit());
    }

    private void FixedUpdate()
    {
            
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FixFront()
    {
        fixFront = calcAngle();
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
                player.GetComponent<KinectWalk>().inputY = 1.0f;
            else
                player.GetComponent<KinectWalk>().inputY = 0.0f;



            float angle = calcAngle();

            if (angle > fixFront + rotationLimit)
            {
                player.GetComponent<KinectWalk>().inputX = 1.0f;
            }
            else if (angle < fixFront - rotationLimit)
            {
                player.GetComponent<KinectWalk>().inputX = -1.0f;
            }
            else
            {
                player.GetComponent<KinectWalk>().inputX = 0;
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
                yield return new WaitForSeconds(4f);

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

    public float calcAngle()
    {
        Vector3 angle = neck.transform.localEulerAngles;
        float x = angle.x;
        float y = angle.y;
        float z = angle.z;

        if (Vector3.Dot(neck.transform.up, Vector3.up) >= 0f)
        {
            if (angle.x >= 0f && angle.x <= 90f)
            {
                x = angle.x;
            }
            if (angle.x >= 270f && angle.x <= 360f)
            {
                x = angle.x - 360f;
            }
        }
        if (Vector3.Dot(neck.transform.up, Vector3.up) < 0f)
        {
            if (angle.x >= 0f && angle.x <= 90f)
            {
                x = 180 - angle.x;
            }
            if (angle.x >= 270f && angle.x <= 360f)
            {
                x = 180 - angle.x;
            }
        }

        if (angle.y > 180)
        {
            y = angle.y - 360f;
        }

        if (angle.z > 180)
        {
            z = angle.z - 360f;
        }

        //Debug.Log(angle + " :::: " + Mathf.Round(x) + " , " + (Mathf.Round(y)) + " , " + Mathf.Round(z));
        Debug.Log(Mathf.Round(y));
        return Mathf.Round(y);    
    }
}
