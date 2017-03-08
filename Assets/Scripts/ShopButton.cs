using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopButton : MonoBehaviour {
	public Texture button_start;
	public Texture button_shop;
	public Texture button_exit;
	
	void OnGUI(){
	//显示标题
	
	//GUI.Label(new Rect(350,50,Screen.width,150),"MonsterLand");
	GUI.backgroundColor=Color.red;
	
	if (GUI.Button (new Rect (400, 280, 180,40), button_start)) {
		SceneManager.LoadScene("Td");
	}
	
	//开始游戏按钮
	if (GUI.Button (new Rect (400, 340, 180, 40), button_shop)) {
		SceneManager.LoadScene("Unity_MySQL");
	}
	
	if (GUI.Button (new Rect (400, 400, 180, 40), button_exit)) {
		
	}
	
	}
}


