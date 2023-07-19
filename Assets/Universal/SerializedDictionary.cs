using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

namespace CULU {

	[System.Serializable]
	public struct SerilizedPair<TKey, TValue> {
		[SerializeField] public TKey key;
		[SerializeField] public TValue value;
	}

	[System.Serializable]
	public class SerializedDictionary<TKey, TValue>:IEnumerable, IEnumerable<KeyValuePair<TKey,TValue>> {
		[SerializeField]
		List<SerilizedPair<TKey,TValue>> data;

		Dictionary<TKey,TValue> _value;
		public Dictionary<TKey,TValue> Value {
			get {
				if(_value==null) {
					_value=new Dictionary<TKey,TValue>();
					foreach(var pair in data) {
						_value.Add(pair.key,pair.value);
					}
				}
				return _value;
			}
		}

		public IEnumerator GetEnumerator() {
			return ((IEnumerable)_value).GetEnumerator();
		}
		IEnumerator<KeyValuePair<TKey,TValue>> IEnumerable<KeyValuePair<TKey,TValue>>.GetEnumerator() {
			return ((IEnumerable<KeyValuePair<TKey,TValue>>)_value).GetEnumerator();
		}
	}

}