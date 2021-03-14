using System.Collections;
using UnityEngine;

public static class Utils
{
	public static IEnumerator LerpToPosition(Transform transform, Vector3 endPosition, float totalMoveTime, float delay)
	{
		if (delay > 0)
			yield return new WaitForSeconds(delay);

		Vector3 startPos = transform.position;

		float time = 0;

		while (time < totalMoveTime)
		{
			time += Time.deltaTime;
			float lerpT = time / totalMoveTime;

			transform.position = Vector3.Lerp(startPos, endPosition, lerpT);
			yield return null;
		}

		transform.position = endPosition;
	}

	public static IEnumerator LerpToPositionLookAt(Transform transform, Vector3 startPosition, Vector3 endPosition, float totalMoveTime, float delay)
	{
		if (delay > 0)
			yield return new WaitForSeconds(delay);

		float time = 0;

		while (time < totalMoveTime)
		{
			time += Time.deltaTime;
			float lerpT = time / totalMoveTime;

			transform.LookAt(Vector3.Lerp(startPosition, endPosition, lerpT));
			yield return null;
		}

		transform.LookAt(endPosition);
	}
}
