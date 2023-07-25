// Currency names
const currencyName = {
	AED: "United Arab Emirates dirham",
	AFN: "Afghan afghani",
	ALL: "Albanian lek",
	AMD: "Armenian dram",
	ANG: "Netherlands Antillean guilder",
	AOA: "Angolan kwanza",
	ARS: "Argentine peso",
	AUD: "Australian dollar",
	AWG: "Aruban florin",
	AZN: "Azerbaijani manat",
	BAM: "Bosnia and Herzegovina convertible mark",
	BBD: "Barbados dollar",
	BDT: "Bangladeshi taka",
	BGN: "Bulgarian lev",
	BHD: "Bahraini dinar",
	BIF: "Burundian franc",
	BMD: "Bermudian dollar",
	BND: "Brunei dollar",
	BOB: "Boliviano",
	BRL: "Brazilian real",
	BSD: "Bahamian dollar",
	BTN: "Bhutanese ngultrum",
	BWP: "Botswana pula",
	BYN: "New Belarusian ruble",
	BYR: "Belarusian ruble",
	BZD: "Belize dollar",
	CAD: "Canadian dollar",
	CDF: "Congolese franc",
	CHF: "Swiss franc",
	CLF: "Unidad de Fomento",
	CLP: "Chilean peso",
	CNY: "Chinese yuan",
	COP: "Colombian peso",
	CRC: "Costa Rican colon",
	CUC: "Cuban convertible peso",
	CUP: "Cuban peso",
	CVE: "Cape Verde escudo",
	CZK: "Czech koruna",
	DJF: "Djiboutian franc",
	DKK: "Danish krone",
	DOP: "Dominican peso",
	DZD: "Algerian dinar",
	EGP: "Egyptian pound",
	ERN: "Eritrean nakfa",
	ETB: "Ethiopian birr",
	EUR: "Euro",
	FJD: "Fiji dollar",
	FKP: "Falkland Islands pound",
	GBP: "Pound sterling",
	GEL: "Georgian lari",
	GHS: "Ghanaian cedi",
	GIP: "Gibraltar pound",
	GMD: "Gambian dalasi",
	GNF: "Guinean franc",
	GTQ: "Guatemalan quetzal",
	GYD: "Guyanese dollar",
	HKD: "Hong Kong dollar",
	HNL: "Honduran lempira",
	HRK: "Croatian kuna",
	HTG: "Haitian gourde",
	HUF: "Hungarian forint",
	IDR: "Indonesian rupiah",
	ILS: "Israeli new shekel",
	INR: "Indian rupee",
	IQD: "Iraqi dinar",
	IRR: "Iranian rial",
	ISK: "Icelandic króna",
	JMD: "Jamaican dollar",
	JOD: "Jordanian dinar",
	JPY: "Japanese yen",
	KES: "Kenyan shilling",
	KGS: "Kyrgyzstani som",
	KHR: "Cambodian riel",
	KMF: "Comoro franc",
	KPW: "North Korean won",
	KRW: "South Korean won",
	KWD: "Kuwaiti dinar",
	KYD: "Cayman Islands dollar",
	KZT: "Kazakhstani tenge",
	LAK: "Lao kip",
	LBP: "Lebanese pound",
	LKR: "Sri Lankan rupee",
	LRD: "Liberian dollar",
	LSL: "Lesotho loti",
	LYD: "Libyan dinar",
	MAD: "Moroccan dirham",
	MDL: "Moldovan leu",
	MGA: "Malagasy ariary",
	MKD: "Macedonian denar",
	MMK: "Myanmar kyat",
	MNT: "Mongolian tögrög",
	MOP: "Macanese pataca",
	MRO: "Mauritanian ouguiya",
	MUR: "Mauritian rupee",
	MVR: "Maldivian rufiyaa",
	MWK: "Malawian kwacha",
	MXN: "Mexican peso",
	MXV: "Mexican Unidad de Inversion",
	MYR: "Malaysian ringgit",
	MZN: "Mozambican metical",
	NAD: "Namibian dollar",
	NGN: "Nigerian naira",
	NIO: "Nicaraguan córdoba",
	NOK: "Norwegian krone",
	NPR: "Nepalese rupee",
	NZD: "New Zealand dollar",
	OMR: "Omani rial",
	PAB: "Panamanian balboa",
	PEN: "Peruvian Sol",
	PGK: "Papua New Guinean kina",
	PHP: "Philippine peso",
	PKR: "Pakistani rupee",
	PLN: "Polish złoty",
	PYG: "Paraguayan guaraní",
	QAR: "Qatari riyal",
	RON: "Romanian leu",
	RSD: "Serbian dinar",
	RUB: "Russian ruble",
	RWF: "Rwandan franc",
	SAR: "Saudi riyal",
	SBD: "Solomon Islands dollar",
	SCR: "Seychelles rupee",
	SDG: "Sudanese pound",
	SEK: "Swedish krona",
	SGD: "Singapore dollar",
	SHP: "Saint Helena pound",
	SLL: "Sierra Leonean leone",
	SOS: "Somali shilling",
	SRD: "Surinamese dollar",
	SSP: "South Sudanese pound",
	STD: "São Tomé and Príncipe dobra",
	SVC: "Salvadoran colón",
	SYP: "Syrian pound",
	SZL: "Swazi lilangeni",
	THB: "Thai baht",
	TJS: "Tajikistani somoni",
	TMT: "Turkmenistani manat",
	TND: "Tunisian dinar",
	TOP: "Tongan paʻanga",
	TRY: "Turkish lira",
	TTD: "Trinidad and Tobago dollar",
	TWD: "New Taiwan dollar",
	TZS: "Tanzanian shilling",
	UAH: "Ukrainian hryvnia",
	UGX: "Ugandan shilling",
	USD: "United States dollar",
	UYI: "Uruguay Peso en Unidades Indexadas",
	UYU: "Uruguayan peso",
	UZS: "Uzbekistan som",
	VEF: "Venezuelan bolívar",
	VND: "Vietnamese đồng",
	VUV: "Vanuatu vatu",
	WST: "Samoan tala",
	XAF: "Central African CFA franc",
	XCD: "East Caribbean dollar",
	XOF: "West African CFA franc",
	XPF: "CFP franc",
	XXX: "No currency",
	YER: "Yemeni rial",
	ZAR: "South African rand",
	ZMW: "Zambian kwacha",
	ZWL: "Zimbabwean dollar"
};

