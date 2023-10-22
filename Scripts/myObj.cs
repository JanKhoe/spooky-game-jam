using Godot;
using System;

public partial class myObj : RigidBody2D
{

	protected Vector2 startingPos = new Vector2(0,0);
	protected float Speed = 400.0f;
	

	protected void goToStart(){
		Position = startingPos;
		
	}
	public void setStartingPos(Vector2 pos){
		startingPos = pos;
	}
}
