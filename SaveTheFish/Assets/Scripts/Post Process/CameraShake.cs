using UnityEngine;
using System.Collections;

namespace PostProcess
{
	public class CameraShake : MonoBehaviour
	{
		[SerializeField]
		private Transform camTransform;

		[SerializeField]
		private float shakeAmount = 0.7f;
		[SerializeField]
		private float decreaseFactor = 1.0f;

		private Vector3 originalPos;
		private float shakeDuration = 0;
		private float currentShakeAmount;

		void Awake()
		{
			if (camTransform == null)
			{
				camTransform = GetComponent(typeof(Transform)) as Transform;
			}
		}

		void OnEnable()
		{
			originalPos = camTransform.localPosition;
		}

		public void StartShake(float duration)
		{
			shakeDuration = duration;
			currentShakeAmount = shakeAmount;
		}

		void Update()
		{
			if (shakeDuration > 0)
			{
				camTransform.localPosition = originalPos + Random.insideUnitSphere * currentShakeAmount;

				shakeDuration -= Time.deltaTime;
				currentShakeAmount = decreaseFactor * currentShakeAmount;
			}
			else
			{
				shakeDuration = 0f;
				camTransform.localPosition = originalPos;
			}
		}

#if UNITY_EDITOR
		[ContextMenu("Test shake")]
		public void Test()
		{
			StartShake(2f);
		}
#endif
	}
}