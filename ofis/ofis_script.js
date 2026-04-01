// Sayfa yüklendiğinde ana oyun verilerini (örn: kullanıcı adı) çekebilirsin (localstorage)

let currentSlide = 0;
let presentationData = [];

function startPresentation() {
    // 1. Girişleri Al
    const tech = document.getElementById('prep-tech').value || 'Temel Prototip';
    const need = document.getElementById('prep-need').value || 'Genel İhtiyaç';
    const style = document.getElementById('prep-style').value;

    // 2. Basit bir Sunum Senaryosu Oluştur (Arayüz mantığı)
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

    // Butonu Aktif Et
    btnElement.classList.remove('disabled');
    btnElement.disabled = false;
}

function nextSlide() {
    currentSlide++;

    if (currentSlide < presentationData.length) {
        showSlide();
    } else {
        // Sunum Bitti - Analiz ve Puanlama Ekranına Geçilebilir
        finishPresentation();
    }
}

function finishPresentation() {
    const textElement = document.getElementById('presentation-text');
    const btnElement = document.getElementById('next-slide-btn');

    // Basit bir analiz (Lego mantığı: Puan verelim)
    // Gelecekte buraya okulda kazanılan puanlar da eklenebilir.
    textElement.innerHTML = `<h3>Sunum Tamamlandı!</h3><p>Patronunuz projeyi değerlendiriyor...</p><p>Sunum Puanınız: <strong>85 / 100</strong></p>`;
    
    // Geri Dön butonunu aktif et
    btnElement.innerText = 'BİTİR VE ÇIK';
    btnElement.onclick = () => window.location.href = '../index.html';
}
