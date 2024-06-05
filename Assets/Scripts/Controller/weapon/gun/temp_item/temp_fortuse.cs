using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp_fortuse : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objectTo;
    public GameObject[] countObjects;
    public int fortnum=0;
    public int fortready=1;


    void Start()
    {
        fortnum=0;
        fortready=1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            CreateFort();
        }
       
    } 
    public void CreateFort()
    {
        countObjects = GameObject.FindGameObjectsWithTag("fort");
        fortnum=countObjects.Length;
        // Debug.Log(fortnum);
        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 cameraForward = Camera.main.transform.forward;
        GameObject myObject = objectTo;
        Vector3 objectPos = cameraPos + cameraForward * 10;

        objectPos.y = cameraPos.y;

        if(fortnum>20)
        {
            fortready=0;
        }
        else if(fortnum<=20 && fortready==1){
            fortready=1;
            GameObject instantiatedObject=Instantiate(myObject, objectPos, Quaternion.identity);
            Destroy(instantiatedObject,10f);
        }
    }

}
