﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// maybe later use daydream app button
public class Movement_Handler : MonoBehaviour, IPointerClickHandler {
	private string keyword = "Ball";
	private GameObject molecule;
	// use one button toggled
	public GameObject ON_OFF_Button;

	// Use this for initialization
	public void Start(){
		ON_OFF_Button = GameObject.Find("Movement_Controller");
		molecule = GameObject.FindGameObjectsWithTag("edmc")[0];
	}
	
	public void OnPointerClick(PointerEventData data) {
		if(ON_OFF_Button.GetComponentsInChildren<Text>()[0].text == "Movement Mode ON"){ // not able to move
			// remove old Sphere Collider
			MeshRenderer[] objects = molecule.GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer i in objects){
				GameObject atom = i.gameObject;
				Destroy(atom.GetComponent<SphereCollider>());
			}
        	// add Sphere Collider
			SphereCollider collider_molecule = molecule.AddComponent<SphereCollider>() as SphereCollider;
    		collider_molecule.radius = 10;
			ON_OFF_Button.GetComponentsInChildren<Text>()[0].text = "Movement Mode OFF";
		}
		else if(ON_OFF_Button.GetComponentsInChildren<Text>()[0].text == "Movement Mode OFF"){
			// remove old Sphere Collider
			Destroy(molecule.GetComponent<SphereCollider>());
			// add Sphere Collider 
			MeshRenderer[] objects = molecule.GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer i in objects){
				GameObject atom = i.gameObject;
				if(atom.ToString().Contains(keyword)){
					SphereCollider collider_atom = atom.AddComponent<SphereCollider>() as SphereCollider;
				}
			}
			ON_OFF_Button.GetComponentsInChildren<Text>()[0].text = "Movement Mode ON";
		}
    }
}