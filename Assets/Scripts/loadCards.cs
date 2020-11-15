using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Card {

	public string name;
	public string desc;
	public int cost;
	public int amm;

}


public class loadCards : MonoBehaviour {

	public List<string> data = new List<string>();
	public Card newCard = new Card();
	public List<Card> deck = new List<Card>();
	public List<Card> hand = new List<Card>();
	public List<Card> shuffleddeck = new List<Card>();

	public GameObject card1;
	public GameObject card2;
	public GameObject card3;
	public GameObject card4;
	public GameObject card5;



	System.Random rd = new System.Random(); 


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
			newCard = new Card();
			newCard.name = data[i];
			newCard.desc = data[i+1];
				newCard.cost = int.Parse(data[i+2]);

			
			deck.Add(newCard);
		//		Debug.Log("Count: " + deck.Count);
			}
				


		}
	
		List<Card> tempdeck = new List<Card>();

		for (int i = 0; i < deck.Count; i++) {
			tempdeck.Add(deck [i]);
		}
	
		for(int i = 0; i < deck.Count ; i++) {

			int randomIndex = rd.Next(0, tempdeck.Count);
			shuffleddeck.Add (tempdeck [randomIndex]);
			tempdeck.Remove (tempdeck[randomIndex]);
		}

	

		foreach(Card card in shuffleddeck) {
		//	Debug.Log("Card: " + card.name);
		}

		DrawHand ();

	}



	void DrawHand() {

		for (int i = 0; i < 5; i++) {
			hand.Add (shuffleddeck [i]);
			shuffleddeck.Remove (shuffleddeck [i]);
		}




		card1.transform.GetChild(0).GetComponent<Text> ().text = hand [0].name;
		card2.transform.GetChild(0).GetComponent<Text> ().text = hand [1].name;
		card3.transform.GetChild(0).GetComponent<Text> ().text = hand [2].name;
		card4.transform.GetChild(0).GetComponent<Text> ().text = hand [3].name;
		card5.transform.GetChild(0).GetComponent<Text> ().text = hand [4].name;

		card1.transform.GetChild(1).GetComponent<Text> ().text = hand [0].desc;
		card2.transform.GetChild(1).GetComponent<Text> ().text = hand [1].desc;
		card3.transform.GetChild(1).GetComponent<Text> ().text = hand [2].desc;
		card4.transform.GetChild(1).GetComponent<Text> ().text = hand [3].desc;
		card5.transform.GetChild(1).GetComponent<Text> ().text = hand [4].desc;

		card1.transform.GetChild(2).GetComponent<Text> ().text = "" + hand [0].cost;
		card2.transform.GetChild(2).GetComponent<Text> ().text = "" + hand [1].cost;
		card3.transform.GetChild(2).GetComponent<Text> ().text = "" + hand [2].cost;
		card4.transform.GetChild(2).GetComponent<Text> ().text = "" + hand [3].cost;
		card5.transform.GetChild(2).GetComponent<Text> ().text = "" + hand [4].cost;

	}

	public void DrawCard(int index) {
		//Debug.Log ("card: " + shuffleddeck [0].name);
		hand.Add (shuffleddeck [0]);
		shuffleddeck.Remove (shuffleddeck [0]);

		List<Card> temphand = new List<Card> ();
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



		card1.transform.GetChild(0).GetComponent<Text> ().text = hand [0].name;
		card2.transform.GetChild(0).GetComponent<Text> ().text = hand [1].name;
		card3.transform.GetChild(0).GetComponent<Text> ().text = hand [2].name;
		card4.transform.GetChild(0).GetComponent<Text> ().text = hand [3].name;
		card5.transform.GetChild(0).GetComponent<Text> ().text = hand [4].name;

		card1.transform.GetChild(1).GetComponent<Text> ().text = hand [0].desc;
		card2.transform.GetChild(1).GetComponent<Text> ().text = hand [1].desc;
		card3.transform.GetChild(1).GetComponent<Text> ().text = hand [2].desc;
		card4.transform.GetChild(1).GetComponent<Text> ().text = hand [3].desc;
		card5.transform.GetChild(1).GetComponent<Text> ().text = hand [4].desc;

		card1.transform.GetChild(2).GetComponent<Text> ().text = "" + hand [0].cost;
		card2.transform.GetChild(2).GetComponent<Text> ().text = "" + hand [1].cost;
		card3.transform.GetChild(2).GetComponent<Text> ().text = "" + hand [2].cost;
		card4.transform.GetChild(2).GetComponent<Text> ().text = "" + hand [3].cost;
		card5.transform.GetChild(2).GetComponent<Text> ().text = "" + hand [4].cost;


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
		
		if (this.gameObject.GetComponent<gameStats> ().goldFloat >= cost) {
			string newType = type.Trim ();
			if (String.Equals ("Samurai", newType)) {
				GetComponent<unitSpawner> ().Spawn (GetComponent<unitSpawner> ().samurai, cost);
			} else if (String.Equals ("Archer", newType)) {
				GetComponent<unitSpawner> ().Spawn (GetComponent<unitSpawner> ().archer, cost);
			} else if (String.Equals ("Warrior", newType)) {
				GetComponent<unitSpawner> ().Spawn (GetComponent<unitSpawner> ().warrior, cost);
			} else if (String.Equals ("Spearman", newType)) {
				GetComponent<unitSpawner> ().Spawn (GetComponent<unitSpawner> ().spearman, cost);
			} else if (String.Equals ("Viking", newType)) {
				GetComponent<unitSpawner> ().Spawn (GetComponent<unitSpawner> ().viking, cost);
			} else if (String.Equals ("Dragon Knight", newType)) {
				GetComponent<unitSpawner> ().Spawn (GetComponent<unitSpawner> ().dragonknight, cost);
			} else if (String.Equals ("Sumo", newType)) {
				GetComponent<unitSpawner> ().Spawn (GetComponent<unitSpawner> ().sumo, cost);
			} else if (String.Equals ("Sniper", newType)) {
				GetComponent<unitSpawner> ().Spawn (GetComponent<unitSpawner> ().sniper, cost);
			} else if (String.Equals ("Javalin Tosser", newType)) {
				GetComponent<unitSpawner> ().Spawn (GetComponent<unitSpawner> ().javalin, cost);
			} else if (String.Equals ("Giant", newType)) {
				GetComponent<unitSpawner> ().Spawn (GetComponent<unitSpawner> ().giant, cost);
			} 
			else if (String.Equals("Miner", newType)) {
				GetComponent<gameStats> ().miners++;
				this.gameObject.GetComponent<gameStats>().goldFloat -= cost;
			}
			else {
				GetComponent<unitSpawner> ().Spawn (GetComponent<unitSpawner> ().samurai, cost);
			}
		}
	}

}
