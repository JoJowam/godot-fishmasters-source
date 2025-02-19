using Godot;
using System;

public partial class FishingMinigame : Node2D {
	Node2D hookArea;
	Node2D topPosition;
	Node2D fishIndicator;
	Node2D bottomPosition;
	
	float fishTimer;
	float fishPosition;
	float fishTargetPosition;
	[Export] float fishThinkTime = 8f;
	[Export] float fishMoveSpeed = 2f;
	
	float hookPosition;
	float hookVelocity;
	[Export] float hookPullPower = 0.5f;
	[Export] float hookGravity = 0.01f;
	[Export] float hookSize = 0.2f;
	
	float hookProgress;
	[Export] float hookPower = 0.2f;
	
	bool isPaused;
	int fishCaughtCount;
	
	[Signal] public delegate void ProgressUpdatedEventHandler(float progress);
	[Signal] public delegate void FishCaughtXpEventHandler(int xp);


	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		topPosition = GetNode<Node2D>("topPosition");
		fishIndicator = GetNode<Node2D>("fishIndicator");
		bottomPosition = GetNode<Node2D>("bottomPosition");
		hookArea = GetNode<Node2D>("hookArea");
		
		fishIndicator.GlobalPosition = CalculatePosition(1f);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		float timeDelta = (float)delta;
		if (isPaused == true){ return; }
		ProcessFish(timeDelta);
		ProcessHook(timeDelta);
		DetectProgress(timeDelta);
	}
	
	void ProcessFish(float timeDelta){
		fishTimer -= timeDelta;
		
		if (fishTimer < 0f){
			fishTimer = fishThinkTime * GD.Randf();
			fishTargetPosition = GD.Randf();
		}
		
		fishPosition = Mathf.Lerp(fishPosition, fishTargetPosition, fishMoveSpeed * timeDelta);
		fishIndicator.GlobalPosition = CalculatePosition(fishPosition);
	}
	
	void ProcessHook(float timeDelta){
		if(Input.IsActionPressed("hook_pull")){
			hookVelocity += hookPullPower * timeDelta;
		}
		
		hookVelocity -= hookGravity * timeDelta;
		hookPosition += hookVelocity;
		hookPosition = Mathf.Clamp(hookPosition, hookSize/2f, 1f - hookSize/2f);
		
		if (hookPosition == hookSize/2f && hookVelocity < 0f){
			hookVelocity = 0f;
		}
		
		if (hookPosition == 1f-hookSize/2f && hookVelocity > 0f){
			hookVelocity = 0f;
		}
		
		hookArea.GlobalPosition = CalculatePosition(hookPosition);
	}
	
	void DetectProgress(float timeDelta){
		float hookTopBoundry = hookPosition + hookSize/2f;
		float hookBottomBoundry = hookPosition - hookSize/2f;
		
		if (hookBottomBoundry < fishPosition && fishPosition < hookTopBoundry){
			hookProgress += hookPower * timeDelta;
			EmitSignal(nameof(ProgressUpdated), hookProgress);
		}
		
		if (hookProgress >= 1f){
			onFishCaught();
		}
	}
	
	void onFishCaught(){
		GD.Print("Ta pescado!");
		isPaused = true;
		fishCaughtCount += 1;
		
		int xpGained = (int)(GD.Randi() % 50 + 10);
		EmitSignal(nameof(FishCaughtXp), xpGained);
	}
	
	Vector2 CalculatePosition(float normalizedPosition){
		normalizedPosition = 1f - normalizedPosition;
		Vector2 newPosition = bottomPosition.GlobalPosition - topPosition.GlobalPosition;
		newPosition *= normalizedPosition;
		newPosition += topPosition.GlobalPosition;
		return newPosition;
	}
}
