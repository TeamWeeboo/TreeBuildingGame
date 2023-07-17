using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementPreviewController:MonoBehaviour {

	Material materialPrototype;
	List<Material> materials = new List<Material>();

	public void Init(Material material) {
		materialPrototype=material;
		var renderers = GetComponentsInChildren<Renderer>();
		foreach(var i in renderers) {
			materials.Add(i.material);
			i.material=new Material(materialPrototype);
			i.shadowCastingMode=UnityEngine.Rendering.ShadowCastingMode.Off;
		}
	}

	public void UpdateColor(Color newColor) {
		foreach(var i in materials) i.color=newColor;
	}

}
