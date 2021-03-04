using System;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ImageWithRoundedCorners : MonoBehaviour {
	private static readonly int Props = Shader.PropertyToID("_WidthHeightRadius");

	public Material material;
	public float radius;
	public Vector3 vector3;

	void OnRectTransformDimensionsChange(){
		Refresh();
	}
	
	private void OnValidate(){
		Refresh();
	}

	private void Refresh(){
		var rect = ((RectTransform) transform).rect;
		//material.SetVector(Props, new Vector4(rect.width, rect.height, radius, 0));
		material.SetVector(Props, new Vector4(vector3.x, vector3.y, vector3.z, 0));
	}
}
