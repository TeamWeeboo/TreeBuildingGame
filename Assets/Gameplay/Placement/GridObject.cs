using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gameplay.Placement {

	public class GridObject:MonoBehaviour {

		[field: SerializeField] public Vector2Int size { get; private set; }
		[field: SerializeField] public float spacing { get; private set; }
		[SerializeField] Vector3 startPositionOffset;
		[SerializeField] Renderer gridRenderer;

		bool[,] isPlantable;
		Terrain terrain;

		public static GridObject instance;

		public GridElement[,] datas = null;

		bool inited;
		public void Init() {
			terrain=GetComponent<Terrain>();
			if(inited) return;
			inited= true;
			isPlantable=new bool[size.x,size.y];
			float startY = transform.position.y+startPositionOffset.y;
			for(int x = 0;x<size.x;x++) {
				for(int y = 0;y<size.y;y++) {
					Vector3 position = GetGridPosition(new Vector2Int(x,y));
					float terrainHeight = transform.position.y+terrain.SampleHeight(position);
					bool canPlantHere = terrainHeight<=startY+0.25f;
					isPlantable[x,y]=canPlantHere;
				}
			}
		}

		private void Awake() {
			instance=this;
		}

		private void Start() {
			datas=new GridElement[size.x,size.y];
			Init();
			gridRenderer.material.mainTextureScale=new Vector2(1,1);
			UpdateGridRenderer();
		}
		private void FixedUpdate() {
			for(int i = 0;i<datas.GetLength(0);i++)
				for(int j = 0;j<datas.GetLength(1);j++) {
					if(datas[i,j]==null) continue;
					datas[i,j].Update();
				}
		}
		private void OnValidate() {
			UpdateGridRenderer();
		}

		void UpdateGridRenderer() {
			gridRenderer.sharedMaterial.mainTextureScale=new Vector2(size.x+1f,size.y+1f);
			gridRenderer.transform.localScale=new Vector3(spacing*(size.x+1),spacing*(size.y+1),1);
			gridRenderer.transform.localPosition=startPositionOffset+new Vector3(0.5f*spacing*size.x,0.25f,0.5f*spacing*size.y);
		}

		private void OnDrawGizmosSelected() {
			Gizmos.color=Color.green;
			Vector3 startPosition = transform.position+startPositionOffset;
			for(int i = 0;i<size.x;i++) {
				Gizmos.DrawLine(
					startPosition+Vector3.right*i*spacing,
					startPosition+Vector3.right*i*spacing+Vector3.forward*size.y*spacing
				);
			}
			for(int i = 0;i<size.y;i++) {
				Gizmos.DrawLine(
					startPosition+Vector3.forward*i*spacing,
					startPosition+Vector3.forward*i*spacing+Vector3.right*size.x*spacing
				);
			}
		}

		public Vector2Int GetGridIndex(Vector3 position) {
			Vector3 startPosition = transform.position+startPositionOffset;
			Vector3 deltaPosition = position-startPosition;
			return new Vector2Int(Mathf.RoundToInt(deltaPosition.x/spacing),Mathf.RoundToInt(deltaPosition.z/spacing));
		}
		public Vector3 GetGridPosition(Vector2Int index) {
			Vector3 startPosition = transform.position+startPositionOffset;
			Vector3 deltaPosition = new Vector3(index.x,0,index.y)*spacing;
			return startPosition+deltaPosition;
		}

		public GridElement GetElement(Vector2Int index) {
			if(IsIndexOOB(index)) return null;
			if(datas[index.x,index.y]==null) {
				datas[index.x,index.y]=new GridElement();
				datas[index.x,index.y].Init(index);
			}
			return datas[index.x,index.y];
		}

		public GridElement GetElement(Vector3 position) => GetElement(GetGridIndex(position));

		public bool IsIndexOOB(Vector2Int index) => index.x>=size.x||index.y>=size.y||index.x<0||index.y<0||!isPlantable[index.x,index.y];

	}

	public class GridElement {

		public Vector2Int selfIndex { get; private set; }
		public CommandData currentCommand { get; private set; }
		public PlacedObjectController placedObjectController { get; private set; }
		public PlacedObjectController placedTileController { get; private set; }
		public TileData tileData { get; private set; }
		public TreeData treeData { get; private set; }
		public float timeAfterCommand { get; private set; }

		public void Init(Vector2Int index) {
			selfIndex=index;
		}
		public void AddCommand(CommandData newCommand) {
			currentCommand=newCommand;
			timeAfterCommand=0;
            newCommand.UpdateCommand(this);
        }
		public void PlaceObject(TreeData treeData) {
			this.treeData=treeData;
			GameObject prefab = treeData?.prefab;
			if(placedObjectController) Object.Destroy(placedObjectController.gameObject);
			if(prefab==null) placedObjectController=null;
			else {
				GameObject gameObject = Object.Instantiate(prefab,GridObject.instance.GetGridPosition(selfIndex),Quaternion.identity,GridObject.instance.transform);
				placedObjectController=gameObject.GetComponent<PlacedObjectController>();
				placedObjectController.Init(this,prefab);
			}
		}
		public void PlaceTile(TileData tileData) {
			if(tileData==null) return;
			this.tileData=tileData;
			GameObject prefab = tileData?.prefab;
			if(placedTileController) Object.Destroy(placedTileController.gameObject);
			if(prefab==null) placedTileController=null;
			else {
				GameObject gameObject = Object.Instantiate(prefab,GridObject.instance.GetGridPosition(selfIndex),Quaternion.identity,GridObject.instance.transform);
				placedTileController=gameObject.GetComponent<PlacedObjectController>();
				placedTileController.Init(this,prefab);
			}
		}
		public void ClearCommand() {
			currentCommand=null;
			timeAfterCommand=0;
		}

		public void Update() {
			timeAfterCommand+=Time.deltaTime;
			currentCommand?.UpdateCommand(this);
		}

	}

}