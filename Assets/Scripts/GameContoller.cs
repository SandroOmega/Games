using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContoller : MonoBehaviour {
    public GameObject StartPosition;
    public int StartShape;
    GameObject ShapeLocation;
    int _shapeid;
    [SerializeField]
    bool _ChangeEnable;
    private GameObject Shapeshifter;
    public CameraController cameraController;
    public List<GameObject> Shapes;
    [SerializeField]
    string Changer;
    private GameObject[] currentShapes;
    List<string> AllShapes =new List<string> {"Bat", "Wolf"};
    public bool IsChangeEnable
    {
        /*set
        {
           /* if (value == true)
            {
                _ChangeEnable=true;
            }
            else
            {
                _ChangeEnable = false;
            }

        }*/
        get
        {
            return _ChangeEnable;
        }
    }

    // Use this for initialization
    int ShapeID
    {
        set
        {
            _shapeid = value;
        }

        get {
            return _shapeid;
        }
    }
    private void Awake()
    {
        _ChangeEnable = true;
        currentShapes= new GameObject[Shapes.Count];
        ShapeLocation = new GameObject("ShapeShifter");
        ShapeLocation.transform.position = StartPosition.transform.position;
        int i = 0;
        foreach (GameObject Shape in Shapes)
        {
            currentShapes[i]= Instantiate(Shape,ShapeLocation.transform);
            currentShapes[i].transform.position = ShapeLocation.transform.position;
            if (i != StartShape)
            {
                currentShapes[i].SetActive(false);
            }
            i++;
        }
        Shapeshifter = currentShapes[0];
        cameraController.Shapeshifter = Shapeshifter;
    }

    
    public void ChangeShape(string NewShape)
    {
        int ID = AllShapes.IndexOf(NewShape)+1;
        if (_ChangeEnable)
        {
            if (ShapeID != ID)
            {
                Shapeshifter.SetActive(false);
                Shapeshifter = currentShapes[ID];
                Shapeshifter.SetActive(true);
                ShapeID = ID;
            }
            else
            {
                Shapeshifter.SetActive(false);
                Shapeshifter = currentShapes[0];
                Shapeshifter.SetActive(true);
                ShapeID = 0;
            }
        }
        
    }
	// Update is called once per frame
	void Update () {
        foreach(GameObject Shape in currentShapes)
        {
            Shape.transform.position = Shapeshifter.transform.position;
        }
        //ShapeLocation.transform.position = Shapeshifter.transform.position;
        
        foreach(string key in AllShapes)
        {
            if (Input.GetButtonDown(key))
            {
                Debug.Log(key);
                
                ChangeShape(key);
            }
        }
        //ChangeShape(Input.inputString,ID);
	}
}
