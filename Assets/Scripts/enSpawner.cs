using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Card1 {

	public string name;
	public string desc;
	public int cost;
	public int amm;

}


public class enSpawner : MonoBehaviour {

	public List<string> data = new List<string>();
	public Card1 newCard = new Card1();
	public List<Card1> deck = new List<Card1>();
	public List<Card1> hand = new List<Card1>();
	public List<Card1> shuffleddeck = new List<Card1>();

	System.Random rd = new System.Random(); 
	System.Random cardChoice = new System.Random(); 


	public float timer = 0f;
	public float spawnTime = 5f;


	public int samuraiCost = 100;
	public int archerCost = 70;
	public int warriorCost = 100;
	public int spearmanCost = 70;
	public int vikingCost = 100;
	public int sniperCost = 70;
	public int dragonknightCost = 100;
	public int sumoCost = 70;
	public int javalinCost = 100;
	public int giantcost = 200;

	public GameObject samurai;
	public GameObject archer;
	public GameObject warrior;
	public GameObject spearman;
	public GameObject viking;
	public GameObject sniper;
	public GameObject dragonknight;
	public GameObject sumo;
	public GameObject javalin;
	public GameObject giant;

	public GameObject spawn;

	// Use this for initialization
	void Start () {

		TextAsset cardData = Resources.Load<TextAsset>("cards");

		string[] linesFromFile = cardData.text.Split('\n');

		foreach (string line in linesFromFile)
		{
			data.Add(line);

		}

		for(int i = 0; i < data.Count - 3; i+=4) {
			//	Debug.Log ("Data: " + data[i]);

			int x = int.Parse(data[i+3]);
			//	Debug.Log ("X: " + x);

			for(int j = 0; j < x; j++) {
				newCard = new Card1();
				newCard.name = data[i];
				newCard.desc = data[i+1];
				newCard.cost = int.Parse(data[i+2]);


				deck.Add(newCard);
				//		Debug.Log("Count: " + deck.Count);
			}



		}

		List<Card1> tempdeck = new List<Card1>();

		for (int i = 0; i < deck.Count; i++) {
			tempdeck.Add(deck [i]);
		}

		for(int i = 0; i < deck.Count ; i++) {

			int randomIndex = rd.Next(0, tempdeck.Count);
			shuffleddeck.Add (tempdeck [randomIndex]);
			tempdeck.Remove (tempdeck[randomIndex]);
		}



		foreach(Card1 card in shuffleddeck) {
			//	Debug.Log("Card: " + card.name);
		}

		DrawHand ();

	}


	void Update() {


		timer += Time.deltaTime;


		if (timer >= spawnTime) {

			int cardPick = cardChoice.Next(0, 4);

			//Debug.Log ("Choice: " + cardPick);

			if (cardPick == 0) {
				if (GetComponent<gameStats> ().enGoldFloat >= hand [0].cost) {
					Card1Used ();

				}
			}
			if (cardPick == 1) {
				if (GetComponent<gameStats> ().enGoldFloat >= hand [1].cost) {
					Card2Used ();
				//	GetComponent<gameStats> ().enGoldFloat -= hand [1].cost;
				}
			}
			if (cardPick == 2) {
				if (GetComponent<gameStats> ().enGoldFloat >= hand [2].cost) {
					Card3Used ();
				//	GetComponent<gameStats> ().enGoldFloat -= hand [2].cost;
				}
			}
			if (cardPick == 3) {
				if (GetComponent<gameStats> ().enGoldFloat >= hand [3].cost) {
					Card4Used ();
				//	GetComponent<gameStats> ().enGoldFloat -= hand [3].cost;
				}
			}
			if (cardPick == 4) {
				if (GetComponent<gameStats> ().enGoldFloat >= hand [4].cost) {
					Card5Used ();
				//	GetComponent<gameStats> ().enGoldFloat -= hand [4].cost;

				}
			}

			timer = 0f;
		}

	}


	void DrawHand() {

		for (int i = 0; i < 5; i++) {
			hand.Add (shuffleddeck [i]);
			shuffleddeck.Remove (shuffleddeck [i]);
		}



	}

	public void DrawCard(int index) {
		//Debug.Log ("card: " + shuffleddeck [0].name);
		hand.Add (shuffleddeck [0]);
		shuffleddeck.Remove (shuffleddeck [0]);

		List<Card1> temphand = new List<Card1> ();
		for (int i = 0; i < 5; i++) {
			temphand.Add (hand [i]);
		}
		hand.Clear ();

		for (int i = 0; i < 5; i++) {
			if (i != index) {
				hand.Add (temphand [i]);
			} else {
				hand.Add (shuffleddeck [0]);
			}
		}



		foreach (Card1 card in hand) {

		//	Debug.Log ("Name: " + card.name + " Cost: " + card.cost);


		}

	//	Debug.Log ("-------");


	}


	public void Card1Used() {
		DoCardAbility (hand [0].name, hand[0].cost);
		DrawCard (0);

	}
	public void Card2Used() {
		DoCardAbility (hand [1].name, hand[1].cost);
		DrawCard (1);

	}

	public void Card3Used() {
		DoCardAbility (hand [2].name, hand[2].cost);
		DrawCard (2);

	}

	public void Card4Used() {
		DoCardAbility (hand [3].name, hand[3].cost);
		DrawCard (3);

	}

	public void Card5Used() {
		DoCardAbility (hand [4].name, hand[4].cost);
		DrawCard (4);

	}



	public void DoCardAbility(string type, int cost) {
		if (this.gameObject.GetComponent<gameStats> ().enGoldFloat >= cost) {

			string newType = type.Trim ();
			if (String.Equals ("Samurai", newType)) {
				Spawn (samurai, cost);
			} else if (String.Equals ("Archer", newType)) {
				Spawn (archer, cost);
			} else if (String.Equals ("Warrior", newType)) {
				Spawn (warrior, cost);
			} else if (String.Equals ("Spearman", newType)) {
				Spawn (spearman, cost);
			} else if (String.Equals ("Viking", newType)) {
				Spawn (viking, cost);
			} else if (String.Equals ("Dragon Knight", newType)) {
				Spawn (dragonknight, cost);
			} else if (String.Equals ("Sumo", newType)) {
				Spawn (sumo, cost);
			} else if (String.Equals ("Sniper", newType)) {
				Spawn (sniper, cost);
			} else if (String.Equals ("Javalin Tosser", newType)) {
				Spawn (javalin, cost);
			} else if (String.Equals ("Giant", newType)) {
				Spawn (giant, cost);
			} 
			else if (String.Equals("Miner", newType)) {
				GetComponent<gameStats> ().enMiners++;
				this.gameObject.GetComponent<gameStats>().enGoldFloat -= cost;
			}
			else {
				Spawn (samurai, cost);
			}
		
	    
		}

	}

	public void Spawn(GameObject type, int cost) {
		this.gameObject.GetComponent<gameStats>().enGoldFloat -= cost;
		GameObject Soldier = Instantiate(type, spawn.transform.position, spawn.transform.rotation) as GameObject;

		GetComponent<gameStats> ().enGoldFloat -= cost;

		GetComponent<gameStats>().enemies.Enqueue(Soldier);


		float tempWorth = cost * 0.80f;

		Soldier.GetComponent<unitAI>().worth = (int)tempWorth;
		GetComponent<gameStats>().enemies.Enqueue(Soldier);



	}

}
