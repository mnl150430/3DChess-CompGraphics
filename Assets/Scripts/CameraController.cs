using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraController : MonoBehaviour 
{

	public Transform[] views;

    public Text[] viewButtons;
	public float transitionSpeed;

	Transform currentView;

	void Start ()
	{
		

	}
   void Update()
    {
        for (int x = 0; x < views.Length; x++)
            if (viewButtons[x].color == Color.red)
                currentView = views[x];

    }
    // Update is called once per frame
    void LateUpdate () {

        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);

        Vector3 currentAngle = new Vector3(
             Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
             Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
             Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));

        transform.eulerAngles = currentAngle;
    
    }
}
