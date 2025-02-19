using Godot;
using System;

public partial class FishingMinigameUi : Control {
	Label xpLabel;
	TextureProgressBar progressBar;
	
	public override void _Ready(){
		xpLabel = GetNode<Label>("Label");
		progressBar = GetNode<TextureProgressBar>("TextureProgressBar");
	}

	private void OnFishingMinigameXpGained(int xp){
		xpLabel.Text = $"XP Ganhado: {xp}";
	}
	
	private void OnFishingMinigameOnFishProcess(float progress){
		progressBar.Value = progress * 100f;
	}
}
