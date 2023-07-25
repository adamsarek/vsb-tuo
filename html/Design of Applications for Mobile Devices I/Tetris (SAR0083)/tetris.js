const maxLevelSpeed = 20;
const levelSpeedExp = Math.log10(maxLevelSpeed);
const levels = [
	{level:  1, minScore:     0, speed: Math.pow( 2 * 0.5, levelSpeedExp)},
	{level:  2, minScore:    20, speed: Math.pow( 3 * 0.5, levelSpeedExp)},
	{level:  3, minScore:    40, speed: Math.pow( 4 * 0.5, levelSpeedExp)},
	{level:  4, minScore:    80, speed: Math.pow( 5 * 0.5, levelSpeedExp)},
	{level:  5, minScore:   160, speed: Math.pow( 6 * 0.5, levelSpeedExp)},
	{level:  6, minScore:   320, speed: Math.pow( 7 * 0.5, levelSpeedExp)},
	{level:  7, minScore:   640, speed: Math.pow( 8 * 0.5, levelSpeedExp)},
	{level:  8, minScore:  1280, speed: Math.pow( 9 * 0.5, levelSpeedExp)},
	{level:  9, minScore:  2560, speed: Math.pow(10 * 0.5, levelSpeedExp)},
	{level: 10, minScore:  5120, speed: Math.pow(11 * 0.5, levelSpeedExp)},
	{level: 11, minScore: 10240, speed: Math.pow(12 * 0.5, levelSpeedExp)},
	{level: 12, minScore: 12000, speed: Math.pow(12 * 0.5, levelSpeedExp)},
	{level: 13, minScore: 13000, speed: Math.pow(13 * 0.5, levelSpeedExp)},
	{level: 14, minScore: 14000, speed: Math.pow(14 * 0.5, levelSpeedExp)},
	{level: 15, minScore: 15000, speed: Math.pow(15 * 0.5, levelSpeedExp)},
	{level: 16, minScore: 16000, speed: Math.pow(16 * 0.5, levelSpeedExp)},
	{level: 17, minScore: 17000, speed: Math.pow(17 * 0.5, levelSpeedExp)},
	{level: 18, minScore: 18000, speed: Math.pow(18 * 0.5, levelSpeedExp)},
	{level: 19, minScore: 19000, speed: Math.pow(19 * 0.5, levelSpeedExp)},
	{level: 20, minScore: 20000, speed: Math.pow(20 * 0.5, levelSpeedExp)}
];

const tetrominos = {
	I:{
		fullImageSrc:'src/img/tetrominos/I.svg',
		blockImage:new Image(),
		matrixRotations:[
			[
				[0,0,0,0],
				[0,0,0,0],
				[1,1,1,1],
				[0,0,0,0]
			],
			[
				[0,0,1,0],
				[0,0,1,0],
				[0,0,1,0],
				[0,0,1,0]
			]
		]
	},
	J:{
		fullImageSrc:'src/img/tetrominos/J.svg',
		blockImage:new Image(),
		matrixRotations:[
			[
				[1,0,0],
				[1,1,1],
				[0,0,0]
			],
			[
				[0,1,1],
				[0,1,0],
				[0,1,0]
			],
			[
				[0,0,0],
				[1,1,1],
				[0,0,1]
			],
			[
				[0,1,0],
				[0,1,0],
				[1,1,0]
			]
		]
	},
	L:{
		fullImageSrc:'src/img/tetrominos/L.svg',
		blockImage:new Image(),
		matrixRotations:[
			[
				[0,0,1],
				[1,1,1],
				[0,0,0]
			],
			[
				[0,1,0],
				[0,1,0],
				[0,1,1]
			],
			[
				[0,0,0],
				[1,1,1],
				[1,0,0]
			],
			[
				[1,1,0],
				[0,1,0],
				[0,1,0]
			]
		]
	},
	O:{
		fullImageSrc:'src/img/tetrominos/O.svg',
		blockImage:new Image(),
		matrixRotations:[
			[
				[1,1],
				[1,1]
			]
		]
	},
	S:{
		fullImageSrc:'src/img/tetrominos/S.svg',
		blockImage:new Image(),
		matrixRotations:[
			[
				[0,0,0],
				[0,1,1],
				[1,1,0]
			],
			[
				[0,1,0],
				[0,1,1],
				[0,0,1]
			]
		]
	},
	T:{
		fullImageSrc:'src/img/tetrominos/T.svg',
		blockImage:new Image(),
		matrixRotations:[
			[
				[0,1,0],
				[1,1,1],
				[0,0,0]
			],
			[
				[0,1,0],
				[0,1,1],
				[0,1,0]
			],
			[
				[0,0,0],
				[1,1,1],
				[0,1,0]
			],
			[
				[0,1,0],
				[1,1,0],
				[0,1,0]
			]
		]
	},
	Z:{
		fullImageSrc:'src/img/tetrominos/Z.svg',
		blockImage:new Image(),
		matrixRotations:[
			[
				[0,0,0],
				[1,1,0],
				[0,1,1]
			],
			[
				[0,0,1],
				[0,1,1],
				[0,1,0]
			]
		]
	}
};
tetrominos.I.blockImage.src = 'src/img/tetromino_blocks/I.svg';
tetrominos.J.blockImage.src = 'src/img/tetromino_blocks/J.svg';
tetrominos.L.blockImage.src = 'src/img/tetromino_blocks/L.svg';
tetrominos.O.blockImage.src = 'src/img/tetromino_blocks/O.svg';
tetrominos.S.blockImage.src = 'src/img/tetromino_blocks/S.svg';
tetrominos.T.blockImage.src = 'src/img/tetromino_blocks/T.svg';
tetrominos.Z.blockImage.src = 'src/img/tetromino_blocks/Z.svg';

