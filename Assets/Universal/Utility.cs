using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CULU;

namespace CULU {

	public delegate void Void();
	public delegate void EventHandler(object sender);

	public struct LoadTextEventArgs {
		public string language;
		public LoadTextEventArgs(string _language) {
			language=_language;
		}
	}

	public static class Utility {
		//����NonAlloc����ʱʹ�õĻ���
		public static RaycastHit2D[] raycastBuffer = new RaycastHit2D[100];
		public static Collider2D[] colliderBuffer = new Collider2D[100];

		//����


		//�жϳ����Ƿ��ڵ��Ի���������
		public static bool debug {
			get {
#if UNITY_EDITOR
				return true;
#else
			return false;
#endif
			}
		}

		//��ʸ�����и����˷�
		public static Vector2 Product(Vector2 a,Vector2 b) {
			return new Vector2(a.x*b.x-a.y*b.y,a.x*b.y+a.y*b.x);
		}

		//����Ѱ��δ����Ķ���
		public static GameObject Find(string name) {
			GameObject result = GameObject.Find(name);
			if(result!=null) return result;
			var all = Resources.FindObjectsOfTypeAll<GameObject>();
			foreach(var i in all) {
				if(i.name==name) {
					result=i;
					break;
				}
			}
			return result;
		}

		public static T FindComponent<T>() where T : MonoBehaviour {
			var all = Resources.FindObjectsOfTypeAll<GameObject>();
			foreach(var i in all) {
				T result = i.GetComponent<T>();
				if(result) return result;
			}
			return null;
		}

		public static void SetActive(GameObject obj,bool value) {
			obj.SetActive(value);
		}

		public static readonly System.Reflection.BindingFlags allMethods =
			System.Reflection.BindingFlags.NonPublic|
			System.Reflection.BindingFlags.Public|
			System.Reflection.BindingFlags.Static|
			System.Reflection.BindingFlags.Instance;

		/// <summary>
		/// ���һ�������Ƿ�����, ����ͬ��������Ӧʹ�ô˷���
		/// </summary>
		/// <param name="testedObject">�����Ķ���</param>
		/// <param name="testedMethod">�����ķ�����</param>
		/// <param name="baseType">�����ķ���������Ļ���</param>
		/// <returns></returns>
		public static bool MethodOverriden(object testedObject,string testedMethod,System.Type baseType) {
			var foundMethod = testedObject.GetType().GetMethod(testedMethod,allMethods);
			if(foundMethod==null) Debug.Log("METHOD NOT FOUND");
			if(foundMethod==null) return false;
			return foundMethod.DeclaringType!=baseType;
		}

		//��ѧ����
		public static bool Chance(float chance) {
			return Random.Range(0f,1f)<chance;
		}
		public static void Swap<T>(ref T a,ref T b) {
			T temp = a;
			a=b;
			b=temp;
		}

		public static float Cross(Vector2 a,Vector2 b) {
			return a.x*b.y-a.y*b.x;
		}

		public static float Taxicab(Vector2 a,Vector2 b) {
			return Mathf.Abs(a.x-b.x)+Mathf.Abs(a.y-b.y);
		}
		public static int Taxicab(Vector2Int a,Vector2Int b) {
			return Mathf.Abs(a.x-b.x)+Mathf.Abs(a.y-b.y);
		}

		//���߼��
		public static bool IfLineOfSight(Vector2 position,Vector2 target) {
			bool result = true;
			Vector2 direction = target-position;
			int cnt = Physics2D.CircleCastNonAlloc(position,0.2f,direction,raycastBuffer,direction.magnitude);
			for(int i = 0;i<cnt;i++)
				if(!raycastBuffer[i].collider.isTrigger&&raycastBuffer[i].collider.tag=="Wall") result=false;
			return result;
		}


		//����
		public static void GizmosDrawSquare(Vector2 start,Vector2 end,Color color) {
			Color col = Gizmos.color;
			Gizmos.color=color;
			Gizmos.DrawWireCube((start+end)/2,end-start);
			Gizmos.color=col;
		}

		//��ֹ֡
		const float maxWaitTime = 0.02f;
		static float timeWaitedThisFrame;
		public static void WaitSync(float time) {
			if(timeWaitedThisFrame>=maxWaitTime) return;
			timeWaitedThisFrame+=time;
			if(timeWaitedThisFrame>maxWaitTime) time-=timeWaitedThisFrame-maxWaitTime;
			float timeTarget = Time.realtimeSinceStartup+time;
			while(Time.realtimeSinceStartup<timeTarget) ;
		}

		public static void WaitSyncNoLimit(float time) {
			float timeTarget = Time.realtimeSinceStartup+time;
			while(Time.realtimeSinceStartup<timeTarget) ;
		}

		public static string language { get; private set; }

		static Vector3[] cornerBuffer = new Vector3[4];
		public static Bounds GetRectBounds(RectTransform rectTransform) {
			rectTransform.GetWorldCorners(cornerBuffer);
			Vector2 min = cornerBuffer[0];
			Vector2 max = cornerBuffer[2];
			return new Bounds((min+max)*0.5f,max-min);
		}

	}
}