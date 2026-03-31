// OYUNCU VERİLERİ
let playerData = {
    username: "",
    role: "Yetişkin", // Varsayılan
    gender: "male"
};

// Son kalınan yeri hatırlamak için (Map mi yoksa Dashboard mu?)
let lastMenu = 'scene-menu'; 

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
    // Eğer çocuk seçildiyse avatar görselleri çocuk olmalı, yetişkinse yetişkin.
    updateAvatarVisual();

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
        // ÇOCUK İKONLARI
        if (playerData.gender === 'male') {
            avatarVisual.innerText = "👦"; // Çocuk Erkek
        } else {
            avatarVisual.innerText = "👧"; // Çocuk Kız
        }
    } else {
        // YETİŞKİN İKONLARI
        if (playerData.gender === 'male') {
            avatarVisual.innerText = "👨"; // Yetişkin Erkek
        } else {
            avatarVisual.innerText = "👩"; // Yetişkin Kadın
        }
    }
}

// ODA / BİNA GİRİŞ FONKSİYONU
// Bu fonksiyon hem menüden hem haritadan çalışır.
function goToRoom(imageFile, roomName) {
    const bgImage = document.getElementById('room-bg');
    const title = document.getElementById('room-title');
    
    // Hangi menüden gelindiğini kaydet (Geri dönünce oraya gitsin)
    // Eğer şu an haritadaysak (scene-map aktifse) geri dönüş haritaya olsun
    if(document.getElementById('scene-map').classList.contains('active')) {
        lastMenu = 'scene-map';
    } else {
        lastMenu = 'scene-menu';
    }

    // Odanın resmini ve adını ayarla
    bgImage.src = imageFile;
    title.innerText = roomName;

    // Odayı göster
    goToScene('scene-room-view');
}

// ODADAN ÇIKIŞ
function goBackFromRoom() {
    goToScene(lastMenu);
}