class Tetromino{
	constructor(id){
		this.id = id;
		this.fullImageSrc = tetrominos[id].fullImageSrc;
		this.blockImage = tetrominos[id].blockImage;
		
		this.rotation = 0;
		this.matrix = tetrominos[id].matrixRotations[this.rotation];
		this.updateSize();
	}
	
	// Position
	getPosition(){return this.position;}
	setPosition(position){this.position = position;}
	
	// Rotation
	rotateLeft(){
		this.rotation = (this.rotation <= 0 ? tetrominos[this.id].matrixRotations.length - 1 : this.rotation - 1);
		this.matrix = tetrominos[this.id].matrixRotations[this.rotation];
		this.updateSize();
	}
	rotateRight(){
		this.rotation = (this.rotation >= tetrominos[this.id].matrixRotations.length - 1 ? 0 : this.rotation + 1);
		this.matrix = tetrominos[this.id].matrixRotations[this.rotation];
		this.updateSize();
	}
	
	// Movement
	moveDown(){
		const position = this.position;
		position.y--;
		this.setPosition(position);
	}
	moveLeft(){
		const position = this.position;
		position.x--;
		this.setPosition(position);
	}
	moveRight(){
		const position = this.position;
		position.x++;
		this.setPosition(position);
	}
	
	// Size
	getSize(){return this.size;}
	updateSize(){
		this.size = {w:0, h:0};
		let xMin = this.matrix[0].length - 1;
		let xMax = 0;
		for(let i = 0; i < this.matrix.length; i++){
			if(this.matrix[i].includes(1)){
				this.size.h++;
				xMin = (this.matrix[i].indexOf(1) < xMin ? this.matrix[i].indexOf(1) : xMin);
				xMax = (this.matrix[i].lastIndexOf(1) > xMax ? this.matrix[i].lastIndexOf(1) : xMax);
			}
		}
		this.size.w = xMax - xMin + 1;
	}
}

