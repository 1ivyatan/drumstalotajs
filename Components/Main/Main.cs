using Godot;
using System;

public partial class Main : Node
{
	SceneManager sceneManager;
	
	public override void _Ready()
	{
		sceneManager = GetNode<Node>("SceneManager") as SceneManager;
		StartMenu();
	}
	
	void ConnectToSignal(Node node, String signalName, String methodName) {
		node.Connect(signalName, new Callable(this, methodName));
	}
	
	void StartMenu() {
		sceneManager.SetScene("StartMenu", SwitchState.DESTROY);
		
		Node recieverNode = sceneManager.GetCurrentScene().GetChild(0);
		
		ConnectToSignal(recieverNode, "LevelSelect", nameof(LevelSelect));
		ConnectToSignal(recieverNode, "Exit", nameof(Exit));
	}
	
	void LevelSelect() {
		sceneManager.SetScene("LevelSelect", SwitchState.DESTROY);
		
		Node recieverNode = sceneManager.GetCurrentScene().GetChild(0);
		
		ConnectToSignal(recieverNode, "StartMenu", nameof(StartMenu));
		ConnectToSignal(recieverNode, "Battle", nameof(Battle));
	}
	
	void Battle() {
		sceneManager.SetScene("Battle", SwitchState.DESTROY);
		
		Node recieverNode = sceneManager.GetCurrentScene().GetChild(0);
		
		ConnectToSignal(recieverNode, "LevelSelect", nameof(LevelSelect));
	}
	
	void Exit() {
		GetTree().Quit();
	}
}
