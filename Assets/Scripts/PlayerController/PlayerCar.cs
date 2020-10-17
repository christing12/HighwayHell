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
        public float MAX_FORWARD_SPEED = 10.0f;
        public float MIN_FORWARD_SPEED = 5.0f;
        public float START_SPEED = 7.5f;
        public float ACCEL = 1.0f;
        public float TURN = 2.0f;
        public float MAX_TURN = 3.0f;
        public float ANG_DECEL = 1.0f;

        // Start is called before the first frame update
        void Start()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Inputs = GetComponents<IInput>();
            Rigidbody.velocity = new Vector3(0.0f, 0.0f, START_SPEED);
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
            Vector3 fwd = Rigidbody.transform.forward;

            // Finding new linear velocity and updating
            Vector3 adjVelocity = Rigidbody.velocity + new Vector3(0,0, linear * ACCEL * Time.deltaTime);

            // Finding new rotation veloicty and updating
            Vector3 turnAccel = new Vector3(TURN * rotational, 0, 0);

            if (rotational == 0)
            {
                if (Mathf.Abs(adjVelocity.x) > 0)
                {
                    if (adjVelocity.x > 0)
                    {
                        if (Mathf.Abs(adjVelocity.x) < ANG_DECEL * Time.deltaTime)
                        {
                            turnAccel = new Vector3(-1*adjVelocity.x / Time.deltaTime,0,0);
                        }
                        else
                        {
                            turnAccel = new Vector3(-1 * ANG_DECEL, 0, 0);
                        }
                    }
                    else
                    {
                        if (Mathf.Abs(adjVelocity.x) < ANG_DECEL * Time.deltaTime)
                        {
                            turnAccel = new Vector3(-1*adjVelocity.x / Time.deltaTime,0,0);
                        }
                        else
                        {
                            turnAccel = new Vector3(ANG_DECEL, 0, 0);
                        }
                    }
                }
            }

            adjVelocity += turnAccel*Time.deltaTime;

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

            // Clamping to min/max linear speed
            if (adjVelocity.z > MAX_FORWARD_SPEED)
            {
                adjVelocity.z = MAX_FORWARD_SPEED;
            }
            else if (adjVelocity.z < MIN_FORWARD_SPEED)
            {
                adjVelocity.z = MIN_FORWARD_SPEED;
            }

            Quaternion rotationQuat = new Quaternion();
            rotationQuat.SetFromToRotation(fwd, adjVelocity);

            Rigidbody.rotation = rotationQuat;



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