class Game{
	constructor(){
		// Default settings
		this.aspectRatio = 1/1;
		this.state = '';
		
		// Remove unfinished runs
		if(localStorage.hasOwnProperty('runs')){
			let runs = JSON.parse(localStorage.getItem('runs'));
			for(let i = 0; i < runs.length; i++){
				const run = runs[i];
				if(!run.hasOwnProperty('timeEnd')){
					runs.splice(i,1);
					localStorage.setItem('runs', JSON.stringify(runs));
				}
			}
			if(runs.length == 0){
				localStorage.removeItem('runs');
			}
		}
		
		// Sounds
		this.sounds = {};
		this.sounds.drop = new Audio('src/audio/drop.mp3');
		this.sounds.gameOver = new Audio('src/audio/gameOver.mp3');
		this.sounds.line = new Audio('src/audio/line.mp3');
		
		// Elements
		this.elements = {};
		this.elements.game = this.createElement('div', {id:'game'});
			this.elements.gameLeft = this.createElement('div', {id:'gameLeft'}, '', this.elements.game);
				this.elements.gameLeftTop = this.createElement('div', {id:'gameLeftTop'}, '', this.elements.gameLeft);
					this.elements.gameLeftTopScore = this.createElement('div', {id:'gameLeftTopScore'}, '', this.elements.gameLeftTop);
						this.elements.gameLeftTopScoreTitle = this.createElement('div', {id:'gameLeftTopScoreTitle'}, 'Score', this.elements.gameLeftTopScore);
						this.elements.gameLeftTopScoreContent = this.createElement('div', {id:'gameLeftTopScoreContent'}, 0, this.elements.gameLeftTopScore);
					this.elements.gameLeftTopLevel = this.createElement('div', {id:'gameLeftTopLevel'}, '', this.elements.gameLeftTop);
						this.elements.gameLeftTopLevelTitle = this.createElement('div', {id:'gameLeftTopLevelTitle'}, 'Level', this.elements.gameLeftTopLevel);
						this.elements.gameLeftTopLevelContent = this.createElement('div', {id:'gameLeftTopLevelContent'}, 1, this.elements.gameLeftTopLevel);
				this.elements.gameLeftBottom = this.createElement('div', {id:'gameLeftBottom'}, '', this.elements.gameLeft);
					this.elements.gameLeftBottomMutedButton = this.createElement('button', {id:'gameLeftBottomMutedButton', 'class':'material-icons'}, 'volume_up', this.elements.gameLeftBottom);
					this.elements.gameLeftBottomPauseButton = this.createElement('button', {id:'gameLeftBottomPauseButton', 'class':'material-icons'}, 'play_arrow', this.elements.gameLeftBottom);
			this.elements.gameCenter = this.createElement('div', {id:'gameCenter'}, '', this.elements.game);
				this.elements.gameCenterInfo = this.createElement('div', {id:'gameCenterInfo'}, '', this.elements.gameCenter);
				this.elements.gameCenterArea = this.createElement('div', {id:'gameCenterArea'}, '', this.elements.gameCenter);
			this.elements.gameRight = this.createElement('div', {id:'gameRight'}, '', this.elements.game);
				this.elements.gameRightTop = this.createElement('div', {id:'gameRightTop'}, '', this.elements.gameRight);
					this.elements.gameRightTopNext = this.createElement('div', {id:'gameRightTopNext'}, '', this.elements.gameRightTop);
						this.elements.gameRightTopNextTitle = this.createElement('div', {id:'gameRightTopNextTitle'}, 'Next', this.elements.gameRightTopNext);
						this.elements.gameRightTopNextContent = this.createElement('div', {id:'gameRightTopNextContent'}, '', this.elements.gameRightTopNext);
							this.elements.gameRightTopNextContentImage = this.createElement('img', {id:'gameRightTopNextContentImage'}, '', this.elements.gameRightTopNextContent);
					this.elements.gameRightTopTime = this.createElement('div', {id:'gameRightTopTime'}, '', this.elements.gameRightTop);
						this.elements.gameRightTopTimeTitle = this.createElement('div', {id:'gameRightTopTimeTitle'}, 'Time', this.elements.gameRightTopTime);
						this.elements.gameRightTopTimeContent = this.createElement('div', {id:'gameRightTopTimeContent'}, this.getTimeFormat(0), this.elements.gameRightTopTime);
				this.elements.gameRightBottom = this.createElement('div', {id:'gameRightBottom'}, '', this.elements.gameRight);
					this.elements.gameRightBottomRotateButton = this.createElement('button', {id:'gameRightBottomRotateButton', 'class':'material-icons'}, 'rotate_right', this.elements.gameRightBottom);
					this.elements.gameRightBottomDropButton = this.createElement('button', {id:'gameRightBottomDropButton', 'class':'material-icons'}, 'arrow_downward', this.elements.gameRightBottom);
			this.elements.gameOverlay = this.createElement('div', {id:'gameOverlay'}, '', this.elements.game);
				this.elements.gameOverlayMenu = this.createElement('div', {id:'gameOverlayMenu', 'class':'visible'}, '', this.elements.gameOverlay);
					this.elements.gameOverlayMenuLogo = this.createElement('img', {id:'gameOverlayMenuLogo', src:'src/img/tetris.png'}, '', this.elements.gameOverlayMenu);
					this.elements.gameOverlayMenuButtonPlay = this.createElement('button', {id:'gameOverlayMenuButtonPlay', 'class':'rounded'}, 'Play', this.elements.gameOverlayMenu);
					this.elements.gameOverlayMenuButtonHighscores = this.createElement('button', {id:'gameOverlayMenuButtonHighscores', 'class':'rounded'}, 'Highscores', this.elements.gameOverlayMenu);
					this.elements.gameOverlayMenuButtonCredits = this.createElement('button', {id:'gameOverlayMenuButtonCredits', 'class':'rounded'}, 'Credits', this.elements.gameOverlayMenu);
				this.elements.gameOverlayHighscores = this.createElement('div', {id:'gameOverlayHighscores'}, '', this.elements.gameOverlay);
					this.elements.gameOverlayHighscoresTitle = this.createElement('h1', {id:'gameOverlayHighscoresTitle'}, 'Highscores', this.elements.gameOverlayHighscores);
						this.elements.gameOverlayHighscoresTitleCloseButton = this.createElement('button', {id:'gameOverlayHighscoresTitleCloseButton', 'class':'gameOverlayCloseButton material-icons'}, 'close', this.elements.gameOverlayHighscoresTitle);
					this.elements.gameOverlayHighscoresContent = this.createElement('div', {id:'gameOverlayHighscoresContent'}, '', this.elements.gameOverlayHighscores);
				this.elements.gameOverlayCredits = this.createElement('div', {id:'gameOverlayCredits'}, '', this.elements.gameOverlay);
					this.elements.gameOverlayCreditsTitle = this.createElement('h1', {id:'gameOverlayCreditsTitle'}, 'Credits', this.elements.gameOverlayCredits);
						this.elements.gameOverlayCreditsTitleCloseButton = this.createElement('button', {id:'gameOverlayCreditsTitleCloseButton', 'class':'gameOverlayCloseButton material-icons'}, 'close', this.elements.gameOverlayCreditsTitle);
					this.elements.gameOverlayCreditsAuthor = this.createElement('div', {id:'gameOverlayCreditsAuthor'}, '', this.elements.gameOverlayCredits);
						this.elements.gameOverlayCreditsAuthorTitle = this.createElement('h2', {id:'gameOverlayCreditsAuthorTitle'}, 'Developer', this.elements.gameOverlayCreditsAuthor);
						this.elements.gameOverlayCreditsAuthorText = this.createElement('h3', {id:'gameOverlayCreditsAuthorText'}, 'Adam Šárek (SAR0083)', this.elements.gameOverlayCreditsAuthor);
		
		// Playfield
		this.width = 10;
		this.height = 20;
		this.blockSize = 100;
		
		// Tetrominos
		this.generateNextTetromino();
		this.spawnTetromino();
		this.fall();
		
		// Overlay events
		this.elements.gameOverlayMenuButtonPlay.onclick = function(){
			game.start();
			game.hideElement(game.elements.gameOverlayMenu.getAttribute('id'));
		};
		this.elements.gameOverlayMenuButtonHighscores.onclick = function(){
			game.loadHighscores();
			game.showElement(game.elements.gameOverlayHighscores.getAttribute('id'));
		};
		this.elements.gameOverlayMenuButtonCredits.onclick = function(){game.showElement(game.elements.gameOverlayCredits.getAttribute('id'));};
		this.elements.gameOverlayHighscoresTitleCloseButton.onclick = function(){game.hideElement(game.elements.gameOverlayHighscores.getAttribute('id'));};
		this.elements.gameOverlayCreditsTitleCloseButton.onclick = function(){game.hideElement(game.elements.gameOverlayCredits.getAttribute('id'));};
		
		// Debug events
		const game = this;
		
		// UI events
		this.elements.gameLeftBottomMutedButton.onclick = function(){
			if(Object.values(game.sounds)[0].muted){
				game.unmute();
			}
			else{
				game.mute();
			}
		};
		this.elements.gameLeftBottomPauseButton.onclick = function(){
			if(game.state == 'ended'){
				game.start();
			}
			else if(game.state == 'paused'){
				game.play();
			}
			else{
				game.pause();
			}
		};
		this.elements.gameRightBottomRotateButton.onclick = function(){
			game.rotateRight();
		};
		this.elements.gameRightBottomDropButton.onclick = function(){
			game.drop();
		};
		
		// Touch events
		this.elements.gameCenterArea.ontouchstart = this.elements.gameCenterArea.ontouchmove = function(event){
			event.preventDefault();
			let mouseX = Math.floor(Math.ceil(event.touches[event.touches.length - 1].clientX - this.getBoundingClientRect().left) / this.getBoundingClientRect().width * game.width);
			
			let blockMinX = game.tetromino.matrix[0].length - 1;
			let blockMaxX = 0;
			for(let i = 0; i < game.tetromino.matrix.length; i++){
				blockMinX = (game.tetromino.matrix[i].indexOf(1) > -1 && game.tetromino.matrix[i].indexOf(1) < blockMinX ? game.tetromino.matrix[i].indexOf(1) : blockMinX);
				blockMaxX = (game.tetromino.matrix[i].lastIndexOf(1) > blockMaxX ? game.tetromino.matrix[i].lastIndexOf(1) : blockMaxX);
			}
			let blockCenterX = blockMinX + Math.floor((blockMaxX - blockMinX) / 2);
			let tetrominoCenterX = game.tetromino.position.x + blockCenterX;
			
			if(mouseX < tetrominoCenterX){
				for(let i = 0; i < tetrominoCenterX - mouseX; i++){
					game.moveLeft();
				}
			}
			else if(mouseX > tetrominoCenterX){
				for(let i = 0; i < mouseX - tetrominoCenterX; i++){
					game.moveRight();
				}
			}
		};
		
		// Mouse events
		this.elements.gameCenterArea.onmousemove = function(event){
			let mouseX = Math.floor(Math.ceil(event.clientX - this.getBoundingClientRect().left) / this.getBoundingClientRect().width * game.width);
			
			let blockMinX = game.tetromino.matrix[0].length - 1;
			let blockMaxX = 0;
			for(let i = 0; i < game.tetromino.matrix.length; i++){
				blockMinX = (game.tetromino.matrix[i].indexOf(1) > -1 && game.tetromino.matrix[i].indexOf(1) < blockMinX ? game.tetromino.matrix[i].indexOf(1) : blockMinX);
				blockMaxX = (game.tetromino.matrix[i].lastIndexOf(1) > blockMaxX ? game.tetromino.matrix[i].lastIndexOf(1) : blockMaxX);
			}
			let blockCenterX = blockMinX + Math.floor((blockMaxX - blockMinX) / 2);
			let tetrominoCenterX = game.tetromino.position.x + blockCenterX;
			
			if(mouseX < tetrominoCenterX){
				for(let i = 0; i < tetrominoCenterX - mouseX; i++){
					game.moveLeft();
				}
			}
			else if(mouseX > tetrominoCenterX){
				for(let i = 0; i < mouseX - tetrominoCenterX; i++){
					game.moveRight();
				}
			}
		};
		this.elements.gameCenterArea.onwheel = function(){
			game.rotateRight();
		};
		
		// Key events
		document.onkeydown = function(event){
			switch(event.key){
				case 'ArrowUp':
				case 'w':
					game.rotateRight();
					break;
				case 'ArrowDown':
				case 's':
					game.drop();
					break;
				case 'ArrowLeft':
				case 'a':
					game.moveLeft();
					break;
				case 'ArrowRight':
				case 'd':
					game.moveRight();
					break;
				case 'm':
					if(Object.values(game.sounds)[0].muted){
						game.unmute();
					}
					else{
						game.mute();
					}
					break;
				case 'p':
					if(game.state == 'ended'){
						game.start();
					}
					else if(game.state == 'paused'){
						game.play();
					}
					else{
						game.pause();
					}
					break;
			}
		};
	}
	
