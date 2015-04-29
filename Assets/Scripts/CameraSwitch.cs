using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {
	private bool sideView;

	// Use this for initialization
	void Start () {
		sideView = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Space)){
			sideView = !sideView;
			changeView ();
		}
	}

	void changeView(){
		if (sideView) {
			Camera.main.transform.position = new Vector3 (-.5f, 1, -9.49f);
			Camera.main.transform.rotation = Quaternion.Euler (0, 90, 0);
		} else {
			Camera.main.transform.position = new Vector3(0,1,-10);
			Camera.main.transform.rotation = Quaternion.Euler(0,0,0);
		}
	}
}
