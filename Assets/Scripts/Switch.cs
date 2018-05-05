using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour,IAction {
    SpriteRenderer SR;
    bool IsActivated;
    public GameObject[] Subjects;
	// Use this for initialization
	void Start () {
        SR = GetComponent<SpriteRenderer>();
        //StartCoroutine("Activate");
    }
    IEnumerator Activate()
    {
        while (true)
        {
            Action();
            yield return new WaitForSeconds(5f);
        }
    }

    public void Action()
    {
        Debug.Log("Un Poco Loco");
        if (IsActivated == false)
        {

            SR.color = Color.red;
            IsActivated = true;
        }
        else
        {
            SR.color = Color.white;
            IsActivated = false;
        }
        SomeChanges();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag.Equals("Human"))
        {
            #if UNITY_EDITOR
                Debug.Log("What the Hay");
            #endif
            if (Input.GetButtonDown("Use"))
            {
                Action();
            }
        }
    }

    void SomeChanges() {
    foreach (GameObject A in Subjects)
        {
            A.SetActive(! A.activeSelf);
        }
    }


    // Update is called once per frame

}
