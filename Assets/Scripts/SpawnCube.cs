using UnityEngine;
using System.Collections;

public class SpawnCube : MonoBehaviour {
	public ZSStylusSelector stylusSelector;
	public GameObject stylusTip, objectToSpawn;

	// Use this for initialization
	void Start () {
		objectToSpawn.transform.localScale = new Vector3(.5f,.5f,.5f);
		count = 0;
	}

	private int count;
	// Update is called once per frame
	void Update () {
		if (stylusSelector.GetButtonUp (1)) {
			print (count);
			switch(count){
			case 0:
				objectToSpawn.transform.localScale = new Vector3(1,1,1);
				count++;
				break;
			case 1:
				objectToSpawn.transform.localScale = new Vector3(.1f,.1f,.1f);
				count++;
				break;
			case 2:
				objectToSpawn.transform.localScale = new Vector3(.5f,.5f,.5f);
				count = 0;
				break;
			}
		}else if(stylusSelector.GetButtonUp(2)){
			Instantiate(objectToSpawn, stylusTip.transform.position, Quaternion.identity);
		}
	}

}
