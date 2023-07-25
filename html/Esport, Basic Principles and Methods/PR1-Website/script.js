/*-- Functions --*/

function resize() {
	// Stream
	const header = document.querySelector('.header');
	const stream = document.querySelector('.stream');
	const streamPlayer = document.querySelector('.stream-player');
	
	const streamMaxWidth = 1280;
	const streamMaxHeight = window.innerHeight - header.offsetHeight;
	
	if(stream) {
		stream.classList.remove('landscape');
		streamPlayer.style.height = '100%';
		
		if(window.innerWidth < streamMaxWidth) {
			stream.classList.remove('landscape');
			streamPlayer.style.height = stream.clientWidth / 16 * 9 + 'px';
			
			if(stream.clientHeight >= streamMaxHeight) {
				stream.classList.add('landscape');
				streamPlayer.style.height = '100%';
			}
		}
	}
}

function rotateCarousels() {
	const carousels = document.querySelectorAll('.carousel');
	let i = 0;
	carousels.forEach((carousel) => {
		const carouselButtonLeft = carousel.querySelector('.carousel-button-left');
		const carouselButtonRight = carousel.querySelector('.carousel-button-right');
		
		carouselButtonLeft.setAttribute('onclick', 'rotateCarousel(' + i + ', -1);');
		carouselButtonRight.setAttribute('onclick', 'rotateCarousel(' + i + ', 1);');
		
		rotateCarousel(i);
		
		i++;
	});
}

function rotateCarousel(carouselId, step = 0) {
	const carousels = document.querySelectorAll('.carousel');
	carousels.forEach((carousel) => {
		const carouselList = carousel.querySelector('.carousel-list');
		
		const carouselListItems = carouselList.querySelectorAll('.carousel-list-item');
		
		let nextActiveId = 0;
		for(let i = 0; i < carouselListItems.length; i++) {
			if(carouselListItems[i].classList.contains('active')) {
				carouselListItems[i].classList.remove('active');
				
				if(step >= 0) { nextActiveId = (i == carouselListItems.length - 1 ? 0 : i + 1); }
				else { nextActiveId = (i == 0 ? carouselListItems.length - 1 : i - 1); }
			}
		}
		
		carouselListItems[nextActiveId].classList.add('active');
		
		if(carouselTimeouts[carouselId]) { clearTimeout(carouselTimeouts[carouselId]); }
		
		carouselTimeouts[carouselId] = setTimeout(() => {
			rotateCarousel(carouselId);
		}, 10000 + 1000);
	});
}

/*-- Events --*/

// Resize
window.onresize = (event) => {
	resize();
};

// Variables
let carouselTimeouts = [];

// Page
let page = window.location.pathname.split('/');
page = page[page.length - 1].split('.html')[0];

/*-- JSON Data --*/

// Data - settings
const data = {
	"team": {
		"blue": {
			"title": "Team MoonBase",
			"members": [
				"Jan \"tzej\" Moták",
				"Dominik \"dminik\" Meca",
				"Marek \"JAVA\" Svatoš",
				"Patrik \"REACT\" Meixner",
				"Lukáš \"maslo\" Maloň"
			],
			"trainings": [
				{
					"week_day": "Pondělí",
					"time": "10:45 - 12:15",
					"game": "Dota 2"
				}
			],
			"sponsors": [
				{
					"title": "Logitech",
					"image": "img/sponsor_logitech.png"
				},
				{
					"title": "BenQ",
					"image": "img/sponsor_benq.png"
				},
				{
					"title": "Twitch",
					"image": "img/sponsor_twitch.png"
				},
				{
					"title": "RedBull",
					"image": "img/sponsor_redbull.png"
				},
				{
					"title": "Nike",
					"image": "img/sponsor_nike.png"
				},
				{
					"title": "Alienware",
					"image": "img/sponsor_alienware.png"
				}
			]
		},
		"red": {
			"title": "VŠB - Random Bois",
			"members": [
				"Michal \"Ajmitch\" Ščepka",
				"Jakub \"Dzain\" Dzian",
				"Josef \"granders\" Micak",
				"Ľubomír \"lubsar\" Hlavko",
				"David \"CalmKeeper\" Vrobel"
			],
			"trainings": [
				{
					"week_day": "Pondělí",
					"time": "10:45 - 12:15",
					"game": "Dota 2"
				}
			],
			"sponsors": [
				{
					"title": "VŠB",
					"image": "img/sponsor_vsb.png"
				},
				{
					"title": "CZC",
					"image": "img/sponsor_czc.png"
				},
				{
					"title": "RedBull",
					"image": "img/sponsor_redbull.png"
				}
			]
		}
	},
	"stream": {
		"channel": "esportvsb",
		"player": 1,
		"chat": 1,
		"live": 1
	},
	"news": [
		{
			"background": "img/news_1.png",
			"title": "Random but still good",
			"content": "Dnes 5.10.2020 se odehrál úplně první zápas v rámci prvního esport předmětu u nás. Týmy a hra byly vybrány v prvním cvičení a o divácky atraktivní utkání se mimo skvělé herní nasazení postarali Esport technici a samozřejmě výborný komentář Cubasse. Červený tým prokázal vyšší znalosti a jednoznačně porazil modrý tým. Není se však za co stydět, jelikož se i mezi nimi může nacházet možný budoucí profesionální hráč.<hr><b>Úvodní fáze:&nbsp;</b><a href=\"https://www.twitch.tv/videos/761247376\">záznam streamu</a><br><b>Samotné utkání:&nbsp;</b><a href=\"https://www.twitch.tv/videos/761252408\">záznam streamu</a>"
		},
		{
			"background": "img/news_2.jpg",
			"title": "Mimořádná opatření s ohledem na šíření koronavirovou epidemii COVID-19",
			"content": "Zákaz osobní přítomnosti studentů na výuce platný od 23. 9. do 31. 10. 2020"
		},
		{
			"background": "img/news_3.jpg",
			"title": "První esportová učebna v ČR!",
			"content": "Právě teď začínáme! Jsme oficiálně první univerzita co učí esport! ❤️"
		},
		{
			"background": "img/news_4.png",
			"title": "Petr „CzechCloud“ Žalud se stává partnerem Riot Games",
			"content": "Jeden z nejdéle vysílajících influencerů a hráčů na tuzemské herní scéně, Petr „CzechCloud“ Žalud, se stává novou oficiální tváří společnosti Riot Games. Dle jeho slov se můžeme těšit na spoustu lokálních turnajů pro navýšení herní komunity."
		},
		{
			"background": "img/news_5.png",
			"title": "Píše se esport, nebo e-sport? Mapujeme vývoj slova",
			"content": "Vývoj používání jednotlivých podob slova a trendy v českém i mezinárodním prostoru mapuje nová studie České asociace esportu. Její závěry ukazují, že dříve se spíše používala varianta „e-sport“, zatímco v současnosti se stále častěji objevuje „esport“. Česká asociace esportu proto doporučuje používat modernější verzi slova."
		}
	],
	"match": [
		{
			"blue": 17,
			"red": 63,
			"game": "Dota 2"
		}
	]
};

