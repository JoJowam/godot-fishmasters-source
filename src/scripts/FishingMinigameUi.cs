using Godot;
using System;

public partial class FishingMinigameUi : Control {
	Label xpLabel;
	Label levelLabel;
	TextureProgressBar progressBar;
	TextureProgressBar progressBarXp;
	
	public override void _Ready(){
		xpLabel = GetNode<Label>("Label");
		levelLabel = GetNode<Label>("LevelLabel");
		progressBar = GetNode<TextureProgressBar>("TextureProgressBar");
		progressBarXp = GetNode<TextureProgressBar>("TextureProgressBarXp");
	}

	private void OnFishingMinigameXpGained(int xp){
		xpLabel.Text = $"XP Ganhado: {xp}";
	}
	
	private void OnFishingMinigameOnFishProcess(float progress){
		progressBar.Value = progress * 100f;
	}
	
	private void OnFishingMinigameXpUpdated(int currentXp, int maxXp, int level){
		progressBarXp.MaxValue = maxXp;
		progressBarXp.Value = currentXp;
		levelLabel.Text = $"Nível: {level}";
	}
}
