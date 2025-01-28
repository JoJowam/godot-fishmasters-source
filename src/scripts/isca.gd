extends CharacterBody2D

func _ready():
	# Inicializar a velocidade se necessário
	velocity = Vector2.ZERO

func _physics_process(delta: float) -> void:
	# Mover a isca com a velocidade definida
	move_and_slide()
