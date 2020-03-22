using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayTracer : MonoBehaviour {

	public bool DEBUG_MOVE = true;

	public float f = 1000.0f;
	public float u = 640.0f;
	public float v = 480.0f;
	 
	//public double [] up = new double[3] { 0.0 , 1.0, 0.0};
	public Vector3 up = Vector3.up;

	public float[,] K = new float[3,3];

	//public Matrix4x4 R;

	public float timeToCompleteCircle = 5.0f;
	public float radius = 300;
	public float currentAngle;

	//Wall points wrt origin
	Vector3[] Wall = new [] { 
		new Vector3( -50.0f,  50.0f, 0.0f), 
		new Vector3( -50.0f, -50.0f, 0.0f), 
		new Vector3(  50.0f, -50.0f, 0.0f), 
		new Vector3(  50.0f,  50.0f, 0.0f), 
	};

	LineRenderer line;

	///*
	Vector3 [] thiShape = new [] {
		new Vector3( 0.0f, 		190.0f, 0.0f),
		new Vector3( -192.0f, 	-144.0f, 0.0f),
		new Vector3( 192.0f,	-144.0f, 0.0f),
		new Vector3( 0.0f, 		190.0f, 0.0f),
		//new Vector3( 0.0f, 		140.0f, 0.0f),
		//new Vector3( -142.0f, 	-114.0f, 0.0f),
		//new Vector3( 142.0f, 	-114.0f, 0.0f),
		//new Vector3( 0.0f, 		140.0f, 0.0f)
	};
	//*/

	// Use this for initialization
	void Start () {

		//Set up camera matrix
		K[0,0] = K[1,1] = f;
		K[0,2] = -u/2.0f;
		K[1,2] = -v/2.0f;
		K[2,2] = -1;


		//GameObject myLine = new GameObject();
		//myLine.transform.position = start;
		this.gameObject.AddComponent<LineRenderer>();
		line = this.gameObject.GetComponent<LineRenderer>();
		//lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
		Color color = Color.magenta;
		line.SetColors(color, color);
		line.SetWidth(10.0f, 10.0f);
		line.positionCount = 4;

		Vector3 ofs = new Vector3( 0.0f, 0.0f, -10.0f);
		line.SetPosition(0, Wall[0] *5.0f + ofs );
		line.SetPosition(1, Wall[1] *5.0f + ofs );
		line.SetPosition(2, Wall[2] *5.0f + ofs );
		line.SetPosition(3, Wall[3] *5.0f + ofs );
		//line.SetPosition(1, end);
		//line.SetPosition(2, this.transform.position );
		//GameObject.Destroy(myLine, duration);
	
	}


	// Update is called once per frame
	void Update () {
		
		// Camera obscura model 
		// lp = K [ R|t ] P, o
		// lp = K R [ I| -c ] P
		// looking at the origin, up vector is [0 1 0]

		//float timeToCompleteCircle = 5.0f;
		float speed = (Mathf.PI * 2.0f) / timeToCompleteCircle;
		//float Cx;
		//float Cy;
		//float radius = 300;

		currentAngle += Time.deltaTime * speed;
		if(currentAngle >= Mathf.PI * 2.0f) currentAngle = 0.0f;

		float Cx = radius * Mathf.Cos (currentAngle);
		float Cy = radius * Mathf.Sin (currentAngle);
		if (DEBUG_MOVE)
			transform.position = new Vector3 (Cx, Cy, transform.position.z);


		transform.LookAt( Vector3.zero );

		//if(LINE_DEBUG){
		Debug.DrawLine(Vector3.zero, transform.position , Color.yellow);
		//}

		/* // Draw Lines
		line.SetPosition(0, start);
		line.SetPosition(1, Vector3.zero );
		line.SetPosition(2, this.transform.position );
		line.SetPosition(3, end);
		//*/
}