	// Start
	start(){
		this.setState('started');
		
		// Runs
		const runs = (localStorage.hasOwnProperty('runs') ? JSON.parse(localStorage.getItem('runs')) : []);
		const run = {score:0, level:1, time:0};
		runs.push(run);
		localStorage.setItem('runs', JSON.stringify(runs));
		this.elements.gameRightTopTimeContent.innerHTML = this.getTimeFormat(run.time);
		
		// Display score & level
		this.elements.gameLeftTopScoreContent.innerHTML = runs[runs.length - 1].score;
		this.elements.gameLeftTopLevelContent.innerHTML = runs[runs.length - 1].level;
		
		// Speed
		this.speed = levels[runs[runs.length - 1].level - 1].speed;
		
		// Playfield
		this.playfield = Array(this.height).fill().map(() => Array(this.width).fill(0));
		
		// Canvas
		this.context.clearRect(0, 0, this.width * this.blockSize, this.height * this.blockSize);
		
		this.play();
	}
	
	// Play
	play(){
		this.setState('resuming');
		this.elements.gameLeftBottomPauseButton.innerHTML = 'pause';
		this.resume();
	}
	
	// Resume
	resume(){
		this.elements.gameCenterInfo.classList.add('visible');
		this.elements.gameCenterInfo.classList.add('temporary');
		this.elements.gameCenterInfo.innerHTML = '3';
		const game = this;
		this.resumeInterval = setInterval(function(){
			const newCountdownNumber = parseInt(game.elements.gameCenterInfo.innerHTML) - 1;
			if(newCountdownNumber > 0){
				game.elements.gameCenterInfo.innerHTML = newCountdownNumber;
			}
			else if(newCountdownNumber == 0){
				game.elements.gameCenterInfo.innerHTML = 'GO';
			}
			else{
				game.elements.gameCenterInfo.classList.remove('visible');
				game.elements.gameCenterInfo.classList.remove('temporary');
				game.setState('playing');
				
				// Runs
				const runs = (localStorage.hasOwnProperty('runs') ? JSON.parse(localStorage.getItem('runs')) : []);
				runs[runs.length - 1].timeStart = Date.now();
				localStorage.setItem('runs', JSON.stringify(runs));
				
				// Time
				const run = runs[runs.length - 1];
				const time = (run.timeStart ? run.time + (Date.now() - run.timeStart) : run.time);
				game.elements.gameRightTopTimeContent.innerHTML = game.getTimeFormat(time);
				game.timeInterval = setInterval(function(){
					const runs = JSON.parse(localStorage.getItem('runs'));
					const run = runs[runs.length - 1];
					const time = (run.timeStart ? run.time + (Date.now() - run.timeStart) : run.time);
					game.elements.gameRightTopTimeContent.innerHTML = game.getTimeFormat(time);
				},1000);
				
				clearInterval(game.resumeInterval);
			}
		},1000);
	}
	
