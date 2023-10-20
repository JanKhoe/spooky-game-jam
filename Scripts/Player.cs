using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;
using System.Collections;
using System.Collections.Generic;

public partial class Player : myObj
{
	Vector2 direction;
	private List<Vector2> followDirections= new();
	private Vector2 curDirection;
	bool reset_state = false;
	

	[Export]
	PackedScene GhostPrefab;
	[Export]
	LvlManager lvlManager;
	[Signal]
	public delegate void RestartRunEventHandler();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// LinearDamp = 0.0f;
		// LinearDampMode = DampMode.Replace;
		curDirection = Vector2.Zero;
		GhostPrefab = GD.Load<PackedScene>("res://Prefabs/Ghost.tscn");
		startingPos = new Vector2(22, 22);
		GD.Print("Hellooooo");
		reset_state = true;
		followDirections.Add(startingPos);
		// Dictionary<Vector2, float> pair =new(){
		// 	{Vector2.Zero, 12.8f}
		// 	};	
		// followDirections.Add(pair);
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
	{
		direction = Vector2.Zero;

		if (Input.IsActionPressed("up")){
			direction.Y -= 1;
		}
		if (Input.IsActionPressed("down")){
			direction.Y += 1;
		}
		if (Input.IsActionPressed("left")){
			direction.X -= 1;
		}
		if (Input.IsActionPressed("right")){
			direction.X += 1;
		}
		if(Input.IsActionJustPressed("check")){
			GD.Print("CURRENT POS: "+ Position);
			foreach(var i in followDirections){
				GD.Print(i);
			}

			// GD.Print(followDirections);
		}
		
		//This conditional will only proc if you switched direction (Stading still also counts as switching directions bc it makes the direction vector 0)
		if(curDirection != direction){
			//If you are not standing still
			if(followDirections[^1]!=Position){
				curDirection = direction;
				followDirections.Add(Position);
			}
			//Otherwise
			else{
				
			}
			
		}
		LinearVelocity = direction.Normalized()*Speed*(float)delta*60;
		// Position += direction.Normalized() * Speed * (float)delta;
		// GD.Print(direction);
		// GD.Print(Position);

	}

	void _on_area_2d_area_entered(Area2D Body){
		GD.Print("Hello");
		followDirections.Add(Position + direction*50);
		lvlManager.setParams(followDirections, startingPos);
		EmitSignal(SignalName.RestartRun);
		GD.Print("NEW POSITION"+Position);
		startingPos = new Vector2(startingPos.X, startingPos.Y+150);
		
		// goToStart();

		reset_state = true;
		// LinearVelocity = Vector2.Zero;
		
		GD.Print("NEW POSITION"+Position);
		followDirections = new(){startingPos};
		
		
	}
    public override void _IntegrateForces(PhysicsDirectBodyState2D state)
    {
				if(reset_state){
					var newTransform = state.Transform;
					newTransform.Origin.X = startingPos.X;
					newTransform.Origin.Y = startingPos.Y;
					state.Transform = newTransform;
					reset_state = false;
				}
        
    }

}
