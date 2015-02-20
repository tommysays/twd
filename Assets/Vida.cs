using UnityEngine;
using System.Collections;

public class Vida : MonoBehaviour {
	public int curHP = 1, maxHP = 1;
	public int damage = 1;
	public Owner owner;
	public enum Owner{
		FRIENDLY, ENEMY, NEUTRAL
	}
	void Start(){
		curHP = maxHP;
	}
	void OnTriggerEnter(Collider other){
		if (other.tag == "Boundary" || other.tag == "Tower")
			return;
		print ("cur = " + tag + ", other = " + other.tag);
		GameObject curObj = GetComponent<Collider>().gameObject;
		GameObject otherObj = other.gameObject;
		Vida otherVida = otherObj.GetComponent<Vida>();
		/*
		if (otherVida == null){
			print ("null other");
			print (otherObj.tag);
			return;
		}
		*/
		if (owner == otherVida.owner){
			print ("same owner");
			return;
		}
		print ("damaging things");
		bool otherDestroyed = DamageObject(otherObj, damage);
		bool thisDestroyed = DamageObject(curObj, otherVida.damage);
		/* 
		 * Code for spawning explosions.
		if (otherDestroyed && other.tag == "Enemy"){
			GameObject exp = Instantiate(other.GetComponent<EnemyController>().explosion, other.transform.position, Quaternion.identity) as GameObject;
			Destroy(exp, 5);
		} else if (thisDestroyed && this.tag == "Enemy"){
			GameObject exp = Instantiate(GetComponent<EnemyController>().explosion, transform.position, Quaternion.identity) as GameObject;
			Destroy(exp, 5);
			gameController.increaseScore(100);
		}
		 */
	}

	bool DamageObject(GameObject obj, int damage){
		Vida vida = obj.GetComponent<Vida>();
		vida.curHP -= damage;
		if (vida.curHP < 0)
			vida.curHP = 0;
		if (vida.curHP == 0){
			Destroy (obj);
			return true;
		}
		return false;
	}
}
