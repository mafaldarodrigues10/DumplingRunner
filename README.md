# Dumpling Runner 🥟

## Elementos do grupo
Inês Marinho - 33201  
Mafalda Rodrigues - 33386  

## Descrição do jogo
Jogo do tipo endless runner onde um dumpling corre continuamente e tem de evitar obstáculos e apanhar moedas. As moedas permitem ao jogador acumular pontuação e podem ser usadas para desbloquear diferentes cenários no jogo.

## Objetivo
O objetivo do jogo é sobreviver o máximo tempo possível, evitando obstáculos, enquanto se tenta obter a maior pontuação e recolher o máximo de moedas possível.

## Funcionalidades implementadas
### Movimento do jogador
O jogador controla um dumpling que se move automaticamente para a frente.  
Pode deslocar-se lateralmente (A/D ou setas esquerda/direita), saltar (seta para cima) e baixar/deslizar (seta para baixo), permitindo evitar obstáculos de diferentes tipos.

### Sistema de colisões
O jogo inclui vários tipos de obstáculos (aproximadamente 5). Quando o jogador não reage a tempo (não desvia, salta ou baixa), ocorre uma colisão que termina o jogo (Game Over).

### Sistema de pontuação
A pontuação aumenta ao longo do tempo de sobrevivência do jogador, incentivando a progressão e melhoria contínua.

### Sistema de moedas
Moedas aparecem ao longo do percurso e podem ser recolhidas pelo jogador. Cada moeda aumenta a pontuação durante a partida. Para além disso, as moedas acumuladas podem ser utilizadas para desbloquear diferentes cenários do jogo. O jogo inclui vários cenários distintos, cada um com um custo específico em moedas. À medida que o jogador vai jogando e acumulando moedas, pode desbloquear novos cenários, que serão implementados como novos ecrãs/ambientes dentro do jogo.

### Obstáculos e geração dinâmica
Os obstáculos são gerados ao longo do percurso, criando variação e aumentando o desafio do jogo.

### Dificuldade incremental
A velocidade do jogo aumenta progressivamente com o tempo, tornando o jogo mais difícil à medida que o jogador avança.

### Interface (UI)
O jogo apresenta interface com:
- Pontuação
- Moedas
- Ecrã de Game Over

### Sistema de áudio
Inclui música de fundo e efeitos sonoros, como:
- Interações na interface
- Colisão com obstáculos
- Recolha de moedas
- Música de fundo


### Variação de cenários
O jogo inclui diferentes cenários que podem ser desbloqueados, proporcionando diversidade visual e maior replayabilidade.

## Como abrir o projeto
1. Clonar ou fazer download do repositório GitHub  
2. Abrir o Unity Hub  
3. Clicar em "Add project" e selecionar a pasta do projeto  
4. Abrar o projeto com a versão correta do Unity  
5. Abrir a cena principal localizada na pasta "Scenes"  
6. Pressionar o botão Play para executar o jogo

## Assets multimédia
### Modelos 3D e texturas
Os elementos visuais do jogo são compostos por modelos 3D no formato `.fbx`, utilizados para personagens, obstáculos e cenários.

Estes modelos utilizam texturas (imagens 2D) aplicadas às superfícies para definir o aspeto visual.  
Inicialmente foram utilizadas texturas de maior resolução (até 4K no dumpling), no entanto, devido a problemas de desempenho (lentidão e falhas), foi feita uma otimização.

A maioria das texturas foi ajustada para resolução de 1024x1024 (1K), garantindo um melhor equilíbrio entre qualidade visual e performance do jogo.

### Áudio
Os efeitos sonoros incluem:
- Interações na interface
- Colisão com obstáculos
- Recolha de moedas
- Música de fundo

Os ficheiros de áudio foram utilizados em formatos comuns, sendo a música de fundo em formato `.mp3` e os efeitos sonoros em formato `.wav`, ambos adequados para integração no Unity.

### Justificação das escolhas
As escolhas de formatos e resoluções foram feitas com o objetivo de:
- Melhorar a performance do jogo
- Reduzir consumo de recursos
- Evitar lag e problemas gráficos
- Manter qualidade visual consistente
- Garantir compatibilidade com o Unity