	// Pause
	pause(){
		this.setState('paused');
		this.elements.gameLeftBottomPauseButton.innerHTML = 'play_arrow';
		clearInterval(this.resumeInterval);
		this.elements.gameCenterInfo.innerHTML = 'Game Paused';
		this.elements.gameCenterInfo.classList.add('visible');
		game.elements.gameCenterInfo.classList.remove('temporary');
		
		// Runs
		const runs = (localStorage.hasOwnProperty('runs') ? JSON.parse(localStorage.getItem('runs')) : []);
		if(runs[runs.length - 1].hasOwnProperty('timeStart')){
			runs[runs.length - 1].time += Date.now() - runs[runs.length - 1].timeStart;
			delete runs[runs.length - 1].timeStart;
		}
		localStorage.setItem('runs', JSON.stringify(runs));
		
		// Time
		const run = runs[runs.length - 1];
		const time = (run.timeStart ? run.time + (Date.now() - run.timeStart) : run.time);
		game.elements.gameRightTopTimeContent.innerHTML = game.getTimeFormat(time);
		clearInterval(game.timeInterval);
	}
	
	// End
	end(){
		this.setState('ended');
		this.elements.gameLeftBottomPauseButton.innerHTML = 'replay';
		clearInterval(this.resumeInterval);
		this.elements.gameCenterInfo.innerHTML = 'Game Over';
		this.elements.gameCenterInfo.classList.add('visible');
		game.elements.gameCenterInfo.classList.remove('temporary');
		
		// Sound effects
		this.sounds.gameOver.currentTime = 0;
		this.sounds.gameOver.play();
		
		// Runs
		const runs = (localStorage.hasOwnProperty('runs') ? JSON.parse(localStorage.getItem('runs')) : []);
		runs[runs.length - 1].timeEnd = Date.now();
		runs[runs.length - 1].time += Date.now() - runs[runs.length - 1].timeStart;
		delete runs[runs.length - 1].timeStart;
		clearInterval(game.timeInterval);
		localStorage.setItem('runs', JSON.stringify(runs));
		
		// Show highscores
		this.loadHighscores();
		game.showElement(game.elements.gameOverlayHighscores.getAttribute('id'));
	}
	
