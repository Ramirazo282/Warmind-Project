using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCard : MonoBehaviour {

    public GameObject movingCard;

    public float speed;

    private float startTime;
    private float journeyLength;

    public Transform startLocation;
    public Transform endLocation;
    bool Moving;

	public void SetUpLocation (GameObject m)
    {
        movingCard = m;
        Instantiate(startLocation);
        startLocation.transform.position = movingCard.transform.position;
        journeyLength = Vector3.Distance(startLocation.transform.position, startLocation.transform.position);
        startTime = Time.time;
        Moving = true;
	}
    void Update()
    {
        if (Moving)
        {
            float distanceCoverd = (Time.time - startTime) * speed;

            float fracJourney = distanceCoverd / journeyLength;

            movingCard.transform.position = Vector3.Lerp(startLocation.transform.position, endLocation.position, fracJourney);
            if (movingCard.transform.position == endLocation.position)
            {
                movingCard = null;
                Moving = false;
            }
        }
    }
	
	
}
