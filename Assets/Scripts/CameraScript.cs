using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour//make sure this always matches on unity
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {

    }
    
    // Update is called once per frame//when using this for camera do late update (give time for player object to do what it has to do b4 updating)
    void LateUpdate()
    {
        this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y,this.transform.position.z); //update x based on character movement 
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
