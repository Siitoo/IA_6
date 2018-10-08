using UnityEngine;
using System.Collections;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

public class SteeringFollowPath : SteeringAbstract {

	Move move;
	SteeringSeek seek;
    public BGCcMath path;

    Vector3 closet_point;
    float range_distance;
	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
		seek = GetComponent<SteeringSeek>();

        // TODO 1: Calculate the closest point in the range [0,1] from this gameobject to the path
        float distance;
        closet_point = path.CalcPositionByClosestPoint(transform.position, out distance);

        range_distance = distance / path.GetDistance();
	}
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 2: Check if the tank is close enough to the desired point
        // If so, create a new point further ahead in the path

        Vector3 position_in_curve = closet_point - transform.position;

        if(position_in_curve.magnitude < 0.05) // min distance to closet point center
        {
            range_distance += 0.05f; //ratio distance

            if(range_distance > 1.0f)
            {
                range_distance = 0.0f;
            }

            closet_point = path.CalcPositionByDistanceRatio(range_distance);
        }
        else
        {
            seek.Steer(closet_point);
        }


	}

	void OnDrawGizmosSelected() 
	{

		if(isActiveAndEnabled)
		{
			// Display the explosion radius when selected
			Gizmos.color = Color.green;
			// Useful if you draw a sphere were on the closest point to the path
		}

	}
}
