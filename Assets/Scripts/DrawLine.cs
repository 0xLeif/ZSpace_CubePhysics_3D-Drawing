using UnityEngine;
using System.Collections;

public class DrawLine : MonoBehaviour {
	public ZSStylusSelector stylus;
	public GameObject stylusTip, endPoints, middle,stylusBeam,stylusBeamEnd;
	private bool buttonDown, pointSwitch;
	private Color[] colorArray;
	private int colorNumber,objectCount, lineCount;
	private Material paintColor;
	private Vector3 location0,location1;
	private Transform container, line,point0 = null,point1 = null;

	// Use this for initialization
	void Start () {
		buttonDown = false;
		colorNumber = 0;
		objectCount = 0;
		lineCount = 0;
		colorArray = new Color[]{Color.gray, Color.blue,Color.red, Color.green, Color.magenta, Color.yellow,Color.white};
		changeColor(colorNumber);
		container = new GameObject("Drawing").transform;
		pointSwitch = true;
	}

	// Update is called once per frame
	void Update () {
		checkForUserInput ();
	}

	void createLine(Vector3 loc1, Vector3 loc2){
		line = new GameObject ("Line: " + lineCount++).transform;
		float distance = Vector3.Distance(location0,location1);
		Vector3 slopeRatio = location1-location0;
		Vector3 rateOfSpheres = slopeRatio/100;
		Vector3 relativePos = location1 - (loc1 + slopeRatio/2);
		Object point1 = Instantiate(endPoints,loc1,Quaternion.identity);
		Object point2 = Instantiate(endPoints,loc2,Quaternion.identity);
		Object go = Instantiate(middle,loc1 + slopeRatio/2,Quaternion.LookRotation(relativePos));
		((GameObject)point1).transform.name = "EndPoint: "+objectCount++;
		((GameObject)point2).transform.name = "EndPoint: "+objectCount++;
		((GameObject)go).transform.name = "Middle: "+objectCount++;
		((GameObject)go).transform.Rotate (90,0,0);
		((GameObject)go).transform.localScale = new Vector3(endPoints.transform.localScale.x,distance/2,endPoints.transform.localScale.z);
		((GameObject)go).transform.parent = line;
		((GameObject)point1).transform.parent = line;
		((GameObject)point2).transform.parent = line;
		line.transform.parent = container;
	}


	int c = 0;
	void checkForUserInput(){
		if(stylus.GetButton(0)){
			if(!point0){
				point0 = stylusTip.transform;
				location0 = stylusTip.transform.position;
			}else if(!point1){
				point1 =  stylusTip.transform;
				location1 =  stylusTip.transform.position;
			}else{
				endPoints.transform.localScale = new Vector3(.01f,.01f,.01f);
				createLine(location0,location1);
				if(pointSwitch)
					point0 = null;
				else
					point1 = null;
				pointSwitch = !pointSwitch;
			}
		}else if(stylus.GetButtonUp(0)){
			point0 = null;
			point1 = null;
		}else if(stylus.GetButtonDown(1)){
			if(colorNumber+1 < colorArray.Length)
				changeColor(++colorNumber);
		}else if(stylus.GetButtonDown(2)){
			if(colorNumber-1 >= 0)
				changeColor(--colorNumber);
		}else if(Input.GetKeyDown(KeyCode.C)){
			Destroy(container.gameObject);
			lineCount = 0;
			objectCount = 0;
			container = new GameObject("Drawing").transform;
		}else if(Input.GetKeyDown(KeyCode.Z)){
			Destroy(container.GetChild(container.childCount-1).gameObject);
		}
	}

	void changeColor(int i){
		paintColor = new Material (Shader.Find("Diffuse"));
		paintColor.color = colorArray [i];
		endPoints.gameObject.renderer.material = paintColor;
		middle.gameObject.renderer.material = paintColor;
		stylusBeam.gameObject.renderer.material = paintColor;
		stylusBeamEnd.gameObject.renderer.material = paintColor;
	}
}
