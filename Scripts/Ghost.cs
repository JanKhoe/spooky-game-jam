using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class Ghost : myObj
{

	// [ExportCategory("myAttributes")]
	// [Export]
	public List<Vector2> followDirections;
	Vector2 curDirection = Vector2.Zero;
	Vector2 start;
	float timer = 0.0f;
	int Counter = 0;
	bool canStart = false;
	bool reset_state = false;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		GD.Print("Ghost spawned");
		goToStart();
		followDirections[0] = startingPos;
		start = followDirections[0];
		// await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);
		// canStart = true;
		foreach(var i in followDirections){
				GD.Print(i);
			}
			// Position = start;
			LinearVelocity = Vector2.Zero;
			reset_state = true;
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

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		

	}
    public override void _PhysicsProcess(double delta)
    {
				// LinearVelocity = new Vector2(1,1);
				if(LinearVelocity != Vector2.Zero){

						Vector2 dirVector1 = (followDirections[Counter] - Position).Normalized();
						Vector2 dirVector2 = (followDirections[Counter]-followDirections[Counter-1]).Normalized();
						float dotPrdt = dirVector1.Dot(dirVector2);
						//NEW PLAN: checks to see if the dot product of the 2 vectors is very cose to -1 that means that the object passed the point it was supposed to reach
						if( (dotPrdt <= -0.975 && dotPrdt >= -1.025) || (Position.X+5f>=followDirections[Counter].X&&Position.X-5<=followDirections[Counter].X&&Position.Y+5f >=followDirections[Counter].Y && Position.Y-5f <= followDirections[Counter].Y)||LinearVelocity == Vector2.Zero){
							if(Counter < followDirections.Count-1){
								start = followDirections[Counter];
								Counter++;
								GD.Print(Counter);
								GD.Print((followDirections[Counter] - Position).Normalized());
								GD.Print((followDirections[Counter]-followDirections[Counter-1]).Normalized());
								GD.Print(Position);
								GD.Print("Direction Changed");
								GD.Print(followDirections[Counter]);
								GD.Print(followDirections[Counter-1]);
								LinearVelocity = (followDirections[Counter] - Position).Normalized() * Speed * 60 * (float)delta;
							}
							else{
								// GD.Print(followDirections[Counter]);
								
								GD.Print("Freed");
								GD.Print(Counter);
								GD.Print(followDirections.Count);
								QueueFree();
							}
						}
				}
				else{
					Counter++;
					LinearVelocity = (followDirections[Counter] - followDirections[Counter-1]).Normalized() * Speed * 60 * (float)delta;
				}
				//if((followDirections[Counter] - Position))
        // if((Position.X+5f>=followDirections[Counter].X&&Position.X-5<=followDirections[Counter].X&&Position.Y+5f >=followDirections[Counter].Y && Position.Y-5f <= followDirections[Counter].Y)||LinearVelocity == Vector2.Zero){
				
				// 	if(Counter < followDirections.Count-1){
				// 		start = followDirections[Counter];
				// 		Counter++;
						
				// 		GD.Print("Direction Changed");
				// 		GD.Print(followDirections[Counter]);
				// 		GD.Print(followDirections[Counter-1]);
				// 		LinearVelocity = (followDirections[Counter] - followDirections[Counter-1]).Normalized() * Speed * 60 * (float)delta;
				// 	}
				// 	else{
				// 		GD.Print("Freed");
				// 		QueueFree();
				// 	}
				// }
					
	}
				

				// GD.Print(Position);
				// GD.Print(followDirections[Counter]);
				// Vector2 test = Position;
				// GD.Print(test.MoveToward(new Vector2(10, 0), 1));
				// LinearVelocity = Vector2.Zero;
				
}

