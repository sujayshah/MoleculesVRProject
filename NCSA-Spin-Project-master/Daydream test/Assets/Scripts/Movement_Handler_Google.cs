﻿using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using HoloToolkit.Unity.InputModule.Tests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// maybe later use daydream app button
public class Movement_Handler_Google : MonoBehaviour, IPointerClickHandler {
    private string keyword = "Ball";
    private GameObject[] array = null;
    private GameObject molecule = null;
    // use one button toggled
    public GameObject ON_OFF_Button;

    // Use this for initialization
    public void Start()
    {
        //ON_OFF_Button = GameObject.Find("Movement_Controller");
        array = GameObject.FindGameObjectsWithTag("edmc");
    }

    public void Update()
    {
        if (array.Length == 0 && molecule == null)
        {
            array = GameObject.FindGameObjectsWithTag("edmc");
            molecule = array[0];
        }
    }

	public void OnPointerClick(PointerEventData data) {
		// GvrControllerInput.AppButton
		if(ON_OFF_Button.GetComponentsInChildren<Text>()[0].text == "Movement Mode ON"){ // not able to move
			// remove old Sphere Collider
			MeshRenderer[] objects = molecule.GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer i in objects){
				GameObject atom = i.gameObject;
				Destroy(atom.GetComponent<SphereCollider>());
			}
            // add grabbable script

            //molecule.AddComponent<InputTest>();


            // add Sphere Collider
            molecule.AddComponent<GrabbableSimple>();
			SphereCollider collider_molecule = molecule.AddComponent<SphereCollider>() as SphereCollider;
    		collider_molecule.radius = 10;
			ON_OFF_Button.GetComponentsInChildren<Text>()[0].text = "Movement Mode OFF";
            molecule.AddComponent<HandDraggable>();
            molecule.AddComponent<RotatableObject>();
        }
		else if(ON_OFF_Button.GetComponentsInChildren<Text>()[0].text == "Movement Mode OFF"){
            Destroy(GetComponent<GrabbableSimple>());
            // remove old Sphere Collider
            Destroy(molecule.GetComponent<SphereCollider>());
            Destroy(molecule.GetComponent<HandDraggable>());
            Destroy(molecule.GetComponent<RotatableObject>());
            // add Sphere Collider 
            MeshRenderer[] objects = molecule.GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer i in objects){
				GameObject atom = i.gameObject;
                if (atom.ToString().Contains(keyword)){
					SphereCollider collider_atom = atom.AddComponent<SphereCollider>() as SphereCollider;
				}
			}
			ON_OFF_Button.GetComponentsInChildren<Text>()[0].text = "Movement Mode ON";

        }
    }

    
}