	// Mute
	mute(){
		Object.values(this.sounds).forEach(sound => {
			sound.muted = true;
		});
		this.elements.gameLeftBottomMutedButton.innerHTML = 'volume_off';
		
		// Local Storage
		const game = (localStorage.hasOwnProperty('game') ? JSON.parse(localStorage.getItem('game')) : {});
		game.muted = true;
		localStorage.setItem('game',JSON.stringify(game));
	}
	
	// Unmute
	unmute(){
		Object.values(this.sounds).forEach(sound => {
			sound.muted = false;
		});
		this.elements.gameLeftBottomMutedButton.innerHTML = 'volume_up';
		
		// Local Storage
		const game = (localStorage.hasOwnProperty('game') ? JSON.parse(localStorage.getItem('game')) : {});
		game.muted = false;
		localStorage.setItem('game',JSON.stringify(game));
	}
	
	// State
	setState(state){
		this.state = state;
	}
	
	// Add next tetromino
	generateNextTetromino(){
		const tetrominoKeys = Object.keys(tetrominos);
		const nextTetrominoKey = tetrominoKeys[Math.floor(Math.random() * tetrominoKeys.length)];
		this.nextTetromino = new Tetromino(nextTetrominoKey);
		this.nextTetromino.setPosition({x:Math.floor((this.width - this.nextTetromino.matrix[0].length) / 2), y:(!this.nextTetromino.matrix[this.nextTetromino.matrix.length - 1].includes(1) ? 19 : 20)});
		this.elements.gameRightTopNextContentImage.className = this.nextTetromino.id;
		this.elements.gameRightTopNextContentImage.src = this.nextTetromino.fullImageSrc;
	}
	
	// Spawn tetromino
	spawnTetromino(){
		this.tetromino = this.nextTetromino;
		this.generateNextTetromino();
	}
	
	// Rotatition
	rotateLeft(){
		if(this.state == 'playing'){
			if(this.canPerform('rotateLeft')){
				this.clear();
				this.tetromino.rotateLeft();
				this.draw();
			}
		}
	}
	rotateRight(){
		if(this.state == 'playing'){
			if(this.canPerform('rotateRight')){
				this.clear();
				this.tetromino.rotateRight();
				this.draw();
			}
		}
	}
	
	// Movement
	moveDown(){
		if(this.state == 'playing'){
			if(this.canPerform('moveDown')){
				this.clear();
				this.tetromino.moveDown();
				this.draw();
			}
			else{
				this.place();
			}
		}
	}
	moveLeft(){
		if(this.state == 'playing'){
			if(this.canPerform('moveLeft')){
				this.clear();
				this.tetromino.moveLeft();
				this.draw();
			}
		}
	}
	moveRight(){
		if(this.state == 'playing'){
			if(this.canPerform('moveRight')){
				this.clear();
				this.tetromino.moveRight();
				this.draw();
			}
		}
	}
	
	// Drop
	drop(){
		if(this.state == 'playing'){
			while(this.canPerform('moveDown')){
				this.moveDown();
			}
			this.moveDown();
		}
	}
	
	// Can perform rotation/movement
	canPerform(canType){
		let copyPlayfield = JSON.parse(JSON.stringify(this.playfield));
		let checkPlayfieldSpawn = Array(4).fill().map(() => Array(this.width).fill(0))
		let checkPlayfield = copyPlayfield.concat(checkPlayfieldSpawn);
		
		for(let i = 0; i < this.tetromino.matrix.length; i++){
			for(let j = 0; j < this.tetromino.matrix[i].length; j++){
				if(this.tetromino.matrix[i][j] == 1){
					let tetrominoCheckX = this.tetromino.position.x + j;
					let tetrominoCheckY = this.tetromino.position.y + (this.tetromino.matrix.length - i) - 1;
					if(checkPlayfield[tetrominoCheckY] !== undefined && checkPlayfield[tetrominoCheckY][tetrominoCheckX] !== undefined){
						checkPlayfield[tetrominoCheckY][tetrominoCheckX] = 0;
					}
				}
			}
		}
		
		let checkTetromino = new Tetromino(this.tetromino.id);
		checkTetromino.matrix = JSON.parse(JSON.stringify(this.tetromino.matrix));
		checkTetromino.position = JSON.parse(JSON.stringify(this.tetromino.position));
		checkTetromino.rotation = JSON.parse(JSON.stringify(this.tetromino.rotation));
		
		switch(canType){
			case 'rotateLeft':
				checkTetromino.rotateLeft();
				break;
			case 'rotateRight':
				checkTetromino.rotateRight();
				break;
			case 'moveDown':
				checkTetromino.moveDown();
				break;
			case 'moveLeft':
				checkTetromino.moveLeft();
				break;
			case 'moveRight':
				checkTetromino.moveRight();
				break;
		}
		
		for(let i = 0; i < checkTetromino.matrix.length; i++){
			for(let j = 0; j < checkTetromino.matrix[i].length; j++){
				if(checkTetromino.matrix[i][j] == 1){
					let tetrominoCheckX = checkTetromino.position.x + j;
					let tetrominoCheckY = checkTetromino.position.y + (checkTetromino.matrix.length - i) - 1;
					if(checkPlayfield[tetrominoCheckY] === undefined || checkPlayfield[tetrominoCheckY][tetrominoCheckX] === undefined || checkPlayfield[tetrominoCheckY][tetrominoCheckX] == 1){
						return false;
					}
				}
			}
		}
		
		return true;
	}
	
