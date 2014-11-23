﻿using UnityEngine;
using System.Collections;

public class GUIItem : MonoBehaviour {

	GameObject m_PlayerEntity; //will initialize to the actual player in InitThis.
	GSP.Char.Character m_PlayerCharacterScript;
	GSP.GUIMapEvents m_GUIMapEventsScript;
	private GSP.DieInput m_DieScript;	//Access the sigleton Die and its functions

	string m_headerString;
	string m_itemString;
	string m_resourceString;
	
	int m_playerWeight = -1;
	
	int m_mainStartX = -1;
	int m_mainStartY = -1;
	int m_mainWidth = -1;
	int m_mainHeight = -1;
	
	bool m_selectionMadeAddRemove = false;	//for internal use, determines if player w to add Ally or not
	bool m_isActionRunning = false;
	
	// Use this for initialization
	void Start () {
		
	}	//end Start()
	
	public void InitGUIAlly(GameObject p_PlayerEntity, int p_startX, int p_startY, int p_startWidth, int p_startHeight, string p_itemStr, string p_resourceStr )
	{
		m_PlayerEntity = p_PlayerEntity;
		m_PlayerCharacterScript = m_PlayerEntity.GetComponent<GSP.Char.Character>();
		m_GUIMapEventsScript = GameObject.FindGameObjectWithTag("GUIMapEventSpriteTag").GetComponent<GSP.GUIMapEvents>();
		
		m_isActionRunning = true;
		
		//GUIMapEvents values transferred over
		m_mainStartX = p_startX;
		m_mainStartY = p_startY;
		m_mainWidth = p_startWidth;
		m_mainHeight = p_startHeight;

		m_itemString = p_itemStr;
		m_resourceString = p_resourceStr;
	}
	
	private void getPlayerAllyValues()
	{
		m_playerWeight = m_PlayerCharacterScript.ResourceWeight;
	}	//end private void getPlayerAllyValues()
	
	void OnGUI()
	{
		GUI.backgroundColor = Color.red;
		if( m_isActionRunning == true )
		{
			if(m_selectionMadeAddRemove == false)
			{
				ConfigHeader ();
				ConfigAddButton ();
				ConfigCancelButton ();
			}
			else
			{
				ConfigHeader();
				ConfigDoneButton();
			}
		}
	}	//end void OnGUI()
	
	private void ConfigHeader()
	{
		if( m_selectionMadeAddRemove == false )
		{
			if( m_resourceString == null)
			{
				m_resourceString = "";
			}
			m_headerString = "Would You Like\nto Add " +m_itemString +" " +m_resourceString +"?";
		}
		
		int headWdth = m_mainWidth - 2;
		int headHght = m_mainHeight / 6;
		int headX = m_mainStartX + ((m_mainWidth -headWdth) /2);
		int headY = m_mainStartY + (headHght*2);
		
		GUI.Box(new Rect(headX, headY, headWdth, headHght*2), m_headerString);
	}	//end private void ConfigHeader()
	
	private void ConfigAddButton()
	{
		int newWdth = m_mainWidth / 5;
		int newHght = m_mainHeight / 6;
		int newX = m_mainStartX + (newWdth*1);
		int newY = m_mainStartY + (newHght*4);
		
		if( GUI.Button(new Rect(newX, newY, newWdth, newHght*2), "Yes") )
		{
			//TODO:GET RESOURCE RESULT FROM MAPEVENT
			//function() will get teh result of the map Event.
			m_itemString = "BLANK"; //function here;
			m_headerString = "Item Added.\n" +m_itemString;
			
			m_selectionMadeAddRemove = true;
		}
	}	// end 	private void ConfigAddButton()
	
	private void ConfigCancelButton()
	{
		int newWdth = m_mainWidth / 5;
		int newHght = m_mainHeight / 6;
		int newX = m_mainStartX + (newWdth*3);
		int newY = m_mainStartY + (newHght*4);
		
		if( GUI.Button(new Rect(newX, newY, newWdth, newHght*2), "No") )
		{
			m_headerString = "Ally was not Added";
			m_selectionMadeAddRemove = true;
		}
	}	//end 	private void ConfigCancelButton()
	
	
	private void ConfigDoneButton()
	{
		//done button
		int doneWidth = m_mainWidth/2;
		int doneHeight = m_mainHeight / 8;
		int doneStartX = m_mainStartX +(m_mainWidth -doneWidth) /2;
		int doneStartY = m_mainStartY +(doneHeight *7);
		GUI.backgroundColor = Color.red;
		
		if ( GUI.Button (new Rect( doneStartX, doneStartY, doneWidth, doneHeight), "DONE") )
		{
			m_isActionRunning = false;
			m_selectionMadeAddRemove = false;
			//once nothing is happening, program returns to Controller's End Turn State
			m_GUIMapEventsScript.MapeEventDone();
		}
	}	//end private void ConfigDoneButton()
}	//end public class GUIItem
