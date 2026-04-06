// Müzik Dosyaları (İnternetten telifsiz örnekler veya kendi dosyaların)
const tracks = {
    jazz: "https://www.bensound.com/bensound-music/bensound-thejazzpiano.mp3",
    lofi: "https://www.bensound.com/bensound-music/bensound-slowmotion.mp3"
};

function playMusic(type) {
    const audio = document.getElementById('bg-audio');
    const label = document.getElementById('current-track');
    
    audio.src = tracks[type];
    audio.play();
    label.innerText = "Çalan: " + type.toUpperCase();
}

function stopMusic() {
    const audio = document.getElementById('bg-audio');
    audio.pause();
    document.getElementById('current-track').innerText = "Çalan: -";
}

function acceptJob(jobName, reward) {
    // Görevi yerel hafızaya kaydet
    localStorage.setItem('activeJob', jobName);
    localStorage.setItem('jobReward', reward);

    // Bildirim göster
    const toast = document.getElementById('toast');
    toast.innerText = jobName + " görevi kabul edildi! " + reward + " ₺ kazanacaksın.";
    toast.className = "toast show";
    
    setTimeout(() => { toast.className = toast.className.replace("show", ""); }, 3000);
}
