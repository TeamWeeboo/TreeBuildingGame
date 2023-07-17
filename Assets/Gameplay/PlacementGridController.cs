using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlacementGrid:MonoBehaviour {

	[SerializeField] int size;
	[SerializeField] float spacing;
	[SerializeField] Vector3 startPositionOffset;

	public static PlacementGrid instance;

	public PlacementGridElementData[,] datas = null;

	private void Start() {
		instance=this;
		datas=new PlacementGridElementData[size,size];
	}

	private void OnDrawGizmosSelected() {
		Gizmos.color=Color.green;
		Vector3 startPosition = transform.position+startPositionOffset;
		for(int i = 0;i<size;i++) {
			Gizmos.DrawLine(
				startPosition+Vector3.right*i*spacing,
				startPosition+Vector3.right*i*spacing+Vector3.forward*size*spacing
			);
			Gizmos.DrawLine(
				startPosition+Vector3.forward*i*spacing,
				startPosition+Vector3.forward*i*spacing+Vector3.right*size*spacing
			);
		}
	}

	public Vector2Int GetGridIndex(Vector3 position) {
		Vector3 startPosition = transform.position+startPositionOffset;
		Vector3 deltaPosition = position-startPosition;
		return new Vector2Int((int)(deltaPosition.x/spacing),(int)(deltaPosition.x/spacing));
	}
	public Vector3 GetGridPosition(Vector2Int index) {
		Vector3 startPosition = transform.position+startPositionOffset;
		Vector3 deltaPosition = (Vector2)index*spacing;
		return startPosition+deltaPosition;
	}

	public PlacementGridElementData GetElement(Vector3 position) {
		Vector2Int index = GetGridIndex(position);
		if(datas[index.x,index.y]==null) datas[index.x,index.y]=new PlacementGridElementData();
		return datas[index.x,index.y];
	}

}

public class PlacementGridElementData {

	Vector2Int selfIndex;
	PlacementOptionData currentCommand;
	GameObject placedObject;


}