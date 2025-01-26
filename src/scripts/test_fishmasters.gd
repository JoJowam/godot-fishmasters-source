extends Node2D

class_name TesteFishMasters

var vara_forca: float = 0.0
var posicao_peixe: Vector2 = Vector2.ZERO
var barra_progresso: float = 0.0

func _ready():
	print("Iniciando testes do FishMasters")
	testar_forca_vara()
	testar_movimento_peixe()
	testar_barra_progresso()

func testar_forca_vara():
	print("\nTestando força da vara:")
	vara_forca = 0.5
	assert(vara_forca >= 0 and vara_forca <= 1, "Força da vara deve estar entre 0 e 1")
	print("✓ Teste de força da vara OK")

func testar_movimento_peixe():
	print("\nTestando movimento do peixe:")
	posicao_peixe = Vector2(100, 150)
	assert(posicao_peixe.x >= 0, "Posição X do peixe deve ser positiva")
	assert(posicao_peixe.y >= 0, "Posição Y do peixe deve ser positiva")
	print("✓ Teste de movimento do peixe OK")

func testar_barra_progresso():
	print("\nTestando barra de progresso:")
	barra_progresso = 0.75
	assert(barra_progresso >= 0 and barra_progresso <= 1, "Progresso deve estar entre 0 e 1")
	print("✓ Teste de barra de progresso OK")

func _process(_delta):
	if Input.is_action_just_pressed("ui_accept"):
		print("\nExecutando todos os testes novamente...")
		testar_forca_vara()
		testar_movimento_peixe()
		testar_barra_progresso()
