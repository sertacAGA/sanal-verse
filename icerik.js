// icerik.js - Sadece ders içerikleri burada durur.

const curriculumData = [
    {
        subject: "Matematik",
        lessons: [
            { 
                id: "mat1", 
                title: "Geometri Temelleri", 
                points: 100, 
                content: {
                    // Daha uzun, doyurucu metin
                    text: "Geometri, evrenin şeklini anlamamıza yarar. Nokta, boyutsuz bir yer belirtir. Doğru, iki yöne sonsuza giden noktalar kümesidir. Işın ise bir noktadan başlayıp sonsuza gider. Üçgenler iç açıları toplamı 180 derece olan kapalı şekillerdir.",
                    
                    // Birden fazla resim (Dizi/Array formatında)
                    images: [
                        "https://images.unsplash.com/photo-1596495578065-6e0763fa1178?w=800&q=80", // Cetvel ve defter
                        "https://images.unsplash.com/photo-1509228468518-180dd4864904?w=800&q=80"  // Matematiksel formüller
                    ],
                    
                    // Oynatılabilir güvenli video linki
                    video: "https://www.youtube.com/embed/7CgO82vXo5Q", 
                    
                    // YENİ: Konu Örnekleri
                    examples: [
                        "Örnek 1: Bir üçgenin iki açısı 60 ve 50 derece ise, üçüncü açısı 70 derecedir (180-110=70).",
                        "Örnek 2: Kare, tüm kenarları eşit bir dikdörtgendir.",
                        "Örnek 3: Dik açı 90 derecedir ve köşeli parantez ile gösterilir."
                    ],

                    activityType: "draw", 
                    activityText: "Aşağıdaki alana bir dik üçgen çiz ve 90 derecelik köşeyi belirginleştir.",
                    
                    // 4-5 Soruluk Test
                    quiz: [
                        { q: "Üçgenin iç açıları toplamı kaçtır?", opts: ["180", "360", "90"], ans: 0 },
                        { q: "Tüm kenarları eşit olan dörtgene ne denir?", opts: ["Yamuk", "Kare", "Dikdörtgen"], ans: 1 },
                        { q: "Boyutu olmayan geometrik terim hangisidir?", opts: ["Doğru", "Nokta", "Düzlem"], ans: 1 },
                        { q: "Bir açısı 90 derece olan üçgene ne denir?", opts: ["Eşkenar", "Dik Üçgen", "Geniş Açılı"], ans: 1 }
                    ]
                }
            },
            { 
                id: "mat2", title: "Cebir", points: 100, 
                content: { 
                    text: "Cebir içerikleri hazırlanıyor...", images: [], video: "", examples: [], activityType: "text", quiz: [] 
                } 
            }
        ]
    },
    {
        subject: "Robotik Kodlama",
        lessons: [
            { 
                id: "rob1", 
                title: "Algoritma", 
                points: 100, 
                content: {
                    text: "Algoritma bir sorunu çözmek için takip edilen mantıksal adımlardır. Robotlar düşünemez, sadece algoritmayı uygularlar.",
                    images: ["https://images.unsplash.com/photo-1517077304055-6e89abbf09b0?w=800"],
                    video: "https://www.youtube.com/embed/8j0UDiN7my4",
                    examples: ["Örnek: Evden okula gitme algoritması.", "Örnek: Çay demleme adımları."],
                    activityType: "draw", 
                    activityText: "Bir robotun engelden kaçma şemasını çiz.",
                    quiz: [
                        { q: "Algoritma nedir?", opts: ["Yemek", "Adım adım çözüm", "Robot"], ans: 1 },
                        { q: "Hata ayıklama işlemine ne denir?", opts: ["Debugging", "Coding", "Running"], ans: 0 },
                        { q: "Hangisi bir programlama dilidir?", opts: ["HTML", "Python", "JPEG"], ans: 1 },
                        { q: "Döngü (Loop) ne işe yarar?", opts: ["Tekrar eder", "Durur", "Siler"], ans: 0 }
                    ]
                }
            }
        ]
    }
];