// Initial currencies
let currencyFrom = 'EUR';
let currencyTo = 'CZK';

// Elements
const currencyFromSelect = document.querySelector('.currencyFromSelect');
const currencyToSelect = document.querySelector('.currencyToSelect');
const currencyFromText = document.querySelector('.currencyFromText');
const amountInput = document.querySelector('.amountInput');
const dateInput = document.querySelector('.dateInput');
const result = document.querySelector('.result');

// Get initial dates
const date = new Date();
let useDate = new Date(date.getTime() - date.getTimezoneOffset() * 60000);
useDate.setFullYear(useDate.getFullYear() - 1);
const startDate = useDate.toJSON().split('T')[0];
useDate.setFullYear(useDate.getFullYear() + 1);
const endDate = useDate.toJSON().split('T')[0];
let lastDate = null;

// Set initial date of exchange
dateInput.value = endDate;
dateInput.max = endDate;

// Init chart
google.charts.load('current', {'packages':['corechart']});
google.charts.setOnLoadCallback(drawChart);

// Fetch currency rate data
let rawData = null;
let appData = {
	dates: [],
	currencies: []
};
let feedData = null;
fetch(`https://api.exchangeratesapi.io/history?start_at=${startDate}&end_at=${endDate}`, {
	method: 'GET',
	headers: {
		'Content-Type': 'text/plain'
	}
})
.then(response => response.json())
.then(responseData => {
	rawData = responseData;
	
	// Parse raw data
	appData.dates = Object.keys(rawData.rates);
	appData.dates.sort();
	appData.currencies = Object.keys(rawData.rates[appData.dates[0]]);
	appData.currencies.push(rawData.base);
	appData.currencies.sort();
	
	// Set min date
	dateInput.min = appData.dates[0];
	
	// Get valid use date
	updateUseDate();
	
	// Events
	currencyFromSelect.onchange = (event) => {
		currencyFromText.innerHTML = currencyFrom = event.target.value;
		calculate();
		updateChart();
	};
	currencyToSelect.onchange = (event) => {
		currencyTo = event.target.value;
		calculate();
		updateChart();
	};
	amountInput.oninput = (event) => {
		if(event.target.value < event.target.min) { event.target.value = 0; }
		calculate();
	};
	dateInput.onchange = (event) => {
		updateUseDate();
		calculate();
	};
	
	// Add options to currency selects
	appData.currencies.forEach((currency) => {
		let option = document.createElement('option');
		option.value = currency;
		option.innerHTML = `(${currency}) ${currencyName[currency]}`;
		if(currency == currencyFrom) { option.setAttribute('selected', ''); }
		currencyFromSelect.appendChild(option);
		
		option = document.createElement('option');
		option.value = currency;
		option.innerHTML = `(${currency}) ${currencyName[currency]}`;
		if(currency == currencyTo) { option.setAttribute('selected', ''); }
		currencyToSelect.appendChild(option);
	});
	currencyFromSelect.dispatchEvent(new Event('change'));
	currencyToSelect.dispatchEvent(new Event('change'));
	
	// Draw chart on resize
	window.onresize = drawChart;
});

