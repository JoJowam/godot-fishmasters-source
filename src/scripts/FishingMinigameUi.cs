using Godot;
using System;

public partial class FishingMinigameUi : Control {
	Label xpLabel;
	TextureProgressBar progressBar;
	
	public override void _Ready(){
		xpLabel = GetNode<Label>("Label");
		progressBar = GetNode<TextureProgressBar>("TextureProgressBar");

		FishingMinigame fishingMinigame = GetNode<FishingMinigame>("../FishingMinigame");
		fishingMinigame.Connect(nameof(FishingMinigame.ProgressUpdated), new Callable(this, nameof(OnFishingMinigameOnFishProcess)));
		fishingMinigame.Connect(nameof(FishingMinigame.FishCaughtXp), new Callable(this, nameof(OnFishingMinigameXpGained)));
	}

	private void OnFishingMinigameXpGained(int xp){
		xpLabel.Text = $"XP Ganhado: {xp}";
	}
	
	private void OnFishingMinigameOnFishProcess(float progress){
		progressBar.Value = progress * 100f;
	}
}
