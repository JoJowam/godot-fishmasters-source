extends Area2D

# Variáveis de instância
var velocidade: float = 100.0  # Velocidade do objeto em pixels por segundo
var direcao: Vector2 = Vector2.ZERO  # Direção inicial do movimento

# Função chamada quando o nó entra na cena
func _ready() -> void:
	randomize()  # Inicializa o gerador de números aleatórios
	_mudar_direcao()  # Define uma direção inicial aleatória
	iniciar_timer(20.0)  # Inicia o timer com intervalo de 20 segundos

# Função para iniciar e configurar o Timer
func iniciar_timer(intervalo: float) -> void:
	var timer = $Timer
	if timer == null:
		timer = Timer.new()
		add_child(timer)
	timer.wait_time = intervalo
	timer.autostart = true
	timer.one_shot = false
	# Conecta o sinal 'timeout' ao método '_on_timer_timeout'
	timer.connect("timeout", Callable(self, "_on_timer_timeout"))
	timer.start()

# Função chamada quando o Timer atinge o tempo limite
func _on_timer_timeout() -> void:
	_mudar_direcao()

# Função para mudar a direção do movimento
func _mudar_direcao() -> void:
	var angulo = randf() * 360.0  # Gera um ângulo aleatório entre 0 e 360 graus
	direcao = Vector2.RIGHT.rotated(deg_to_rad(angulo)).normalized()
	if $Label:
		$Label.text = "Nova direção: " + str(direcao)

# Função processada a cada frame para atualizar a posição do objeto
func _process(delta: float) -> void:
	if direcao != Vector2.ZERO:
		position += direcao * (velocidade * 0.02) * delta

# Função para detectar entrada do usuário para depuração
func _input(event: InputEvent) -> void:
	if event.is_action_pressed("ui_accept"):
		_mudar_direcao()