function updateUseDate() {
	const dateInputMinDate = new Date(dateInput.getAttribute('min'));
	const dateInputMaxDate = new Date(dateInput.getAttribute('max'));
	
	useDate = new Date(dateInput.value);
	if(useDate.getTime() < dateInputMinDate.getTime() || useDate > dateInputMaxDate.getTime()) {
		useDate = lastDate;
	}
	lastDate = useDate;
	
	useDate = getLastWorkingDate(useDate);
}

function getLastWorkingDate(tryDate) {
	for(let i = 0; i < appData.dates.length; i++) {
		if(appData.dates.includes(tryDate.toJSON().split('T')[0])) {
			break;
		}
		tryDate.setDate(tryDate.getDate() - 1);
	}
	
	return tryDate;
}

function calculate() {
	const conversionTo = (currencyTo == rawData.base ? 1 : rawData.rates[useDate.toJSON().split('T')[0]][currencyTo]);
	const conversionFrom = (currencyFrom == rawData.base ? 1 : rawData.rates[useDate.toJSON().split('T')[0]][currencyFrom]);
	const conversionRate = conversionTo / conversionFrom;
	result.innerHTML = (amountInput.value * conversionRate).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2, style: 'currency', currency: currencyTo });
}

function updateChart() {
	// Feed data for chart
	feedData = [
		['Date', currencyTo]
	];
	
	const tryDate = new Date(endDate);
	tryDate.setDate(tryDate.getDate() - 7);
	
	// Last 7 days
	const dayCount = 7;
	for(let i = 0; i < dayCount; i++) {
		tryDate.setDate(tryDate.getDate() + 1);
		const workDate = getLastWorkingDate(new Date(tryDate.getTime()));
		
		const conversionTo = (currencyTo == rawData.base ? 1 : rawData.rates[workDate.toJSON().split('T')[0]][currencyTo]);
		const conversionFrom = (currencyFrom == rawData.base ? 1 : rawData.rates[workDate.toJSON().split('T')[0]][currencyFrom]);
		const conversionRate = conversionTo / conversionFrom;
		
		feedData[i + 1] = [tryDate.toLocaleDateString(), conversionRate];
	}
	
	drawChart();
}

function drawChart() {
	if(google && google.visualization && google.visualization.arrayToDataTable && feedData != null) {
		document.getElementById('currencyRateChart').innerHTML = '';
		
		const chartData = google.visualization.arrayToDataTable(feedData);
		
		const chartTextStyle = { color: '#FFF' };
		const gridStyle = { color: '#F00' };
		
		const options = {
			title: `Currency rate ${currencyFrom}-${currencyTo} (last 7 days)`,
			curveType: 'none',
			vAxis: {
				textStyle: chartTextStyle,
				titleTextStyle: chartTextStyle,
				gridLines: gridStyle,
				viewWindowMode: 'maximized'
			},
			hAxis: {
				textStyle: chartTextStyle,
				titleTextStyle: chartTextStyle,
				gridLines: gridStyle,
				viewWindowMode: 'maximized'
			},
			legend: {
				position: 'bottom',
				textStyle: chartTextStyle
			},
			colors: ['#0C8'],
			backgroundColor: '#222',
			pointSize: 4,
			pointShape: 'circle',
			titleTextStyle: chartTextStyle
		};
		
		const chart = new google.visualization.LineChart(document.getElementById('currencyRateChart'));
		chart.draw(chartData, options);
	}
}