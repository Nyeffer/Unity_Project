using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { INTRO, MENU, PLAY, WON}

public class StateManager : MonoBehaviour {

	// Use this for initialization

	public GameObject[] m_gameState;

	private GameStates m_activeState;

	private int m_numStates;

	void Start() {
		m_numStates = m_gameState.Length;
		for(int i = 0; i < m_numStates; i++) {
			m_gameState[i].SetActive(false);
		}
		m_activeState = GameStates.INTRO;
		m_gameState[(int)m_activeState].SetActive(true);
	}

	public void PlayGame() {
		Debug.Log("Lol");
		m_gameState[(int)m_activeState].SetActive(false);
		m_activeState = GameStates.PLAY;
		m_gameState[(int)m_activeState].SetActive(true);
	}

	public void QuitGame() {
	}

	public void ChangeState(GameStates newState) {
		m_gameState[(int)m_activeState].SetActive(false);
		m_activeState = newState;
		m_gameState[(int)m_activeState].SetActive(true);
	}
}
