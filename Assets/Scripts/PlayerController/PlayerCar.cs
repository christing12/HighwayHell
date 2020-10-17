using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityTemplateProjects.PlayerController
{
    public class PlayerCar : MonoBehaviour
    {
        public Rigidbody rb;
        public Vector2 Input;
        public IInput[] Inputs;
        public float MAX_FORWARD_SPEED = 0.1f;
        public float MIN_FORWARD_SPEED = 0.075f;
        public float START_SPEED = 0.5f;
        public float ACCEL = 0.2f;
        public float TURN = 2.0f;
        public float MAX_TURN = 10.0f;
        public float ANG_DECEL = 1.0f;
        public float angularVelocitySmoothSpeed = 20f;
        public Text speedText;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            Inputs = GetComponents<IInput>();
            rb.velocity = new Vector3(0.0f, 0.0f, START_SPEED);
            speedText.text = (START_SPEED*10).ToString("F1");
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

            // Getting current car state
            Quaternion turnAngle = Quaternion.AngleAxis(rotational * TURN, rb.transform.up);
            Vector3 fwd = turnAngle * rb.transform.forward;

            // Finding new linear velocity and updating
            Vector3 forwardAccel = rb.transform.forward * linear * ACCEL;

            Vector3 adjVelocity = rb.velocity + forwardAccel*Time.deltaTime;

            // Clamping to forward and backward speed
            if (adjVelocity.magnitude > MAX_FORWARD_SPEED)
            {
                adjVelocity = rb.transform.forward * MAX_FORWARD_SPEED;
            }
            else if (adjVelocity.magnitude < MIN_FORWARD_SPEED)
            {
                adjVelocity = rb.transform.forward * MIN_FORWARD_SPEED;
            }

            // Clamping to min/max rotational speed
            if (Mathf.Abs(adjVelocity.x) > MAX_TURN)
            {
                if (adjVelocity.x < 0)
                {
                    adjVelocity.x = -1 * MAX_TURN;
                }
                else
                {
                    adjVelocity.x = MAX_TURN;
                }
            }

            rb.velocity = adjVelocity;

            var angularVel = rb.angularVelocity;

            // move the Y angular velocity towards our target
            float angularVelocitySteering = .4f;
            angularVel.y = Mathf.MoveTowards(angularVel.y, rotational * TURN * angularVelocitySteering, Time.deltaTime * angularVelocitySmoothSpeed);

            // apply the angular velocity
            rb.angularVelocity = angularVel;

            float velocitySteering = 25f;
            // rotate our velocity based on current steer value
            rb.velocity = Quaternion.Euler(0f, rotational * TURN * velocitySteering * Time.deltaTime, 0f) * rb.velocity;

            //Update speed text on screen
            speedText.text = (rb.velocity.magnitude*10).ToString("F1");
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