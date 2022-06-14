using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
	Animator anim;
	[SerializeField] private GameObject player;
	bool detectado = false;
	float distDetectado = 6.0f;
	float distAtaque = 2.0f;
	float velMovimiento = 3f;
	float velRot = 12.0f;
	bool ataque = false;
	
	void Start()
	{
		//anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 posicion = transform.position;
		Vector3 posicionYo = player.transform.position;
		float distancia = Vector3.Distance(posicionYo, posicion);
		if (distancia <= distDetectado && distancia > distAtaque)
		{
			//si detectado es false se convierte en true de forma permanente
			if (!detectado)
			{
				detectado = true;
				//anim.SetBool("detectado", detectado);
			}
			//si la distancia es mayor que la distancia de ataque ataque será falso
			if (ataque)
			{
				ataque = false;
				//anim.SetBool("ataque", ataque);
			}
			//si la distancia es menor que la distancia de ataque ataque será true
		}
		else if (distancia <= distAtaque)
		{
			ataque = true;
			//anim.SetBool("ataque", ataque);
		}
		//una vez que el enemigo ha localizado al player lo persigue
		if (detectado)
		{
			Seguir(player);
		}
	}

	void Seguir(GameObject target)
	{
		Vector3 posicion = transform.position;
		Vector3 posicionTarget = target.transform.position;
		Vector3 direccion = posicionTarget - posicion;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direccion, Vector3.up), velRot * Time.deltaTime);
		transform.position += direccion.normalized * velMovimiento * Time.deltaTime;
	}

	protected void SeguirNavMesh(GameObject target)
	{
		GetComponent<NavMeshAgent>().destination = target.transform.position;
		//poca velocidad porque la animación ya genera movimiento
		GetComponent<NavMeshAgent>().speed = 0.1f;
	}

}