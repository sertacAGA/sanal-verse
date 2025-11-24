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

The Android version of the application is available on the Google Play Store: https://play.google.com/store/apps/details?id=com.crownbros.sanalverse

The internet version of the application (WebGL) can be accessed at this link: https://sertacaga.itch.io/sanalverse


![alt text](https://sertacaga.github.io/SanalVerse/Ekran-1.png)

TÜRKÇE
-----------------------------------------------------------------------
Merhaba. Sanal Verse uygulaması ile Online çok oyunculu sınıf ve konferans buluşmalarınızı yapabileceğiniz bir uygulamadır.

Uygulada yer alan özellikler aşağıda belirtilmiştir;

Giriş ekranında isim giriş alanı vardır. Burada yer alan isim uygulamaya giriş yapıldığında aktif olmaktadır. Avatar seçimi için roller mevcuttur; yetişkin, çocuk. Yetişkin olarak odaya bağlanan oyuncular içerikleri değiştirebilmektedirler. Çocuk seçimi yapan oyuncular içerik değiştirememektedir.

İkinci panelde avatar seçimi için modeller vardır. Model seçildikten sonra oda seçim ekranı belirmektedir. Oda ekranında Sınıf, Konferans Salonu, Atölye ve Ofis yer almaktadır. Oyunda açılmış olan odaları görebilmek için "Açık Odalar" adında bir düğme mevcuttur.

Oda seçimi yapıldıktan sonra oyuncular girmiş olduğu odalardaki çoklu medya sistemlerini kullanabilmektedirler. Her odanın kullanıcı arayüzünde sesli iletişimi açıp kapatmak için düğme mevcuttur. Odada yer alan çoklu medya sistemi için gerekli düğmeler odalarda yer almaktadır.

Her odada ana menüye dönmek için bir çıkış düğmesi mevcuttur. Oyuncunun odalarda yer alan sandalyelere oturabilmesi için gerekli düğme sandalyeye yakınlaşınca aktif olmaktadır.

Server sistemi ücretsiz Photon Engine 2 Multiplayer sistemi kullanılarak hazırlanmıştır. Sesli iletişim için Photon Voice ücretsiz versiyonu kullanılmıştır. Sohbet yapılabilmesi için Photon Chat ücretsiz versiyonu kullanılmıştır. (Şu an aktif değildir!)

Her odaya en fazla 16 kişi katılabilmektedir. 

Sınıf tipi odada; çizim yapılabilen bir ekran yer almaktadır. Arayüzde Kalem ve Silgi düğmeleri mevcuttur. Bu düğmeler aracılığı ile ekrana çizim yapılabilmekte, sonrasında bu çizim çizimi paylaş düğmesi ile diğer oyunculara aktarılabilmektedir.

Ofis tipi odada; resim slide gösterisi yapılabilen bir ekran yer almaktadır. Ekranda yer alan resimler, oyuncular tarafından "Sunum Yükle" düğmesi ile değiştirilebilmektedir. Slide geçişlerini ileri veya geri oynatmak için düğmeler de mevcuttur.

Konferans Salonu tipi odada; internet üzerinden bağlantısını verdiğimiz 5 adet video oynatılabilmektedir. Video ekranındaki başlat ve dur komutları için arayüzde düğmeler mevcuttur. 

Not: Sınıfa sonradan giriş yapan oyuncular için video senkronizasyonu henüz yapılmamıştır. Video durdurulduktan sonra tekrar başlatınca videoyu başa almaktadır.

Atölye tipi odada; 3 Boyutlu nesneler, nesne ve kaplama olarak yüklendikten sonra diğer oyunculara sunulabilmektedir. Kaide üzerinde 3 boyutlu nesneler belirmektedir. 3 Boyutlu Nesneleri değiştirmek için, ileri geri düğmeleri mevcuttur.

Sınıflarda sandalyeler mevcuttur ve sandalyelerde oturma sistemi vardır. Uygula açıldığında yer alan içerikler github üzerinde barındırılmaktadır. Kullanıcı isteğe göre başka bir sunucudan resim, video veya 3 boyutlu nesne çağırabilmektedir.

Uygulamanın internet versiyonu (WebGL) bu linkten ulaşılabilir: https://sertacaga.itch.io/sanalverse

Uygulamanın Android versiyonu Google Play Store'da yer almaktadır: https://play.google.com/store/apps/details?id=com.crownbros.sanalverse

![alt text](https://sertacaga.github.io/SanalVerse/Ekran-2.jpg)

![alt text](https://sertacaga.github.io/SanalVerse/Ekran-3.png)
