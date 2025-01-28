extends Node2D

@export var bait_scene: PackedScene  # Referência para a cena da isca
@export var launch_speed: float = 500.0  # Velocidade de lançamento da isca

func _input(event: InputEvent) -> void:
	# Detectar clique do botão esquerdo do mouse
	if event is InputEventMouseButton and event.button_index == MOUSE_BUTTON_LEFT and event.pressed:
		# Calcular o ângulo entre a vara e o mouse
		var mouse_position = get_global_mouse_position()
		var vara_position = global_position
		var angle = (mouse_position - vara_position).angle()

		# Lançar a isca a partir da posição da vara
		launch_bait(angle, vara_position)

func launch_bait(angle: float, position: Vector2) -> void:
	# Verificar se a cena da isca foi atribuída
	if bait_scene == null:
		print("Erro: bait_scene não foi atribuída!")
		return

	# Instanciar a isca
	var bait = bait_scene.instantiate() as CharacterBody2D
	if bait == null:
		print("Erro: bait_scene não é uma cena válida!")
		return

	# Adicionar a isca ao nó pai da vara (ou outro nó apropriado)
	get_parent().add_child(bait)

	# Configurar a posição inicial da isca
	bait.global_position = position

	# Calcular a direção com base no ângulo
	var direction = Vector2(cos(angle), sin(angle)).normalized()

	# Configurar a velocidade inicial da isca
	bait.velocity = direction * launch_speed