	// Fall
	fall(){
		let game = this;
		
		setTimeout(function(){
			if(game.state == 'playing'){
				game.moveDown();
			}
			game.fall();
		}, 1000 / this.speed);
	}
	
	// Place
	place(){
		if(this.tetromino.position.y >= this.height - 1){
			this.end();
		}
		else{
			// Sound effects
			this.sounds.drop.currentTime = 0;
			this.sounds.drop.play();
			
			for(let i = 0; i < this.playfield.length; i++){
				if(this.playfield[i].every(value => value == 1)){
					const runs = (localStorage.hasOwnProperty('runs') ? JSON.parse(localStorage.getItem('runs')) : []);
					
					// Add score
					runs[runs.length - 1].score += 10;
					
					// Add level
					runs[runs.length - 1].level += (levels[runs[runs.length - 1].level] && runs[runs.length - 1].score >= levels[runs[runs.length - 1].level].minScore ? 1 : 0);
					
					// Change speed
					this.speed = levels[runs[runs.length - 1].level - 1].speed;
					
					// Save score & level
					localStorage.setItem('runs', JSON.stringify(runs));
					
					// Display score & level
					this.elements.gameLeftTopScoreContent.innerHTML = runs[runs.length - 1].score;
					this.elements.gameLeftTopLevelContent.innerHTML = runs[runs.length - 1].level;
					
					// Move down lines above
					let imageData = this.context.getImageData(0, 0, this.width * this.blockSize, (this.height - (i + 1)) * this.blockSize);
					this.context.putImageData(imageData, 0, this.blockSize);
					for(let j = i; j < this.playfield.length-1; j++){
						this.playfield[j] = JSON.parse(JSON.stringify(this.playfield[j+1]));
					}
					i--;
					
					// Sound effects
					this.sounds.line.currentTime = 0;
					this.sounds.line.play();
				}
			}
			this.spawnTetromino();
		}
	}
	
	// Clear
	clear(){
		for(let i = 0; i < this.tetromino.matrix.length; i++){
			for(let j = 0; j < this.tetromino.matrix[i].length; j++){
				if(this.tetromino.matrix[i][j] == 1){
					// Clear from playfield matrix
					const playfieldTetrominoBlockX = this.tetromino.position.x + j;
					const playfieldTetrominoBlockY = this.tetromino.position.y + (this.tetromino.matrix.length - i) - 1;
					if(this.playfield[playfieldTetrominoBlockY] !== undefined && this.playfield[playfieldTetrominoBlockY][playfieldTetrominoBlockX] !== undefined){
						this.playfield[playfieldTetrominoBlockY][playfieldTetrominoBlockX] = 0;
					}
					
					// Clear from canvas
					const canvasTetrominoBlockX = this.tetromino.position.x + j;
					const canvasTetrominoBlockY = (this.height - this.tetromino.position.y - this.tetromino.matrix.length) + i;
					this.context.clearRect(canvasTetrominoBlockX * this.blockSize, canvasTetrominoBlockY * this.blockSize, this.blockSize, this.blockSize);
				}
			}
		}
	}
	
	// Draw
	draw(){
		const tetromino = this;
		if(!this.tetromino.blockImage.complete){
			this.tetromino.blockImage.onload = function(){
				tetromino.draw();
			};
		}
		else{
			for(let i = 0; i < this.tetromino.matrix.length; i++){
				for(let j = 0; j < this.tetromino.matrix[i].length; j++){
					if(this.tetromino.matrix[i][j] == 1){
						// Place on playfield matrix
						const playfieldTetrominoBlockX = this.tetromino.position.x + j;
						const playfieldTetrominoBlockY = this.tetromino.position.y + (this.tetromino.matrix.length - i) - 1;
						if(this.playfield[playfieldTetrominoBlockY] !== undefined && this.playfield[playfieldTetrominoBlockY][playfieldTetrominoBlockX] !== undefined){
							this.playfield[playfieldTetrominoBlockY][playfieldTetrominoBlockX] = 1;
						}
						
						// Draw on canvas
						const canvasTetrominoBlockX = this.tetromino.position.x + j;
						const canvasTetrominoBlockY = (this.height - this.tetromino.position.y - this.tetromino.matrix.length) + i;
						this.context.drawImage(this.tetromino.blockImage, canvasTetrominoBlockX * this.blockSize, canvasTetrominoBlockY * this.blockSize, this.blockSize, this.blockSize);
					}
				}
			}
		}
	}
	
