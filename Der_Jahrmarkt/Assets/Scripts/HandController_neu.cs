using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]

public class HandController_neu : MonoBehaviour
{
	ActionBasedController controller;
	
	public Hand_neu hand;
	
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ActionBasedController>();
    }

    // Update is called once per frame
    void Update()
    {
        hand.SetGrip(controller.selectAction.action.ReadValue<float>());
		hand.SetTrigger(controller.activateAction.action.ReadValue<float>());
    }
}