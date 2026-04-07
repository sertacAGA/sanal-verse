// OYUNCU VERİLERİ (Hafızadan Çek)
let playerName = localStorage.getItem('playerName') || "Oyuncu";
let playerRole = localStorage.getItem('playerRole') || "Yetişkin";

let currentSlide = 0;
let presentationData = [];

// Sayfa yüklendiğinde ismi göster
window.onload = () => {
    const welcomeText = document.getElementById('office-welcome');
    if(welcomeText) welcomeText.innerText = `Hoş geldin ${playerName}, projen hazır mı?`;
};

// HARİTAYA GERİ DÖN (Klasör dışına çıkış)
function goBackToMap() {
    window.location.href = '../index.html';
}

function startPresentation() {
    // 1. Girişleri Al
    const tech = document.getElementById('prep-tech').value || 'Temel Prototip';
    const need = document.getElementById('prep-need').value || 'Genel İhtiyaç';
    const style = document.getElementById('prep-style').value;

    // 2. Senaryo Oluştur
    presentationData = [
        `Merhaba, bugün sizlere projemizi sunmaktan heyecan duyuyorum.`,
        `Projemizin temel teknik odak noktası: ${tech}.`,
        `Bu projenin kasabamızda çözüm bulacağı ana ihtiyaç ise: ${need}.`,
        `Bu projeyi hayata geçirmek için en uygun tarzımız: ${style}.`,
        `Sunumumuzu dinlediğiniz için teşekkürler. Sonucunuzu bekliyoruz!`
    ];

    // 3. Sahneleri Değiştir
    document.getElementById('office-preparation').classList.remove('active');
    document.getElementById('office-presentation').classList.add('active');

    // 4. İlk Slaytı Başlat
    currentSlide = 0;
    showSlide();
}

function showSlide() {
    const textElement = document.getElementById('presentation-text');
    const btnElement = document.getElementById('next-slide-btn');

    textElement.innerText = presentationData[currentSlide];
    btnElement.innerText = (currentSlide === presentationData.length - 1) ? "SUNUMU BİTİR" : "SONRAKİ SAYFA ➔";
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

    // Sunum Analizi
    textElement.innerHTML = `
        <div class="result-screen">
            <h3>Sunum Tamamlandı!</h3>
            <p>Patronunuz projeyi değerlendirdi...</p>
            <div class="score-badge">85 / 100</div>
            <p style="margin-top:10px; font-size:14px;">Tebrikler ${playerName}, ${playerRole} olarak harika bir iş çıkardın!</p>
        </div>
    `;
    
    // Butonu Ana Sayfaya Dönüş Butonuna Çevir
    btnElement.innerText = 'HARİTAYA DÖN VE DEVAM ET';
    btnElement.classList.add('primary');
    btnElement.onclick = () => goBackToMap();
}
