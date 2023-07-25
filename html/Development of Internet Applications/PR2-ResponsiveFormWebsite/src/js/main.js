/*----- Classes -----*/
class Countdown {
	constructor(finalDate, msg, finalMsg, msgEl, timeEl) {
		const dateTimestamp = Date.now();
		const finalTimestamp = finalDate.getTime();
		
		if(dateTimestamp < finalTimestamp) {
			msgEl.innerHTML = msg;
			timeEl.innerHTML = this.getDisplayTime(finalTimestamp);
			
			// Regular tick
			const regularTick = setInterval(() => {
				timeEl.innerHTML = this.getDisplayTime(finalTimestamp);
			}, 1000);
			
			// Final tick
			if(finalTimestamp - dateTimestamp <= 2147483647) {
				setTimeout(() => {
					msgEl.innerHTML = finalMsg;
					timeEl.innerHTML = '';
					
					clearInterval(regularTick);
				}, (finalTimestamp - dateTimestamp));
			}
		}
		else {
			// After finish
			msgEl.innerHTML = finalMsg;
			timeEl.innerHTML = '';
		}
	}
	
	getDisplayTime(finalTimestamp=0) {
		let diff = finalTimestamp - Date.now();
		
		if(diff > 0) {
			const d = Math.floor(diff / 86400000);	diff -= d * 86400000;
			const h = Math.floor(diff / 3600000);	diff -= h * 3600000;
			const m = Math.floor(diff / 60000);		diff -= m * 60000;
			const s = Math.floor(diff / 1000);
			
			const D = (d <= 0 ? '' : d + ':');
			const H = (d <= 0 ? (h <= 0 ? '' : h + ':') : (h < 10 ? '0' + h : h) + ':');
			const M = (d <= 0 && h <= 0 ? (m <= 0 ? '' : m + ':') : (m < 10 ? '0' + m : m) + ':');
			const S = (d <= 0 && h <= 0 && m <= 0 ? (s <= 0 ? '' : s) : (s < 10 ? '0' + s : s));
			
			return (D + H + M + S);
		}
		
		return '';
	}
}

class Dialog {
	constructor(className) {
		this.dialog = document.querySelector('.o-dialog');
		
		if(!this.dialog) {
			this.dialog = document.createElement('dialog');
			document.body.appendChild(this.dialog);
		}
		
		this.dialog.className = 'o-dialog ' + className;
		this.dialog.setAttribute('open', '');
	}
}

