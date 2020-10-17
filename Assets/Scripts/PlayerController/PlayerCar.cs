using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTemplateProjects.PlayerController
{
    public class PlayerCar : MonoBehaviour
    {
        public Rigidbody Rigidbody;
        public Vector2 Input;
        public IInput[] Inputs;
        public float accelTimer;
        public const float MAX_FORWARD_SPEED = 10.0f;
        public const float MIN_FORWARD_SPEED = 5.0f;
        public const float START_SPEED = 7.5f;
        public const float ACCEL = 1.0f;

        // Start is called before the first frame update
        void Start()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Inputs = GetComponents<IInput>();
            accelTimer = 0.0f;
            Rigidbody.velocity = new Vector3(0.0f, 0.0f, START_SPEED);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            GatherInputs();

            // Processing inputs
            float linear = Input.y;

            // Getting acceleration factor
            float accelDir;
            if (linear > 0)
            {
                accelDir = 1.0f;
            }
            else if (linear < 0)
            {
                accelDir = -1.0f;
            }
            else
            {
                accelDir = 0.0f;
            }

            // Applying acceleration if necessary
            float totalAccel = 0.0f;
            if (accelDir != 0)
            {
                totalAccel = accelDir * ACCEL;
                accelTimer += Time.deltaTime;
            }
            else
            {
                accelTimer = 0.0f;
            }

            // Getting current car state
            Vector3 fwd = Rigidbody.transform.forward;

            // Finding new velocity and updating
            Vector3 adjVelocity = Rigidbody.velocity + fwd * totalAccel * Time.deltaTime;

            // Clamping to min/max linear speed
            if (adjVelocity.z > MAX_FORWARD_SPEED)
            {
                adjVelocity.z = MAX_FORWARD_SPEED;
            }
            else if (adjVelocity.z < MIN_FORWARD_SPEED)
            {
                adjVelocity.z = MIN_FORWARD_SPEED;
            }

            Rigidbody.velocity = adjVelocity;
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