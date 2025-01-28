extends CharacterBody2D

@export var lifetime: float = 5.0  # Tempo de vida da isca, em segundos

func _ready() -> void:
	# Remover a isca automaticamente após o tempo de vida
	await get_tree().create_timer(lifetime).timeout
	queue_free()

func _physics_process(delta: float) -> void:
	# Mover a isca com a velocidade definida
	move_and_slide()

	# Parar a isca se ela colidir com algo
	if is_on_wall() or is_on_floor():
		velocity = Vector2.ZERO
