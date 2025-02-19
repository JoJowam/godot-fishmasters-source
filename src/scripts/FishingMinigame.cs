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
	float fishMoveSpeed;
	
	float hookPosition;
	float hookVelocity;
	[Export] float hookPullPower = 0.5f;
	[Export] float hookGravity = 0.01f;
	[Export] float hookSize = 0.2f;
	
	float hookProgress;
	[Export] float hookPower = 0.2f;
	
	bool isPaused;
	int fishCaughtCount;
	
	int currentXp = 0;
	int xpToNextLevel = 100;
	int level = 1;
	
	public enum Difficulty {
		Easy = 1,
		Medium = 2,
		Hard = 3
	}
	
	[Export] public Difficulty currentDifficulty = Difficulty.Easy;
	
	[Signal] public delegate void ProgressUpdatedEventHandler(float progress);
	[Signal] public delegate void FishCaughtXpEventHandler(int xp);
	[Signal] public delegate void XpUpdatedEventHandler(int xp, int maxXp, int level);
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		topPosition = GetNode<Node2D>("topPosition");
		fishIndicator = GetNode<Node2D>("fishIndicator");
		bottomPosition = GetNode<Node2D>("bottomPosition");
		hookArea = GetNode<Node2D>("hookArea");
		
		SetFishSpeed();
		ResetFishPosition();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		float timeDelta = (float)delta;
		if (Input.IsActionJustPressed("restart")){
			Restart();
		}
		if (isPaused == true){ return; }		
		ProcessFish(timeDelta);
		ProcessHook(timeDelta);
		DetectProgress(timeDelta);
	}
	
	 void SetFishSpeed(){
		switch (currentDifficulty){
			case Difficulty.Easy:
				fishMoveSpeed = 1.5f;
				break;
			case Difficulty.Medium:
				fishMoveSpeed = 2.5f;
				break;
			case Difficulty.Hard:
				fishMoveSpeed = 4f;
				break;
		}
	}
	
	void ResetFishPosition(){
		fishPosition = 0.01f;
		fishTargetPosition = fishPosition;
		fishIndicator.GlobalPosition = CalculatePosition(fishPosition);
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
		float hookTopBoundry = hookPosition + hookSize / 2f;
		float hookBottomBoundry = hookPosition - hookSize / 2f;

		if (hookBottomBoundry < fishPosition && fishPosition < hookTopBoundry){
			AddProgress(hookPower * timeDelta);
		} else {
			AddProgress(-hookPower * 0.5f * timeDelta); 
		}

		if (hookProgress >= 1f){
			onFishCaught();
		}
		
		if (hookProgress <= 0f){
			onFishEscaped();
		}
		
		hookProgress = Mathf.Clamp(hookProgress, 0f, 1f);
	}
	
	void AddProgress(float amount){
		hookProgress += amount;
		EmitSignal(nameof(ProgressUpdated), hookProgress);
	}
	
	void SetProgress(float to){
		hookProgress = to;
		EmitSignal(nameof(ProgressUpdated), hookProgress);
	}
	
	void onFishCaught(){
		GD.Print("Ta pescado!");
		isPaused = true;
		fishCaughtCount += 1;
		
		int xpGained = (int)(GD.Randi() % 50 + 10);
		EmitSignal(nameof(FishCaughtXp), xpGained);
		AddXp(xpGained);
	}
	
	void onFishEscaped(){
		GD.Print("O peixe escapou!");
		isPaused = true;
	}
	
	void AddXp(int xpGained){
		currentXp += xpGained;

		if (currentXp >= xpToNextLevel){
			currentXp -= xpToNextLevel; 
			level++;
			xpToNextLevel += 100;
			GD.Print($"Subiu para o nível {level}!");
		}

		EmitSignal(nameof(XpUpdated), currentXp, xpToNextLevel, level);
	}
	
	void Restart(){
		hookPosition = 0f;
		hookVelocity = 0f;
		SetProgress(0f);
		isPaused = false;
		
		SetFishSpeed();
		ResetFishPosition();
	}
	
	Vector2 CalculatePosition(float normalizedPosition){
		normalizedPosition = 1f - normalizedPosition;
		Vector2 newPosition = bottomPosition.GlobalPosition - topPosition.GlobalPosition;
		newPosition *= normalizedPosition;
		newPosition += topPosition.GlobalPosition;
		return newPosition;
	}
}
