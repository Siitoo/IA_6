using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Move : MonoBehaviour {

	public GameObject target;
	public GameObject aim;
	public Slider arrow;
	public float max_mov_velocity = 5.0f;
	public float max_mov_acceleration = 0.1f;
	public float max_rot_velocity = 10.0f; // in degrees / second
	public float max_rot_acceleration = 0.1f; // in degrees

	[Header("-------- Read Only --------")]
	public Vector3 movement = Vector3.zero;
	public float rotation = 0.0f; // degrees

    Vector3[] movement_velocity = new Vector3[5];
    float[] rotation_velocity = new float[5];
    // Methods for behaviours to set / add velocities
    public void SetMovementVelocity (Vector3 velocity) 
	{
        movement = velocity;
	}

	public void AccelerateMovement (Vector3 velocity, int priority) 
	{
        movement_velocity[priority] += velocity;
	}

	public void SetRotationVelocity (float rotation_velocity) 
	{
		rotation = rotation_velocity;
	}

	public void AccelerateRotation (float rotation_acceleration, int priority) 
	{
        rotation_velocity[priority] += rotation_acceleration;
	}

	
	// Update is called once per frame
	void Update () 
	{

        for(int i = 0; i < movement_velocity.Length; ++i)
        {
            if(!Mathf.Approximately(movement_velocity[i].magnitude,0.0F))
            {
                movement += movement_velocity[i];
                break;
            }
        }

        for (int i = 0; i < rotation_velocity.Length; ++i)
        {
            if (!Mathf.Approximately(rotation_velocity[i], 0.0F))
            {
                rotation += rotation_velocity[i];
                break;
            }
        }


        // cap velocity
        if (movement.magnitude > max_mov_velocity)
		{
			movement.Normalize();
			movement *= max_mov_velocity;
		}

		// cap rotation
		rotation = Mathf.Clamp(rotation, -max_rot_velocity, max_rot_velocity);

		// rotate the arrow
		float angle = Mathf.Atan2(movement.x, movement.z);
		aim.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

		// strech it
		arrow.value = movement.magnitude * 4;

		// final rotate
		transform.rotation *= Quaternion.AngleAxis(rotation * Time.deltaTime, Vector3.up);

		// finally move
		transform.position += movement * Time.deltaTime;


      for(int i = 0; i < movement_velocity.Length; ++i)
        {
            movement_velocity[i] = Vector3.zero;
        }



	}
}
