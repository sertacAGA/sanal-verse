let playerName = localStorage.getItem('playerName') || "Oyuncu";
let playerRole = localStorage.getItem('playerRole') || "Yetişkin";
let currentSlide = 0;
let presentationData = [];

window.onload = () => {
    const welcomeText = document.getElementById('office-welcome');
    if(welcomeText) welcomeText.innerText = `Hoş geldin ${playerName}, projen sunuma hazır mı?`;
};

function goBackToMap() { window.location.href = '../index.html'; }

function startPresentation() {
    // Bilgileri tam bu anda çekiyoruz
    const tech = document.getElementById('prep-tech').value || 'Akıllı Sistemler';
    const need = document.getElementById('prep-need').value || 'Hızlı Çözümler';
    const style = document.getElementById('prep-style').value;

    presentationData = [
        `Merhaba, bugün sizlere projemizi sunmaktan heyecan duyuyorum.`,
        `Projemizin teknik odak noktası tamamen "${tech}" üzerine kurulu.`,
        `Bu sayede kasabamızdaki "${need}" sorununa kökten bir çözüm sunuyoruz.`,
        `Görsel dilimizde "${style}" tarzını seçerek fark yaratmayı hedefledik.`,
        `Sunumumu dinlediğiniz için teşekkürler. Kararınızı bekliyorum!`
    ];

    document.getElementById('office-preparation').classList.remove('active');
    document.getElementById('office-presentation').classList.add('active');

    currentSlide = 0;
    showSlide();
}

function showSlide() {
    const textElement = document.getElementById('presentation-text');
    const btnElement = document.getElementById('next-slide-btn');
    
    if(textElement) {
        textElement.innerText = presentationData[currentSlide];
        btnElement.innerText = (currentSlide === presentationData.length - 1) ? "SONUCU GÖR" : "SONRAKİ SAYFA ➔";
    }
}

function nextSlide() {
    currentSlide++;
    if (currentSlide < presentationData.length) {
        showSlide();
    } else {
        finishPresentation();
    }
}

function finishPresentation() {
    const textElement = document.getElementById('presentation-text');
    const btnElement = document.getElementById('next-slide-btn');

    textElement.innerHTML = `
        <div style="width:100%">
            <h3 style="color:#2575fc">Sunum Tamamlandı!</h3>
            <div style="font-size:3rem; margin:15px 0; color:#f1c40f">85 / 100</div>
            <p>Tebrikler ${playerName}, bir ${playerRole} olarak harika iş çıkardın!</p>
        </div>
    `;
    
    btnElement.innerText = 'HARİTAYA DÖN';
    btnElement.onclick = goBackToMap;
}
