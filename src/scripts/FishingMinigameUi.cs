using Godot;
using System;

public partial class FishingMinigameUi : Control {
	Label fishCount;
	TextureProgressBar progressBar;
	
	public override void _Ready(){
		fishCount = GetNode<Label>("Label");
		progressBar = GetNode<TextureProgressBar>("TextureProgressBar");

		FishingMinigame fishingMinigame = GetNode<FishingMinigame>("../FishingMinigame");
	
		// Conecta os signals do FishingMinigame aos métodos aqui definidos
		fishingMinigame.Connect(nameof(FishingMinigame.FishCaught), new Callable(this, nameof(OnFishingMinigameOnFishCaught)));
		fishingMinigame.Connect(nameof(FishingMinigame.FishProcess), new Callable(this, nameof(OnFishingMinigameOnFishProcess)));
	}

	private void OnFishingMinigameOnFishCaught(int count){
		fishCount.Text = count.ToString();
	}
	
	private void OnFishingMinigameOnFishProcess(float progress){
		// Atualiza a barra de progresso (supondo que 100 seja o máximo)
		progressBar.Value = progress * 100f;
	}
}
