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
		else //�� Ŭ���̾��� ����ü ��� �ƴѰܿ� - >Remote (���� ��ü) 
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

        //x,y������ �����ؼ����������� �����̰� �ʹ�
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
			// ���� �߻�ó�� ���� �ʰ� ���忡�� �ñ�
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
				//�浹�� �������� �и��� 
				float dirc = this.gameObject.transform.position.x - collision.gameObject.transform.position.x > 0 ? 1 : -1;
				rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode.Impulse);
				HP--;
			}
        }

		if (HP == 0)
		{
			//�浹 ���̾ �������� �����
			this.gameObject.layer = 10;
			this.transform.position += new Vector3(0, 0, 1.5f);
			this.gameObject.GetComponent<BoxCollider>().enabled = false;
			//�÷��� �ٲٰ�ʹ�
			this.gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.4f);
		}
    }


	//�����Ͱ� �������� ���������� ������ ��
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting == true) //�� ��ü�� ���̳� �ൿ�� �̷�������� photonview-mine
		{//������ ���� ����ü(Remote) �� ���� �־�� ���� 
		 //������ �Լ� - stream. sendNext
			stream.SendNext(this.transform.position);
			stream.SendNext(this.transform.rotation);
		}
		if (stream.IsReading)//������ ���� ����ü(Remote) �϶� photonview-mine false �϶�  
		{
			setPos = (Vector3)stream.ReceiveNext();//��ó�� postion ���� �־�����. position��
			setRot = (Quaternion)stream.ReceiveNext();//rotatation �־����� rotation �� 
		}
	}
}
