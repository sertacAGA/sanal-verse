// Modalı Aç
function openModal(modalId) {
    document.getElementById(modalId).classList.add('active');
}

// Modalı Kapat
function closeModal(modalId) {
    document.getElementById(modalId).classList.remove('active');
    
    // Eğer TV kapatılıyorsa, videonun sesini kesmek için iframe'i yenile
    if (modalId === 'tv-modal') {
        let iframe = document.querySelector('#tv-modal iframe');
        if (iframe) {
            let iframeSrc = iframe.src;
            iframe.src = iframeSrc; 
        }
    }
}
