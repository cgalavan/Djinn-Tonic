using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Change_Position : MonoBehaviour
{
	//Position
	public GameObject Point1;
	public GameObject Point2;
	int Target_Point = 0;
	float Position_Timer;

	//Movement
	public float Movement_Speed = 2f;
	public GameObject Player;
	public bool Activate;


    // Start is called before the first frame update
    void Start()
    {
		Position_Timer = 2f;
		this.gameObject.transform.position = new Vector3 (Point2.transform.position.x, Point2.transform.position.y, Point2.transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
		if ((this.gameObject.transform.position - Player.transform.position).sqrMagnitude < 50f){

			Activate = true;
		
		}
		else{
			Activate = false;
		}

		if (Activate == true){
		Position_Timer -= Time.deltaTime;

		if (Position_Timer < 0){
			Change_Point();
			Position_Timer = 2f;
		}
		}

		
    }

	void Change_Point(){
	
		Target_Point ++;

		switch (Target_Point){

		case 1:
			this.gameObject.transform.position = new Vector3 (Point1.transform.position.x, Point1.transform.position.y, Point1.transform.position.z);
			break;
		case 2:
			this.gameObject.transform.position = new Vector3 (Point2.transform.position.x, Point2.transform.position.y, Point2.transform.position.z);
			Target_Point = 0;
			break;
		}

	}


}