// Data / attributes - settings
const dataAttr = {
	html: 'innerHTML',
	title: 'title'
};
const dataAttrKeys = Object.keys(dataAttr);
const dataAttrValues = Object.values(dataAttr);

// Data / attributes - use
for(let i = 0; i < dataAttrKeys.length; i++) {
	const dataElements = document.querySelectorAll('[data-' + dataAttrKeys[i] + ']');
	dataElements.forEach((dataElement) => {
		const dataValue = dataElement.dataset[dataAttrKeys[i]];
		let dataValueContent = '';
		
		if(!dataValue.includes('-')) {
			dataValueContent = (data[dataValue] !== undefined ? data[dataValue] : '{' + dataValue + '}');
		}
		else {
			const dataValueParts = dataValue.split('-');
			let dataValuePartContent = data;
			
			let j;
			for(j = 0; j < dataValueParts.length; j++) {
				const dataValuePart = dataValueParts[j];
				if(dataValuePartContent[dataValuePart] === undefined) { break; }
				
				dataValuePartContent = dataValuePartContent[dataValuePart];
			}
			
			dataValueContent = (j == dataValueParts.length ? dataValuePartContent : '{' + dataValue + '}');
		}
		
		dataElement[dataAttrValues[i]] = dataValueContent;
	});
}

// Data / stream - use
const stream = document.querySelector('.stream');
const streamPlayer = document.querySelector('.stream-player');
const streamChat = document.querySelector('.stream-chat');

if(stream) {
	streamPlayer.src = 'https://player.twitch.tv/?channel=' + data.stream.channel + '&parent=esport-vsb.tk&muted=true';
	streamChat.src = 'https://www.twitch.tv/embed/' + data.stream.channel + '/chat?parent=esport-vsb.tk&darkpopout';
	
	if(data.stream.player == 1) { stream.classList.add('player-enabled'); }
	if(data.stream.chat == 1) { stream.classList.add('chat-enabled'); }
	if(data.stream.live == 1) { stream.classList.add('live'); }
}

// Data / news - use

const news = document.getElementById('news');
if(news) {
	data.news.forEach((dataNews) => {
		const dataNewsItem = document.createElement('li');
		dataNewsItem.className = 'carousel-list-item list-item';
		
		const dataNewsItemBackground = document.createElement('div');
		dataNewsItemBackground.className = 'news-background';
		if(dataNews.background) { dataNewsItemBackground.style.backgroundImage = 'url(' + dataNews.background + ')'; }
		dataNewsItem.appendChild(dataNewsItemBackground);
		
		const dataNewsItemBody = document.createElement('div');
		dataNewsItemBody.className = 'news-body carousel-content-width';
		dataNewsItem.appendChild(dataNewsItemBody);
		
		const dataNewsItemBodyTitle = document.createElement('div');
		dataNewsItemBodyTitle.className = 'news-body-title';
		dataNewsItemBodyTitle.innerHTML = dataNews.title;
		dataNewsItemBody.appendChild(dataNewsItemBodyTitle);
		
		const dataNewsItemBodyContent = document.createElement('div');
		dataNewsItemBodyContent.className = 'news-body-content';
		dataNewsItemBodyContent.innerHTML = dataNews.content;
		dataNewsItemBody.appendChild(dataNewsItemBodyContent);
		
		news.appendChild(dataNewsItem);
	});
}

