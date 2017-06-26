using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chainControll : MonoBehaviour {

	Vector3 goalPos;	//電撃の終着位置
	Vector3 directVec;  //電撃の向かう方向
	public float speed;
	public float offset;
	private float timeCnt;

	// Use this for initialization
	void Start () {
		offset = 0.05f;
		timeCnt = 0.0f;
	}

	public void Set(Vector3 fromPos, Vector3 toPos)
	{
		//必要情報の取得
		Vector3 fromVec = fromPos;
		Vector3 toVec = toPos;

		goalPos = toPos;
		directVec = toPos - fromPos;


		//回転の反映
		transform.position = fromPos;

		Vector3.Normalize(fromVec);
		Vector3.Normalize(toVec);

		float x = toVec.x - fromVec.x;
		float y = toVec.y - fromVec.y;

		float rad = Mathf.Atan2(y, x);
		float deg = rad * Mathf.Rad2Deg;

		float changeRot = transform.rotation.z;
		changeRot += deg;
		this.transform.Rotate(new Vector3(0.0f, 0.0f, changeRot));
	}
	
	// Update is called once per frame
	void Update () {
		timeCnt += Time.deltaTime;
		if(timeCnt > 0.3f)
		{
			Destroy(gameObject);
		}

		//位置更新
		Vector3 pos = transform.position;
		pos += (directVec / 30) * speed;
		transform.position = pos;

		//消える位置 = ゴール位置
		if (transform.position.x < goalPos.x + offset && transform.position.x > goalPos.x - offset)
		{
			if (transform.position.y < goalPos.y + offset && transform.position.y > goalPos.y - offset)
			{
				transform.position = goalPos;
			}
		}
	}
}