class SlideshowDialog extends Dialog {
	constructor(imageSelected) {
		super('o-slideshow-dialog');
		const self = this;
		
		this.dialogImageSelectedId = 0;
		this.dialogImageCount = 0;
		
		const list = imageSelected.closest('.c-list');
		this.dialogList = document.createElement('ul');
		this.dialogList.className = 'c-list u-grid';
		this.dialog.appendChild(this.dialogList);
		
		const listItems = list.querySelectorAll('.c-list-item');
		this.dialogListItems = [];
		for(let i = 0; i < listItems.length; i++) {
			this.dialogListItems[i] = document.createElement('li');
			this.dialogListItems[i].className = 'c-list-item u-grid-item';
			this.dialogList.appendChild(this.dialogListItems[i]);
		}
		
		const images = list.querySelectorAll('.c-list-item > img');
		this.dialogImages = [];
		for(let i = 0; i < images.length; i++) {
			if(imageSelected == images[i]) {
				this.dialogImageSelectedId = i + 1;
			}
			
			this.dialogImages[i] = document.createElement('img');
			this.dialogImages[i].setAttribute('src', images[i].getAttribute('src'));
			this.dialogImages[i].setAttribute('alt', images[i].getAttribute('alt'));
			this.dialogListItems[i].appendChild(this.dialogImages[i]);
		}
		
		this.dialogListItems[this.dialogImageSelectedId - 1].classList.add('active');
		this.dialogImageCount = this.dialogImages.length;
		
		// Slideshow footer
		this.dialogFooter = document.createElement('div');
		this.dialogFooter.className = 'o-slideshow-dialog__footer u-grid-item';
		this.dialogFooter.innerHTML = this.dialogImageSelectedId + ' / ' + this.dialogImageCount;
		this.dialog.appendChild(this.dialogFooter);
		
		this.dialogPrevButton = document.createElement('button');
		this.dialogPrevButton.className = 'o-slideshow-dialog__prev-button c-button';
		this.dialogPrevButton.innerHTML = '<';
		this.dialogPrevButton.onclick = () => {
			self.dialogImageSelectedId -= (self.dialogImageSelectedId > 1 ? 1 : 0);
			
			for(let i = 0; i < listItems.length; i++) {
				this.dialogListItems[i].classList.remove('active');
			}
			this.dialogListItems[self.dialogImageSelectedId - 1].classList.add('active');
			
			self.dialogFooter.innerHTML = self.dialogImageSelectedId + ' / ' + self.dialogImageCount;
		};
		this.dialog.appendChild(this.dialogPrevButton);
		
		this.dialogNextButton = document.createElement('button');
		this.dialogNextButton.className = 'o-slideshow-dialog__next-button c-button';
		this.dialogNextButton.innerHTML = '>';
		this.dialogNextButton.onclick = () => {
			self.dialogImageSelectedId += (self.dialogImageSelectedId < self.dialogImageCount ? 1 : 0);
			
			for(let i = 0; i < listItems.length; i++) {
				this.dialogListItems[i].classList.remove('active');
			}
			this.dialogListItems[self.dialogImageSelectedId - 1].classList.add('active');
			
			self.dialogFooter.innerHTML = self.dialogImageSelectedId + ' / ' + self.dialogImageCount;
		};
		this.dialog.appendChild(this.dialogNextButton);
		
		this.dialogCloseButton = document.createElement('button');
		this.dialogCloseButton.className = 'o-slideshow-dialog__close-button c-button';
		this.dialogCloseButton.innerHTML = 'X';
		this.dialogCloseButton.onclick = () => {
			self.dialog.remove();
		};
		this.dialog.appendChild(this.dialogCloseButton);
	}
}

class SlideshowManager {
	constructor(slideshows) {
		if(slideshows.length > 0) {
			for(let i = 0; i < slideshows.length; i++) {
				const slideshowListItems = slideshows[i].getElementsByClassName('c-list-item');
				for(let j = 0; j < slideshowListItems.length; j++) {
					slideshowListItems[j].onclick = (event) => {
						const slideshowDialog = new SlideshowDialog(event.target);
					};
				}
			}
		}
	}
}





/*----- Main -----*/
// Navigation
const nav = document.querySelector('.o-header__nav');
const navBtn = document.querySelector('.o-header__nav-button');
if(navBtn) {
	navBtn.onclick = () => {
		nav.classList.toggle('open');
	};
}

// Countdown
const msgEl = document.querySelector('.o-countdown__message');
const timeEl = document.querySelector('.o-countdown__time');
const countdown = (msgEl && timeEl ? new Countdown(new Date("2021-01-01 0:00:00"), "Countdown to New Year 2021", "The countdown has passed!", msgEl, timeEl) : null);

// Slideshow
const slideshows = document.getElementsByClassName('o-slideshow');
const slideshowManager = (slideshows[0] ? new SlideshowManager(slideshows) : null);

// Tabs
const tabButtons = document.querySelectorAll('.o-blog-tab-buttons > button');
const tabs = document.querySelectorAll('.o-blog-posts > ul');
for(let i = 0; i < tabButtons.length; i++) {
	tabButtons[i].onclick = (event) => {
		for(let j = 0; j < tabButtons.length; j++) {
			tabButtons[j].classList.remove('active');
		}
		event.target.classList.add('active');
		
		for(let j = 0; j < tabs.length; j++) {
			tabs[j].classList.add('hidden');
			if(event.target.dataset.tabSelect == tabs[j].dataset.tabPage) {
				tabs[j].classList.remove('hidden');
			}
		}
	};
}