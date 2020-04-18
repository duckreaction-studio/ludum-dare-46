using System;
using UnityEngine;


namespace Helper
{
	public class SingletonSaved<T> : MonoBehaviour where T : MonoBehaviour
	{
		protected static bool isDestroyed = false;
		protected static T instance { get; set; } = null;

		public static T Instance
		{
			get
			{
				if (instance == null && !isDestroyed)
				{
					Debug.LogWarning("[SingletonSaved] Auto instanciate " + GetName());
					GameObject go = new GameObject(GetName(), typeof(T));
					instance = go.GetComponent<T>();
				}
				return instance;
			}
		}

		public bool dontDestroyOnLoad = false;

		public virtual void Awake()
		{
			if (instance != null && instance != this)
			{
				Debug.LogWarning("[SingletonSaved] Another instance of " + typeof(T).ToString() + " aleardy exists, so we destroy it !");
				Destroy(this.gameObject);
				return;
			}
			else
			{
				instance = this.gameObject.GetComponent<T>();
				isDestroyed = false;
			}
			if (dontDestroyOnLoad)
				DontDestroyOnLoad(this.gameObject);
		}

		public static string GetName()
		{
			return "(singleton saved) " + typeof(T).ToString();
		}

		protected virtual void OnDestroy()
		{
			if (instance == this)
				isDestroyed = true;
		}
	}
}