// Data / members - use
let dataMembers = [];
if(page == 'blue') {
	dataMembers = data.team.blue.members;
}
else if(page == 'red') {
	dataMembers = data.team.red.members;
}

const members = document.getElementById('members');
let i = 0;
dataMembers.forEach((dataMember) => {
	const dataMembersItem = document.createElement('li');
	dataMembersItem.className = 'members-list-item list-item';
	
	const dataMembersItemPlayer = document.createElement('div');
	dataMembersItemPlayer.className = (i == 0 ? 'leader-background' : '');
	dataMembersItemPlayer.innerHTML = dataMember;
	dataMembersItem.appendChild(dataMembersItemPlayer);
	
	members.appendChild(dataMembersItem);
	
	i++;
});

// Data / trainings - use
let dataTrainings = [];
if(page == 'blue') {
	dataTrainings = data.team.blue.trainings;
}
else if(page == 'red') {
	dataTrainings = data.team.red.trainings;
}

const trainings = document.getElementById('trainings');
dataTrainings.forEach((dataTraining) => {
	const dataTrainingsItem = document.createElement('li');
	dataTrainingsItem.className = 'trainings-list-item list-item';
	
	const dataTrainingsItemWeekDay = document.createElement('div');
	dataTrainingsItemWeekDay.innerHTML = dataTraining.week_day;
	dataTrainingsItem.appendChild(dataTrainingsItemWeekDay);
	
	const dataTrainingsItemTime = document.createElement('div');
	dataTrainingsItemTime.innerHTML = dataTraining.time;
	dataTrainingsItem.appendChild(dataTrainingsItemTime);
	
	const dataTrainingsItemGame = document.createElement('div');
	dataTrainingsItemGame.innerHTML = dataTraining.game;
	dataTrainingsItem.appendChild(dataTrainingsItemGame);
	
	trainings.appendChild(dataTrainingsItem);
});

// Data / match - use
let dataMatchResultScoreClass = [
	'team-blue-background',
	'team-red-background',
	'draw-background'
];
if(page == 'blue') {
	dataMatchResultScoreClass = [
		'win-background',
		'lose-background',
		'draw-background'
	];
}
else if(page == 'red') {
	dataMatchResultScoreClass = [
		'lose-background',
		'win-background',
		'draw-background'
	];
}

const results = document.getElementById('results');
data.match.forEach((dataMatch) => {
	const dataMatchResult = document.createElement('li');
	dataMatchResult.className = 'results-list-item list-item';
	
	const dataMatchResultScore = document.createElement('div');
	dataMatchResultScore.className = (dataMatch.blue > dataMatch.red ? dataMatchResultScoreClass[0] : (dataMatch.blue < dataMatch.red ? dataMatchResultScoreClass[1] : dataMatchResultScoreClass[2]));
	dataMatchResult.appendChild(dataMatchResultScore);
	
	const dataMatchResultScoreBlue = document.createElement('span');
	dataMatchResultScoreBlue.innerHTML = dataMatch.blue;
	
	const dataMatchResultScoreRed = document.createElement('span');
	dataMatchResultScoreRed.innerHTML = dataMatch.red;
	
	const dataMatchResultGame = document.createElement('div');
	dataMatchResultGame.innerHTML = dataMatch.game;
	dataMatchResult.appendChild(dataMatchResultGame);
	
	if(page == 'red') {
		dataMatchResultScore.appendChild(dataMatchResultScoreRed);
		dataMatchResultScore.innerHTML += ':';
		dataMatchResultScore.appendChild(dataMatchResultScoreBlue);
	}
	else {
		dataMatchResultScore.appendChild(dataMatchResultScoreBlue);
		dataMatchResultScore.innerHTML += ':';
		dataMatchResultScore.appendChild(dataMatchResultScoreRed);
	}
	
	results.appendChild(dataMatchResult);
});

// Data / sponsors - use
let dataSponsors = [];
if(page == 'blue') {
	dataSponsors = data.team.blue.sponsors;
}
else if(page == 'red') {
	dataSponsors = data.team.red.sponsors;
}

const sponsors = document.getElementById('sponsors');
dataSponsors.forEach((dataSponsor) => {
	const dataSponsorsItem = document.createElement('li');
	dataSponsorsItem.className = 'sponsors-list-item list-item';
	
	const dataSponsorsItemImage = document.createElement('img');
	dataSponsorsItemImage.src = dataSponsor.image;
	dataSponsorsItemImage.title = dataSponsor.title;
	dataSponsorsItem.appendChild(dataSponsorsItemImage);
	
	sponsors.appendChild(dataSponsorsItem);
});

// Start
resize();
rotateCarousels();