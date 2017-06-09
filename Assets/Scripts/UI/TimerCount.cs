using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerCount : MonoBehaviour
{
	public Rect rect;
	public Material material;
	public int textureWidth, textureHeight;
	public GameObject timer;
	
	
	public Material left_time;
	public Material center_time;
	public Material right_time;
	
	void Start()
	{
		Texture texture = center_time.mainTexture;
		textureWidth = texture.width;
		textureHeight = texture.height;
		Debug.Log(textureWidth + "/" + textureHeight);
	}
	
	void Update()
	{
		Vector2 offset, scale;
	
		offset = new Vector2(rect.x / textureWidth, rect.y / textureHeight);
	
		scale = new Vector2(rect.width / textureWidth, rect.height / textureHeight);
	
		material.SetTextureOffset("_MainTex", offset);
	
		material.SetTextureScale("_MainTex", scale);
	}
}
