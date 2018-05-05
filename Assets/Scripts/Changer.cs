using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changer : MonoBehaviour,IAction {
    GameContoller GC;
    public string Shapes;
	// Use this for initialization
	void Start () {
      
        GC = GameObject.Find("GameController").GetComponent<GameContoller>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Action()
    {
        if (GC.IsChangeEnable == true)
        {
            GC.ChangeShape(Shapes);
           /// GC.IsChangeEnable = !GC.IsChangeEnable;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Action();
        

    }
}
