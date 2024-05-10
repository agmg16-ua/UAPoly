using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Transform[] waypoints = new Transform[41];

    [SerializeField]
    private float moveSpeed = 5f;

    public bool otraVuelta = false;

    [HideInInspector]
    public int waypointIndex = 0;

    public bool moveAllowed = false;

    // Start is called before the first frame update
    private void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (moveAllowed)
        {
            Move();
        }
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                waypoints[waypointIndex].transform.position,
                moveSpeed * Time.deltaTime);

            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }

            if ((waypointIndex == waypoints.Length - 1) && (otraVuelta == true))
            {
                waypointIndex = 0;
            }
        }
    }
}
