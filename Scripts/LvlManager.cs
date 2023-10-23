using Godot;
using System;
using System.Collections.Generic;

public partial class LvlManager : Node
{
	List<List<Vector2>> followDirections = new();
	List<Vector2> pos = new();
	[Export]
	PackedScene GhostPrefab;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GhostPrefab = GD.Load<PackedScene>("res://Prefabs/Ghost.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void setParams(List<Vector2> followDirections, Vector2 pos){
		this.followDirections = new();
		this.pos = new();
		this.followDirections.Add(followDirections);
		this.pos.Add(pos);
	}
	private void _on_p_layer_restart_run(){
		for(int i = 0; i< followDirections.Count; i++){
			RigidBody2D Ghost = GhostPrefab.Instantiate<RigidBody2D>();
			Ghost.Position = Vector2.Zero;
			Ghost ghostScript = Ghost as Ghost;
			GD.Print(ghostScript);
			
			ghostScript.setStartingPos(pos[i]);
			// Ghost.CallDeferred("AddChild");
			ghostScript.followDirections = followDirections[i];
			GD.Print("Ghost made");

			CallDeferred("add_child", Ghost);
		}
		
		// AddChild(Ghost);
		
	}
}
