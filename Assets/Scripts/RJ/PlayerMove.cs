using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Mathf;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMove : MonoBehaviourPun, IPunObservable
{
	public float rotateSpeed = 5f;
	public float speed = 5f;
	
	[Header("Fire")]
	public GameObject[] bullet = new GameObject[2];
	public Transform firePos;
	public float power;
	private int currWeapon = 0;

	[Header("HP")]
	public Slider hpBar;
	int hp;
	public int maxHP = 4;

	// For Photon Network
	private Vector3 setPos;
	private Quaternion setRot;

	private Rigidbody rigid;

	public int HP
	{
		get { return hp; }
		set
		{
			hp = value;
			hpBar.value = hp;
		}
	}

	private void Awake()
    {
		rigid = GetComponent<Rigidbody>();
	}

    void Start()
	{
		hpBar.maxValue = maxHP;
		HP = maxHP;
	}

	void Update()
	{
		PosInterpolation();
		Area();
		Shoot();
	}

    private void FixedUpdate()
    {
		Move();
	}

    void Move()
	{
		if (photonView.IsMine)
		{
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");

            if (h != 0)
            {
				transform.rotation = Quaternion.Euler(0, h > 0 ? 0 : 180, 0);
			}

			Vector3 dir = new Vector3(h, v, 0);
			dir.Normalize();
			rigid.AddForce(dir * power, ForceMode.Acceleration);
		}
		else //내 클라이언의 내객체 제어가 아닌겨우 - >Remote (상대방 객체) 
		{
			transform.rotation = setRot;
		}
	}

	void PosInterpolation()
    {
        if (!photonView.IsMine)
        {
			transform.position = Vector3.Lerp(transform.position, setPos, Time.deltaTime * 10);
		}
	}


	void Area()
	{
		var curPos = transform.position;

        //x,y범위를 지정해서범위에서만 움직이고 싶다
        curPos.x = Clamp(curPos.x, -16, 16);
		curPos.y = Clamp(curPos.y, -7, 6);

		transform.position = curPos;
	}

	private void ChangeWeapon(int weaponNumber)
    {

    }

	private void Shoot()
	{
		if (Input.GetKeyDown(KeyCode.Space) && photonView.IsMine)
		{
			// 직접 발사처리 하지 않고 방장에게 맡김
			photonView.RPC(nameof(Fire), RpcTarget.AllBuffered, null);
		}
	}

	[PunRPC]
	void Fire()
    {
		Instantiate(bullet[currWeapon], firePos.position, transform.rotation);
	}

	private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
			if (HP >= 1)
			{
				//충돌한 방향으로 밀린다 
				float dirc = this.gameObject.transform.position.x - collision.gameObject.transform.position.x > 0 ? 1 : -1;
				rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode.Impulse);
				HP--;
			}
        }

		if (HP == 0)
		{
			//충돌 레이어를 유령으로 만들고
			this.gameObject.layer = 10;
			this.transform.position += new Vector3(0, 0, 1.5f);
			this.gameObject.GetComponent<BoxCollider>().enabled = false;
			//컬러를 바꾸고싶다
			this.gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.4f);
		}
    }


	//데이터가 차곡차곡 순차적으로 오고가고 함
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting == true) //내 객체의 값이나 행동이 이루어졌을대 photonview-mine
		{//상대방이 보는 내객체(Remote) 에 값을 주어야 겠지 
		 //보내는 함수 - stream. sendNext
			stream.SendNext(this.transform.position);
			stream.SendNext(this.transform.rotation);
		}
		if (stream.IsReading)//상대방이 보는 내객체(Remote) 일때 photonview-mine false 일때  
		{
			setPos = (Vector3)stream.ReceiveNext();//맨처음 postion 으로 넣었으니. position값
			setRot = (Quaternion)stream.ReceiveNext();//rotatation 넣었으니 rotation 값 
		}
	}
}
