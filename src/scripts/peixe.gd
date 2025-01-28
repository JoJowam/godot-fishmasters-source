extends CharacterBody2D

const SPEED = 50.0
var direction := Vector2.ZERO

func _ready():
	# Inicializa uma direção aleatória
	set_random_direction()

func _physics_process(delta: float) -> void:
	# Move o peixe na direção atual
	velocity = direction * SPEED
	move_and_slide()

	# Altere a direção aleatoriamente em intervalos
	if randf() < 0.01:  # Pequena chance de mudar a direção a cada frame
		set_random_direction()

func set_random_direction():
	# Define uma direção aleatória normalizada
	direction = Vector2(randf() * 2 - 1, randf() * 2 - 1).normalized()
