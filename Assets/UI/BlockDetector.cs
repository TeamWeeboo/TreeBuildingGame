using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockDetector:MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public static bool isBlocked {
		get {
			if(!instance||instance.gameObject.activeInHierarchy==false) return false;
			return !isOver||clickedOutside;
		}
	}
	static bool isOver;
	static bool clickedOutside;

	static BlockDetector instance;
	void Start() {
		if(instance) throw new System.Exception("Duplicate Instance");
		instance=this;
	}

	void Update() {
		if(!isOver&&Input.GetMouseButtonDown(0)) clickedOutside=true;
		if(Input.GetMouseButtonUp(0)) clickedOutside=false;
	}

	public void OnPointerEnter(PointerEventData eventData) => isOver=true;
	public void OnPointerExit(PointerEventData eventData) => isOver=false;
}
