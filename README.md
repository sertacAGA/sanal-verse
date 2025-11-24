# 🎓 Sanal Sınıf: İnsansız Eğitim Platformu (V5)

![Project Status](https://img.shields.io/badge/Status-Active-success)
![Technology](https://img.shields.io/badge/Tech-HTML%20%7C%20JS%20%7C%20A--Frame-blue)
![Platform](https://img.shields.io/badge/Platform-Web%20%7C%20Mobile%20%7C%20Tablet-orange)

**Sanal Sınıf**, öğrencilerin öğretmen gözetimi olmadan kendi hızlarında ders çalışabilecekleri, 3 boyutlu bir sınıf ortamı sunan web tabanlı bir eğitim platformudur. A-Frame teknolojisi ile tarayıcı üzerinde çalışan bu sistem; metin okuma (TTS), interaktif çizim etkinlikleri, video dersler ve puanlı sınav sistemiyle donatılmıştır.

---

## 🌟 Öne Çıkan Özellikler

### 🏫 3D Sınıf Atmosferi
- **A-Frame** altyapısı ile tarayıcı tabanlı 3D sınıf ortamı.
- Kullanıcı arayüzü (UI), sınıfın içindeki "Akıllı Tahta" üzerine entegre edilmiştir.

### 📚 Modüler Ders Sistemi
- **6 Ana Branş:** Matematik, Fen, Edebiyat, Resim, Müzik, Kimya.
- **Dinamik İçerik:** Dersler harici bir JavaScript dosyasından (`icerik.js`) çekilir, kolayca yeni ders eklenebilir.
- **Adım Adım Öğrenme:** Konu Anlatımı -> Görsel Galeri -> Video -> Örnekler -> Etkinlik -> Sınav.

### 🎮 Oyunlaştırma ve Puanlama
- **Başarı Puanı:** Çözülen testlerdeki başarı oranına göre puan kazanılır.
- **Kalıcı İlerleme:** Tarayıcı kapansa bile kazanılan puanlar ve tamamlanan dersler (`Local Storage` sayesinde) korunur.
- **Tekrar Modu:** Tamamlanan dersler tekrar incelenebilir.

### 🎨 İnteraktif Araçlar
- **Sesli Anlatım (TTS):** Ders notları otomatik olarak Türkçe seslendirilir.
- **Çizim Tahtası (Canvas):** Hem PC (Mouse) hem de Tablet/iPad (Dokunmatik) uyumlu çizim alanı.
- **Görsel Galeri:** Kaydırılabilir konu görselleri.

---

## 🚀 Kurulum ve Çalıştırma

Bu proje herhangi bir sunucu kurulumu veya veritabanı gerektirmez.

1.  Bu repoyu bilgisayarınıza indirin (Clone veya Download ZIP).
2.  Klasör içindeki `index.html` ve `icerik.js` dosyalarının yan yana olduğundan emin olun.
3.  `index.html` dosyasına çift tıklayın ve tarayıcınızda açın.
4.  Hepsi bu kadar! 🎉

> **Not:** Sesli okuma (TTS) özelliklerinin sorunsuz çalışması için modern tarayıcılar (Chrome, Edge, Safari) önerilir.

---

## 📂 Proje Yapısı

- **`index.html`**: Uygulamanın beyni. 3D sahne, kullanıcı arayüzü, puanlama mantığı ve etkileşim kodlarını içerir.
- **`icerik.js`**: Uygulamanın veritabanı. Tüm ders metinleri, resim linkleri, videolar ve sınav soruları bu dosyada JSON formatında tutulur.

### İçerik Nasıl Eklenir?
`icerik.js` dosyasını açarak `curriculumData` dizisine yeni bir obje ekleyerek kolayca yeni ders oluşturabilirsiniz:

```javascript
{
    id: "yeniDers1",
    title: "Ders Başlığı",
    points: 100,
    content: {
        text: "Ders içeriği...",
        images: ["resim.jpg"],
        video: "video_linki",
        activityType: "draw",
        activityText: "Çizim görevi...",
        quiz: [{ q: "Soru?", opts: ["A", "B"], ans: 0 }]
    }
}
