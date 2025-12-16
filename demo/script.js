// OYUNCU VERİLERİ
let playerData = {
    username: "",
    role: "Yetişkin",
    gender: "male"
};

// SAHNE DEĞİŞTİRME FONKSİYONU
function goToScene(sceneId) {
    // 1. Tüm sahnelerden 'active' sınıfını kaldır
    const scenes = document.querySelectorAll('.scene');
    scenes.forEach(scene => {
        scene.classList.remove('active');
    });

    // 2. İstenen sahneye 'active' sınıfını ekle
    const targetScene = document.getElementById(sceneId);
    if (targetScene) {
        targetScene.classList.add('active');
    }
}

// GİRİŞ KONTROLÜ
function validateAndGo(nextSceneId) {
    const nameInput = document.getElementById('username');
    const name = nameInput.value.trim();

    if (name === "") {
        alert("Lütfen isminizi giriniz!");
        // Input kutusunu salla (CSS efekti eklenebilir, şimdilik odaklayalım)
        nameInput.focus();
        return;
    }

    // İsmi kaydet
    playerData.username = name;

    // Seçili rolü bul ve kaydet
    const roleOptions = document.getElementsByName('role');
    for (const option of roleOptions) {
        if (option.checked) {
            playerData.role = option.value;
            break;
        }
    }

    console.log("Kayıt Başarılı:", playerData);
    
    // İsmi menüye yazdır
    document.getElementById('display-name').innerText = playerData.username;
    
    goToScene(nextSceneId);
}

// CİNSİYET SEÇİMİ
function selectGender(gender, btn) {
    playerData.gender = gender;

    // Tüm butonların seçili halini kaldır
    document.querySelectorAll('.gender-btn').forEach(b => b.classList.remove('selected'));
    
    // Tıklanan butonu seçili yap
    btn.classList.add('selected');

    // Görseli güncelle
    const avatarVisual = document.getElementById('avatar-visual');
    if (gender === 'male') {
        avatarVisual.innerText = "🧔";
    } else {
        avatarVisual.innerText = "👩";
    }
}

// BİNAYA GİRİŞ (HARİTA)
function enterBuilding(buildingName) {
    // Burada ileride yeni sayfa açabilir veya modal gösterebiliriz
    // Şimdilik sadece uyarı verelim
    const message = `Sayın ${playerData.role}, ${buildingName} binasına giriş yapılıyor...`;
    alert(message);
}
