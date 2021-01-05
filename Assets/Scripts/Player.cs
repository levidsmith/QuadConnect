﻿//2020 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject DiscPrefab;
    public List<Disc> discs;
    public string strName;
    public bool isHumanControlled;
    public int iPlayerIndex;

    public GameManager gamemanager;

    void Start() {
        //setupPlayer();
        
    }

    void Update() {
        if (!gamemanager.isGameOver && gamemanager.currentPlayer == this && !gamemanager.board.isDiscMoving()) {
            if (isHumanControlled) {
                handleInput();
            } else {
                handleInputAI();
            }
        }
        
    }

    private void handleInput() {
        //handle input through the OnMouseDown of ColumnSelect
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycasthit;

            Physics.Raycast(ray, out raycasthit);
            if (raycasthit.collider != null) {
                ColumnSelect columnselect = raycasthit.collider.GetComponent<ColumnSelect>();
                if (columnselect != null) {
                    gamemanager.playColumn(columnselect.iCol);
                }
            }
        }

    }

    private void handleInputAI() {
        int iRandCol = Random.Range(0, gamemanager.board.getCols() + 1);
        gamemanager.playColumn(iRandCol);
    }

    public void setupPlayer() {
        int iDiscCount = 21;
        int i;


        for (i = 0; i < iDiscCount; i++) {
            //            Disc disc = Instantiate(DiscPrefab, transform.position + new Vector3(0f, i * 0.2f, 0f), Quaternion.identity).GetComponent<Disc>();
            Disc disc = Instantiate(DiscPrefab, transform.position + new Vector3(0f, i * 0.2f, 0f), Quaternion.Euler(90f, 0f, 0f)).GetComponent<Disc>();
            disc.player = this;
            disc.gamemanager = gamemanager;
            disc.targetPosition = disc.transform.position;
            disc.setMaterial(gamemanager.matDiscs[iPlayerIndex]);
            disc.transform.SetParent(transform);
            discs.Add(disc);
        }

    }
}