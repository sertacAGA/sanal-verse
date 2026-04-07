// OYUNCU VERİLERİ
let playerData = {
    username: "",
    role: "Yetişkin", // Varsayılan
    gender: "male"
};

// Son kalınan yeri hatırlamak için (Map mi yoksa Dashboard mu?)
let lastMenu = 'scene-menu'; 

// SİSTEM BAŞLANGICI: HAFIZAYI KONTROL ET
window.onload = function() {
    // Oyuncu daha önce giriş yapmış mı?
    if(localStorage.getItem('isLoggedIn') === 'true') {
        // Verileri hafızadan çek
        playerData.username = localStorage.getItem('playerName');
        playerData.role = localStorage.getItem('playerRole') || "Yetişkin";
        playerData.gender = localStorage.getItem('playerGender') || "male";

        // Ekrana yansıt
        document.getElementById('display-name').innerText = playerData.username;
        updateAvatarVisual();

        // Başlangıç ekranlarını geçip direkt menüyü aç
        document.querySelectorAll('.scene').forEach(scene => scene.classList.remove('active'));
        document.getElementById('scene-menu').classList.add('active');
    }
};

// SAHNE DEĞİŞTİRME
function goToScene(sceneId) {
    document.querySelectorAll('.scene').forEach(scene => scene.classList.remove('active'));
    document.getElementById(sceneId).classList.add('active');
}

// AYARLAR MENÜSÜ AÇ/KAPA
function toggleSettings() {
    const modal = document.getElementById('settings-modal');
    modal.classList.toggle('open');
}

// OYUNU SIFIRLA / ÇIKIŞ YAP (Opsiyonel kullanım için)
function logout() {
    localStorage.clear(); // Tüm kayıtları sil
    location.reload();    // Sayfayı baştan yükle
}

// GİRİŞ KONTROLÜ VE ROL MANTIĞI
function validateAndGo(nextSceneId) {
    const nameInput = document.getElementById('username');
    if (nameInput.value.trim() === "") {
        alert("Lütfen isminizi giriniz!");
        return;
    }
    playerData.username = nameInput.value;

    // Rolü Al
    const roleOptions = document.getElementsByName('role');
    for (const option of roleOptions) {
        if (option.checked) {
            playerData.role = option.value;
            break;
        }
    }

    // İsmi Ekrana Yaz
    document.getElementById('display-name').innerText = playerData.username;

    // ROL'E GÖRE AVATAR SIFIRLA
    updateAvatarVisual();

    // *** YENİ: VERİLERİ HAFIZAYA KAYDET ***
    localStorage.setItem('isLoggedIn', 'true');
    localStorage.setItem('playerName', playerData.username);
    localStorage.setItem('playerRole', playerData.role);
    localStorage.setItem('playerGender', playerData.gender);

    goToScene(nextSceneId);
}

// CİNSİYET SEÇİMİ VE GÖRSEL GÜNCELLEME
function selectGender(gender, btn) {
    playerData.gender = gender;
    document.querySelectorAll('.gender-btn').forEach(b => b.classList.remove('selected'));
    btn.classList.add('selected');
    updateAvatarVisual();
}

function updateAvatarVisual() {
    const avatarVisual = document.getElementById('avatar-visual');
    
    if (playerData.role === "Çocuk") {
        if (playerData.gender === 'male') {
            avatarVisual.innerText = "👦"; 
        } else {
            avatarVisual.innerText = "👧"; 
        }
    } else {
        if (playerData.gender === 'male') {
            avatarVisual.innerText = "👨"; 
        } else {
            avatarVisual.innerText = "👩"; 
        }
    }
}

// ODA / BİNA GİRİŞ FONKSİYONU
function goToRoom(imageFile, roomName) {
    const bgImage = document.getElementById('room-bg');
    const title = document.getElementById('room-title');
    
    // Hangi menüden gelindiğini kaydet
    if(document.getElementById('scene-map').classList.contains('active')) {
        lastMenu = 'scene-map';
    } else {
        lastMenu = 'scene-menu';
    }

    bgImage.src = imageFile;
    title.innerText = roomName;

    goToScene('scene-room-view');
}

// ODADAN ÇIKIŞ
function goBackFromRoom() {
    goToScene(lastMenu);
}
