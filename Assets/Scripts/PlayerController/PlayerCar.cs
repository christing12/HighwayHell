using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UnityTemplateProjects.PlayerController
{
    public class PlayerCar : MonoBehaviour
    {
        public Rigidbody rb;
        public Vector2 Input;
        public IInput[] Inputs;
        public float MAX_FORWARD_SPEED = 5.0f;
        public float MIN_FORWARD_SPEED = 2.5f;
        public float START_SPEED = 3.75f;
        public float ACCEL = 0.2f;
        public float TURN = 2.0f;
        public float MAX_TURN = 10.0f;
        public float ANG_DECEL = 1.0f;
        public float angularVelocitySmoothSpeed = 20f;
        public float angularVelocitySteering = 0.4f;
        public float steeringVelocity = 25f;
        public TextMeshProUGUI speedText;
        public TextMeshProUGUI score;


        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            Inputs = GetComponents<IInput>();
            rb.velocity = new Vector3(0.0f, 0.0f, START_SPEED);
            speedText.SetText((START_SPEED * 10).ToString("F1"));
        }
        private void Update()
        {


        }
        // Update is called once per frame
        void FixedUpdate()
        {
            GatherInputs();

            // Processing inputs
            float linear = Input.y;
            float rotational = Input.x;

            // Getting linear acceleration factor
            if (linear > 0)
            {
                linear = 1.0f;
            }
            else if (linear < 0)
            {
                linear = -1.0f;
            }
            else
            {
                linear = 0.0f;
            }

            // Getting rotational acceleration factor
            if (rotational > 0)
            {
                rotational = 1.0f;
            }
            else if (rotational < 0)
            {
                rotational = -1.0f;
            }
            else
            {
                rotational = 0.0f;
            }

            //Stop linear acceleration (forward) in the air
            //Check if outside of the threshold
            if (Mathf.Abs(rb.transform.forward.y) > 0.05)
            {
                linear = 0.0f;
            }

            // Getting current car state
            Quaternion turnAngle = Quaternion.AngleAxis(rotational * TURN, rb.transform.up);
            Vector3 fwd = turnAngle * rb.transform.forward;

            // Finding new linear velocity and updating
            Vector3 forwardAccel = fwd * linear * ACCEL;

            Vector3 adjVelocity = rb.velocity + forwardAccel * Time.deltaTime;

            // Clamping to forward and backward speed
            float planeSpeed = Mathf.Sqrt(Mathf.Pow(adjVelocity.x,2) + Mathf.Pow(adjVelocity.z,2));
            if (planeSpeed > MAX_FORWARD_SPEED)
            {
                adjVelocity = new Vector3(0,rb.velocity.y,0) + fwd * MAX_FORWARD_SPEED;
            }
            else if (planeSpeed < MIN_FORWARD_SPEED)
            {
                adjVelocity = new Vector3(0, rb.velocity.y, 0) + fwd * MIN_FORWARD_SPEED;
            }

            rb.velocity = adjVelocity;

            var angularVel = rb.angularVelocity;

            // move the Y angular velocity towards our target
            angularVel.y = Mathf.MoveTowards(angularVel.y, rotational * TURN * angularVelocitySteering, Time.deltaTime * angularVelocitySmoothSpeed);

            // apply the angular velocity
            rb.angularVelocity = angularVel;

            // rotate our velocity based on current steer value
            rb.velocity = Quaternion.Euler(0f, rotational * TURN * steeringVelocity * Time.deltaTime, 0f) * rb.velocity;

            // Resetting local z to 0 to prevent local z rotation
            Vector3 localRot = rb.transform.localRotation.eulerAngles;
            Quaternion adjLocRot = Quaternion.Euler(localRot + new Vector3(0, 0, -1 * localRot.z));
            rb.transform.localRotation = adjLocRot;

            //Update speed text on screen
            speedText.SetText((rb.velocity.magnitude * 10).ToString("F1"));
        }

        void GatherInputs()
        {
            // reset input
            Input = Vector2.zero;

            // gather nonzero input from our sources
            for (int i = 0; i < Inputs.Length; i++)
            {
                var inputSource = Inputs[i];
                Vector2 current = inputSource.GenerateInput();
                if (current.sqrMagnitude > 0)
                {
                    Input = current;
                }
            }
        }
    }
}