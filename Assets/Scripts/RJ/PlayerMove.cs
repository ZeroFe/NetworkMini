using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class PlayerMove : MonoBehaviour
{
	public float rotateSpeed = 5f;
	public float speed = 5f;
	public GameObject[] bullet = new GameObject[2];
	float h;
	float v;
	bool ismove;
	public Transform firePos_Right;
	public Transform firePos_Left;
	public Transform firePos;

	public float power;

	private Rigidbody rigid;

    private void Awake()
    {
		rigid = GetComponent<Rigidbody>();
	}

    void Start()
	{
	}

	void Update()
	{
		Area();
		Fire();
	}

    private void FixedUpdate()
    {
		Move();
	}

    void Move()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		transform.rotation = Quaternion.Euler(0, h > 0 ? 0 : 180, 0);

		ismove = true;
		Vector3 dir = new Vector3(h, v, 0);
		dir.Normalize();
		rigid.AddForce(dir * power, ForceMode.Acceleration);
	}


	void Area()
	{
		ismove = true;
		var curPos = transform.position;
		curPos += new Vector3(h, v, 0) * speed * Time.deltaTime;

		//x,y범위를 지정해서범위에서만 움직이고 싶다
		curPos.x = Clamp(curPos.x, -16, 16);
		curPos.y = Clamp(curPos.y, -7, 6);

		transform.position = curPos;
	}

	void Fire()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Instantiate(bullet[0], firePos.position, transform.rotation);
        }
		if (Input.GetKeyDown(KeyCode.P))
		{
			Instantiate(bullet[1], firePos.position, transform.rotation);
		}
	}

	private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
			if (HPManager.instance.HP >= 1)
			{
				//충돌한 방향으로 밀린다 
				float dirc = this.gameObject.transform.position.x - collision.gameObject.transform.position.x > 0 ? 1 : -1;
				rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode.Impulse);
				HPManager.instance.HP--;
			}

        }
				if (HPManager.instance.HP == 0)
				{
					//충돌 레이어를 유령으로 만들고
					this.gameObject.layer = 10;
					this.transform.position += new Vector3(0, 0, 1.5f);
				    this.gameObject.GetComponent<BoxCollider>().enabled = false;
					//컬러를 바꾸고싶다
					this.gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.4f);
				}
    }
}
