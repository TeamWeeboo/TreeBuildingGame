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
			Material[] materialsHere = i.materials;
			for(int j = 0;j<materialsHere.Length;j++) {
				Texture texture = materialsHere[j].mainTexture;
				materialsHere[j]=new Material(materialPrototype);
				materialsHere[j].mainTexture=texture;
				materials.Add(materialsHere[j]);
			}
			i.materials=materialsHere;
			i.shadowCastingMode=UnityEngine.Rendering.ShadowCastingMode.Off;
		}
	}

	public void UpdateColor(Color newColor) {
		foreach(var i in materials) i.color=newColor;
	}

}