	// Highscores
	loadHighscores(){
		// Clear previous data
		this.elements.gameOverlayHighscoresContent.innerHTML = '';
		
		// Prepare header
		this.elements.gameOverlayHighscoresContents = [];
		this.elements.gameOverlayHighscoresContents.push(this.createElement('div', {'class':'gameOverlayHighscoresContentBlock'}, 'Rank', this.elements.gameOverlayHighscoresContent));
		this.elements.gameOverlayHighscoresContents.push(this.createElement('div', {'class':'gameOverlayHighscoresContentBlock'}, 'Score', this.elements.gameOverlayHighscoresContent));
		this.elements.gameOverlayHighscoresContents.push(this.createElement('div', {'class':'gameOverlayHighscoresContentBlock'}, 'Time', this.elements.gameOverlayHighscoresContent));
		
		// Sort runs
		const runs = (localStorage.hasOwnProperty('runs') ? JSON.parse(localStorage.getItem('runs')) : []);
		if(runs.length > 0){
			runs.forEach(a => {a.last = false;});
			runs[runs.length - 1].last = true;
			
			runs.sort((a, b) => (a.score < b.score) ? 1 : ((a.score === b.score) ? ((a.time > b.time) ? 1 : ((a.time === b.time) ? ((a.timeEnd > b.timeEnd) ? 1 : -1) : -1)) : -1));
			
			// Display highscores
			let rank = 1;
			let lastRunEl;
			runs.forEach(run => {
				let contents = [rank, run.score, this.getTimeFormat(run.time)];
				contents.forEach(content => {
					if(run.last){
						lastRunEl = this.createElement('div', {'class':'gameOverlayHighscoresContentBlock last'}, content, this.elements.gameOverlayHighscoresContent);
						this.elements.gameOverlayHighscoresContents.push(lastRunEl);
					}
					else{
						this.elements.gameOverlayHighscoresContents.push(this.createElement('div', {'class':'gameOverlayHighscoresContentBlock'}, content, this.elements.gameOverlayHighscoresContent));
					}
				});
				rank++;
			});
			
			// Scroll to last run
			let game = this;
			game.elements.gameOverlayHighscoresContent.scrollTo(0,0);
			setTimeout(function(){
				let viewport = game.elements.gameOverlayHighscoresContent.offsetHeight - 40;
				let top = lastRunEl.getBoundingClientRect().top + 40 + (lastRunEl.getBoundingClientRect().height - viewport) / 2;
				game.elements.gameOverlayHighscoresContent.scrollTo({top: top, left: 0, behavior: 'smooth'});
			}, 500);
		}
	}
	
	// Time
	getTimeFormat(time){
		const h = Math.floor(time / 3600000);
		const m = Math.floor(time / 60000) - h * 60;
		const s = Math.floor(time / 1000) - m * 60 - h * 3600;
		
		const timeH = (h == 0 ? '' : h + ':');
		const timeM = (h == 0 ? m : (m < 10 ? '0' + m : m)) + ':';
		const timeS = (s < 10 ? '0' + s : s);
		
		const timeFormat = timeH + timeM + timeS;
		
		return timeFormat;
	}
	
	// Canvas
	setCanvas(canvas){
		// Add canvas
		this.elements.gameCenterCanvas = canvas;
		this.elements.gameCenterCanvas.setAttribute('id','gameCenterCanvas');
		this.elements.gameCenterCanvas.width = this.blockSize * this.width;
		this.elements.gameCenterCanvas.height = this.blockSize * this.height;
		this.elements.gameCenterCanvas.style.backgroundSize = (100 / this.width) + '% ' + (100 / this.height) + '%';
		this.elements.gameCenterCanvas.parentNode.insertBefore(this.elements.game, this.elements.gameCenterCanvas);
		this.elements.gameCenter.prepend(this.elements.gameCenterCanvas);
		
		// Set map context
		this.context = canvas.getContext('2d');
	}
	
	// Resize
	resize(){
		// Resize game
		let gameWidth, gameHeight;
		if(window.innerWidth / window.innerHeight > this.aspectRatio){
			gameWidth = window.innerHeight * this.aspectRatio;
			gameHeight = window.innerHeight;
		}
		else{
			gameWidth = window.innerWidth;
			gameHeight = window.innerWidth / this.aspectRatio;
		}
		this.elements.game.style.width = gameWidth + 'px';
		this.elements.game.style.height = gameHeight + 'px';
	}
	
	// Show/Hide element
	showElement(id){this.elements[id].classList.add('visible');}
	hideElement(id){this.elements[id].classList.remove('visible');}
	
	// Create element
	createElement(tagName, attributes={}, content='', parentNode=undefined){
		const element = document.createElement(tagName);
		
		// Set all given attributes
		Object.keys(attributes).forEach(key => {
			element.setAttribute(key, attributes[key]);
		});
		
		// Set content
		element.innerHTML = content;
		
		// Append to given parent
		if(parentNode){
			parentNode.append(element);
		}
		
		return element;
	}
}

// Variables
const game = new Game();

// Events
document.addEventListener('DOMContentLoaded', (event) => {
	game.setCanvas(document.getElementById('game'));
});
window.addEventListener('load', (event) => {
	game.resize();
});
window.addEventListener('resize', (event) => {
	game.resize();
});