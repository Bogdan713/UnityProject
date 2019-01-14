using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    public Transform tree;
    public int treeCount;

    private Vector2 mapSize = new Vector2(50,50);

	void Start () {
        GenerateMap();
    }

    void GenerateMap() {
        treeCount = 333;
        for (int i = 0; i < treeCount; i++) {
            Instantiate(tree, new Vector2(Random.Range(0, mapSize.x), Random.Range(0, mapSize.y)), tree.rotation);
        }
    }
	
	void Update () {
		
	}
}
