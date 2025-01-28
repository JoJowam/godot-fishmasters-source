using Godot;
using System;

public partial class FishingMinigameUi : Control{
	
	TextureProgressBar progressBar;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		progressBar = GetNode<TextureProgressBar>("TextureProgressBar");
	}

	private void OnFishingMinigameOnFishProcess(double progress){
		progressBar.Value = progress * 100f;
	}
}
