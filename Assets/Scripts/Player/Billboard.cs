using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{

	[SerializeField] private Camera cam;

	void Update()
	{
		transform.LookAt(cam.transform);
	}
}
