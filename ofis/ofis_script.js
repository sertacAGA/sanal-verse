// --- OYUNCU VERİLERİ (localStorage üzerinden güvenli çekim) ---
let playerName = localStorage.getItem('playerName') || "Oyuncu";
let playerRole = localStorage.getItem('playerRole') || "Ziyaretçi";

let currentSlide = 0;
let presentationData = [];

// Sayfa yüklendiğinde karşılama metnini ayarla
window.onload = () => {
    const welcomeText = document.getElementById('office-welcome');
    if (welcomeText) {
        welcomeText.innerText = `Hoş geldin ${playerName}, projen sunuma hazır mı?`;
    }
};

// --- NAVİGASYON FONKSİYONLARI ---
function goBackToMap() {
    // Klasör yapısına göre ana haritaya (index.html) yönlendirir
    window.location.href = '../index.html';
}

// --- SUNUM MANTIĞI ---
function startPresentation() {
    // 1. Kullanıcı Girişlerini Yakala
    const techInput = document.getElementById('prep-tech').value;
    const needInput = document.getElementById('prep-need').value;
    const styleInput = document.getElementById('prep-style').value;

    // Boş bırakılan yerler için varsayılan değerler ata
    const tech = techInput || 'Yenilikçi Teknoloji';
    const need = needInput || 'Toplumsal İhtiyaç';
    const style = styleInput || 'Modern';

    // 2. Dinamik Sunum Senaryosu Oluştur
    presentationData = [
        `Merhaba, bugün sizlere yeni projemizi sunmaktan heyecan duyuyorum.`,
        `Projemizin temel teknik odak noktası: ${tech}.`,
        `Bu projenin kasabamızda çözüm bulacağı ana ihtiyaç: ${need}.`,
        `Tasarım dili olarak belirlediğimiz tarz: ${style}.`,
        `Sunumumuzu dinlediğiniz için teşekkürler. Değerlendirmenizi bekliyoruz!`
    ];

    // 3. Görünür Sahneleri Değiştir (Active Class Yönetimi)
    const prepScene = document.getElementById('office-preparation');
    const presScene = document.getElementById('office-presentation');

    if (prepScene && presScene) {
        prepScene.classList.remove('active');
        presScene.classList.add('active');
    }

    // 4. İlk Slaytı Ekrana Bas
    currentSlide = 0;
    showSlide();
}

function showSlide() {
    const textElement = document.getElementById('presentation-text');
    const btnElement = document.getElementById('next-slide-btn');

    if (textElement && btnElement) {
        textElement.innerText = presentationData[currentSlide];
        
        // Son slaytta buton metnini değiştir
        if (currentSlide === presentationData.length - 1) {
            btnElement.innerText = "SONUCU GÖR 📊";
        } else {
            btnElement.innerText = "SONRAKİ SAYFA ➔";
        }
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

// finishPresentation fonksiyonu içindeki ilgili alan:
function finishPresentation() {
    const textElement = document.getElementById('presentation-text');
    const btnElement = document.getElementById('next-slide-btn');

    // CSS'deki dialogue-box yapısını koruyarak sonucu gösteriyoruz
    textElement.innerHTML = `
        <div style="text-align: center; width: 100%;">
            <h3 style="color: #2575fc; margin-bottom: 5px;">Sunum Başarıyla Tamamlandı!</h3>
            <div style="font-size: 2.5em; font-weight: bold; color: #f1c40f;">🏆 85 / 100</div>
            <p style="font-size: 16px; margin-top: 10px;">Tebrikler ${playerName}, harika bir ${playerRole} vizyonu!</p>
        </div>
    `;
    
    btnElement.innerText = 'HARİTAYA DÖN VE DEVAM ET';
    btnElement.onclick = () => goBackToMap();
}
