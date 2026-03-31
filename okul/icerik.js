// icerik.js - TAM SÜRÜM (6 Ders x 3 Konu = 18 Derslik Müfredat)

const curriculumData = [
    // --- 1. MATEMATİK ---
    {
        subject: "Matematik",
        lessons: [
            { 
                id: "mat1", 
                title: "Geometri Temelleri", 
                points: 100, 
                content: {
                    text: "Geometri, şekillerin ve uzayın bilimidir. Temel kavramlar arasında Nokta (boyutsuz yer belirteci), Doğru (iki yöne sonsuza giden noktalar kümesi) ve Düzlem (sonsuz genişlikteki yüzey) bulunur. Açılar ise iki ışının kesişmesiyle oluşur.",
                    images: [
                        "https://images.unsplash.com/photo-1596495578065-6e0763fa1178?w=800&q=80",
                        "https://images.unsplash.com/photo-1509228468518-180dd4864904?w=800&q=80"
                    ],
                    video: "https://www.youtube.com/embed/7CgO82vXo5Q", 
                    examples: [
                        "Örnek 1: Bir üçgenin iç açıları toplamı daima 180 derecedir.",
                        "Örnek 2: Karenin 4 kenarı eşittir ve her açısı 90 derecedir.",
                        "Örnek 3: Paralel doğrular asla kesişmezler."
                    ],
                    activityType: "draw", 
                    activityText: "Aşağıdaki alana birbirine paralel iki doğru ve onları kesen üçüncü bir doğru çiz.",
                    quiz: [
                        { q: "Üçgenin iç açıları toplamı kaçtır?", opts: ["180", "360", "90"], ans: 0 },
                        { q: "Tüm kenarları eşit olan dörtgene ne denir?", opts: ["Yamuk", "Kare", "Dikdörtgen"], ans: 1 },
                        { q: "Boyutu olmayan geometrik terim hangisidir?", opts: ["Doğru", "Nokta", "Düzlem"], ans: 1 },
                        { q: "Bir açısı 90 derece olan üçgene ne denir?", opts: ["Eşkenar", "Dik Üçgen", "Geniş Açılı"], ans: 1 }
                    ]
                }
            },
            { 
                id: "mat2", title: "Üslü Sayılar", points: 100, 
                content: { 
                    text: "Üslü sayılar, bir sayının kendisiyle tekrarlı çarpımını ifade eder. Örneğin 2 üssü 3 demek, 2'yi yan yana 3 kere yazıp çarpmak demektir (2x2x2). Taban çarpılan sayıyı, üs ise kaç kere çarpılacağını gösterir.",
                    images: ["https://images.unsplash.com/photo-1635070041078-e363dbe005cb?w=800"], 
                    video: "https://www.youtube.com/embed/LhXo8sJ5Hwk", 
                    examples: ["Örnek: 5² = 5 x 5 = 25", "Örnek: 2³ = 2 x 2 x 2 = 8", "Örnek: Her sayının 0. kuvveti 1'dir (0 hariç)."], 
                    activityType: "text", 
                    activityText: "Defterine 3'ün 1. kuvvetinden 4. kuvvetine kadar olan değerleri hesaplayarak yaz.", 
                    quiz: [
                        { q: "2 üzeri 3 kaçtır?", opts: ["6", "8", "9"], ans: 1 },
                        { q: "5'in karesi ifadesi hangisidir?", opts: ["5x2", "5+5", "5x5"], ans: 2 },
                        { q: "Bir sayının sıfırıncı kuvveti kaçtır?", opts: ["0", "1", "Kendisi"], ans: 1 }
                    ] 
                } 
            },
            { 
                id: "mat3", title: "Oran ve Orantı", points: 100, 
                content: { 
                    text: "İki çokluğun birbirine bölünerek karşılaştırılmasına oran denir. İki oranın eşitliğine ise orantı denir. Örneğin 1 bardak pirince 2 bardak su konulması bir orandır (1/2).",
                    images: ["https://images.unsplash.com/photo-1554224155-6726b3ff858f?w=800"], 
                    video: "https://www.youtube.com/embed/Wy7yDqRzCgE", 
                    examples: ["Örnek: Sınıfta 10 kız, 20 erkek varsa, kızların erkeklere oranı 1/2'dir.", "Örnek: Hız = Yol / Zaman bir orandır."], 
                    activityType: "draw", 
                    activityText: "Bir pasta grafiği çiz ve yarısını boyayarak 1/2 oranını göster.", 
                    quiz: [
                        { q: "3 elmanın 6 elmaya oranı nedir?", opts: ["1/2", "1/3", "2/1"], ans: 0 },
                        { q: "Orantı nedir?", opts: ["İki oranın eşitliği", "Toplama işlemi", "Çarpma işlemi"], ans: 0 },
                        { q: "Hangisi birim kesirdir?", opts: ["3/5", "1/4", "5/2"], ans: 1 }
                    ] 
                } 
            }
        ]
    },

    // --- 2. EDEBİYAT ---
    {
        subject: "Edebiyat",
        lessons: [
            { 
                id: "edb1", title: "Şiir Bilgisi", points: 100, 
                content: {
                    text: "Şiir, duyguların, hayallerin ve düşüncelerin ölçülü ve ahenkli bir dille anlatılmasıdır. Şiirde dize (mısra), kıta (dörtlük) ve kafiye (uyak) gibi unsurlar bulunur.",
                    images: ["https://images.unsplash.com/photo-1457369804613-52c61a468e7d?w=800"],
                    video: "https://www.youtube.com/embed/example", // Geçerli bir link eklenebilir
                    examples: ["Örnek: 'Sessiz gemi' şiiri Yahya Kemal'e aittir.", "Örnek: Dörtlük, dört dizeden oluşan şiir birimidir."],
                    activityType: "text", 
                    activityText: "En sevdiğin bir şiirden bir dörtlük yaz veya kendin kısa bir şiir denemesi yap.",
                    quiz: [
                        { q: "Şiirin her bir satırına ne denir?", opts: ["Cümle", "Dize", "Paragraf"], ans: 1 },
                        { q: "Dört dizeden oluşan bütüne ne ad verilir?", opts: ["Dörtlük", "Beyit", "Bent"], ans: 0 },
                        { q: "Mısra sonlarındaki ses benzerliğine ne denir?", opts: ["Redif", "Kafiye", "Ölçü"], ans: 1 }
                    ]
                }
            },
            { 
                id: "edb2", title: "Hikaye Unsurları", points: 100, 
                content: {
                    text: "Bir hikayede mutlaka bulunması gereken 4 temel unsur vardır: Olay (ne olduğu), Kişiler (kahramanlar), Yer (mekan) ve Zaman. Bu unsurlar kurguyu oluşturur.",
                    images: ["https://images.unsplash.com/photo-1474932430478-367dbb6832c1?w=800"],
                    video: "https://www.youtube.com/embed/example",
                    examples: ["Örnek: Kırmızı Başlıklı Kız hikayesinde mekan ormandır.", "Örnek: Olay, kurdun büyükanneyi kandırmasıdır."],
                    activityType: "draw", 
                    activityText: "Kendi hayalindeki bir hikaye kahramanını çiz.",
                    quiz: [
                        { q: "Hangisi hikaye unsuru değildir?", opts: ["Zaman", "Mekan", "Kafiye"], ans: 2 },
                        { q: "Hikayede olayı yaşayanlara ne denir?", opts: ["Yazar", "Kişiler", "Okur"], ans: 1 },
                        { q: "Hikayenin geçtiği yere ne ad verilir?", opts: ["Zaman", "Mekan", "Konu"], ans: 1 }
                    ]
                }
            },
            { 
                id: "edb3", title: "Noktalama İşaretleri", points: 100, 
                content: {
                    text: "Yazıda okumayı kolaylaştırmak ve anlam karışıklığını önlemek için noktalama işaretleri kullanılır. Nokta biten cümleye, virgül eş görevli kelimelere konur.",
                    images: ["https://images.unsplash.com/photo-1455390582262-044cdead277a?w=800"],
                    video: "https://www.youtube.com/embed/example",
                    examples: ["Örnek: Ali, Ayşe ve Mehmet geldiler.", "Örnek: Eyvah! Geç kaldım (Ünlem)."],
                    activityType: "text", 
                    activityText: "Nokta, virgül ve soru işaretinin kullanıldığı üç farklı cümle yaz.",
                    quiz: [
                        { q: "Biten cümlenin sonuna ne konur?", opts: ["Virgül", "Nokta", "İki nokta"], ans: 1 },
                        { q: "Soru bildiren cümlelerin sonuna ne gelir?", opts: ["Ünlem", "Soru İşareti", "Nokta"], ans: 1 },
                        { q: "Eş görevli kelimeleri ayırmak için ne kullanılır?", opts: ["Virgül", "Noktalı virgül", "Kısa çizgi"], ans: 0 }
                    ]
                }
            }
        ]
    },

    // --- 3. RESİM ---
    {
        subject: "Resim",
        lessons: [
            { 
                id: "rsm1", title: "Renk Teorisi", points: 100, 
                content: {
                    text: "Renkler ana ve ara renkler olarak ayrılır. Ana renkler Kırmızı, Sarı ve Mavidir. Bu renklerin karışımıyla Turuncu, Yeşil ve Mor gibi ara renkler oluşur.",
                    images: ["https://images.unsplash.com/photo-1525909002-1b05e0c869d8?w=800"],
                    video: "https://www.youtube.com/embed/example",
                    examples: ["Sarı + Mavi = Yeşil", "Kırmızı + Sarı = Turuncu", "Mavi + Kırmızı = Mor"],
                    activityType: "draw", 
                    activityText: "Ekrana üç tane daire çiz ve içlerini ana renklerle boyadığını hayal et (veya etiketle).",
                    quiz: [
                        { q: "Hangisi ana renktir?", opts: ["Yeşil", "Mavi", "Turuncu"], ans: 1 },
                        { q: "Sarı ve Kırmızı karışırsa hangi renk olur?", opts: ["Mor", "Yeşil", "Turuncu"], ans: 2 },
                        { q: "Sıcak renk hangisidir?", opts: ["Mavi", "Kırmızı", "Mor"], ans: 1 }
                    ]
                }
            },
            { 
                id: "rsm2", title: "Perspektif", points: 100, 
                content: {
                    text: "Perspektif, iki boyutlu yüzeye üç boyutlu derinlik hissi verme tekniğidir. Nesneler uzaklaştıkça küçülür ve bir kaçış noktasında birleşir.",
                    images: ["https://images.unsplash.com/photo-1558865869-c93f6f8482af?w=800"],
                    video: "https://www.youtube.com/embed/example",
                    examples: ["Örnek: Tren raylarının ufukta birleşiyormuş gibi görünmesi.", "Örnek: Yakındaki ağaçların uzaktakilerden büyük görünmesi."],
                    activityType: "draw", 
                    activityText: "Ufuk çizgisi ve tek bir kaçış noktası olan bir tren yolu çiz.",
                    quiz: [
                        { q: "Nesnelerin uzaklaştıkça küçülmesi ne ile ilgilidir?", opts: ["Renk", "Perspektif", "Doku"], ans: 1 },
                        { q: "Çizgilerin birleştiği noktaya ne denir?", opts: ["Kaçış noktası", "Başlangıç noktası", "Orta nokta"], ans: 0 },
                        { q: "Perspektif çizime ne katar?", opts: ["Renk", "Derinlik", "Ses"], ans: 1 }
                    ]
                }
            },
            { 
                id: "rsm3", title: "Portre Çizimi", points: 100, 
                content: {
                    text: "Portre, bir kişinin yüzünün çizimidir. Yüz oranlarına dikkat etmek önemlidir. Gözler genellikle başın tam ortasında yer alır.",
                    images: ["https://images.unsplash.com/photo-1578301978693-85fa9c0320b9?w=800"],
                    video: "https://www.youtube.com/embed/example",
                    examples: ["Örnek: İki göz arasındaki mesafe bir göz boyutu kadardır.", "Örnek: Kulaklar kaş ve burun hizasındadır."],
                    activityType: "draw", 
                    activityText: "Basit bir insan yüzü taslağı çiz (Oval kafa, gözler, burun, ağız).",
                    quiz: [
                        { q: "İnsan yüzü çizimine ne ad verilir?", opts: ["Manzara", "Natürmort", "Portre"], ans: 2 },
                        { q: "Gözler yüzün neresinde bulunur?", opts: ["Alında", "Tam ortada", "Çenede"], ans: 1 },
                        { q: "İki göz arası mesafe ne kadardır?", opts: ["Bir göz boyu", "İki parmak", "Hiç"], ans: 0 }
                    ]
                }
            }
        ]
    },

    // --- 4. MÜZİK ---
    {
        subject: "Müzik",
        lessons: [
            { 
                id: "mzk1", title: "Notaları Öğrenelim", points: 100, 
                content: {
                    text: "Müzik seslerini gösteren işaretlere nota denir. Temel notalar Do, Re, Mi, Fa, Sol, La, Si'dir. Notalar dizek (porte) üzerine yazılır.",
                    images: ["https://images.unsplash.com/photo-1507838153414-b4b713384ebd?w=800"],
                    video: "https://www.youtube.com/embed/example",
                    examples: ["Örnek: Sol anahtarı dizeğin başındadır.", "Örnek: Kalın sesler aşağıda, ince sesler yukarıda olur."],
                    activityType: "draw", 
                    activityText: "Beş çizgiden oluşan bir dizek ve başına Sol Anahtarı çiz.",
                    quiz: [
                        { q: "Müziğin yazı diline ne denir?", opts: ["Nota", "Şiir", "Resim"], ans: 0 },
                        { q: "Kaç tane ana nota vardır?", opts: ["5", "7", "12"], ans: 1 },
                        { q: "Notaların yazıldığı 5 çizgili alana ne denir?", opts: ["Dizek", "Satır", "Kare"], ans: 0 }
                    ]
                }
            },
            { 
                id: "mzk2", title: "Ritim ve Vuruş", points: 100, 
                content: {
                    text: "Ritim, müziğin zaman içindeki akışıdır. Düzenli tekrar eden vuruşlar ritmi oluşturur. Saat tiktakları gibi düzenli vuruşlara tempo denir.",
                    images: ["https://images.unsplash.com/photo-1519892300165-cb5542fb47c7?w=800"],
                    video: "https://www.youtube.com/embed/example",
                    examples: ["Örnek: Marşlarda ritim 2/4'lüktür (Bir-Ki, Bir-Ki).", "Örnek: Vals ritmi 3/4'lüktür (Bir-ki-üç)."],
                    activityType: "text", 
                    activityText: "Masaya elinle 4 vuruşluk bir ritim vur ve bunu yazı ile ifade et (Dum-Tek-Tek-Dum gibi).",
                    quiz: [
                        { q: "Müziğin zaman içindeki akışına ne denir?", opts: ["Melodi", "Ritim", "Armoni"], ans: 1 },
                        { q: "Müziğin hızına ne ad verilir?", opts: ["Tempo", "Ses", "Gürlük"], ans: 0 },
                        { q: "Hangisi bir ritim aletidir?", opts: ["Keman", "Davul", "Flüt"], ans: 1 }
                    ]
                }
            },
            { 
                id: "mzk3", title: "Müzik Aletleri", points: 100, 
                content: {
                    text: "Müzik aletleri çalınış biçimlerine göre ayrılır: Telli (Gitar), Vurmalı (Davul), Üflemeli (Flüt) ve Yaylı (Keman) çalgılar.",
                    images: ["https://images.unsplash.com/photo-1511192336575-5a79af67a629?w=800"],
                    video: "https://www.youtube.com/embed/example",
                    examples: ["Örnek: Piyano tuşlu bir çalgıdır.", "Örnek: Bağlama telli bir Türk halk çalgısıdır."],
                    activityType: "draw", 
                    activityText: "En sevdiğin müzik aletinin resmini çiz.",
                    quiz: [
                        { q: "Hangisi üflemeli bir çalgıdır?", opts: ["Davul", "Ney", "Gitar"], ans: 1 },
                        { q: "Keman nasıl çalınır?", opts: ["Vurarak", "Üfleyerek", "Yay ile"], ans: 2 },
                        { q: "Piyano hangi gruba girer?", opts: ["Tuşlu", "Üflemeli", "Vurmalı"], ans: 0 }
                    ]
                }
            }
        ]
    },

    // --- 5. FEN BİLİMLERİ ---
    {
        subject: "Fen Bilimleri",
        lessons: [
            { 
                id: "fen1", title: "Kuvvet ve Hareket", points: 100, 
                content: {
                    text: "Kuvvet, duran bir cismi hareket ettiren, hareket eden cismi durduran etkidir. İtme ve çekme birer kuvvettir. Kuvvetin birimi Newton'dur (N).",
                    images: ["https://images.unsplash.com/photo-1633259584604-af7789377c65?w=800"],
                    video: "https://www.youtube.com/embed/example",
                    examples: ["Örnek: Topa vurmak itme kuvvetidir.", "Örnek: Kapıyı açmak çekme veya itme kuvveti olabilir."],
                    activityType: "draw", 
                    activityText: "Bir kutuyu iten bir çöp adam çiz ve kuvvetin yönünü ok ile göster.",
                    quiz: [
                        { q: "Kuvvetin birimi nedir?", opts: ["Metre", "Newton", "Kilogram"], ans: 1 },
                        { q: "Hangisi temas gerektirmeyen kuvvettir?", opts: ["İtme", "Mıknatıs", "Vurma"], ans: 1 },
                        { q: "Hareketli cismi durduran etkiye ne denir?", opts: ["Kuvvet", "Enerji", "Isı"], ans: 0 }
                    ]
                }
            },
            { 
                id: "fen2", title: "Elektrik Devreleri", points: 100, 
                content: {
                    text: "Basit bir elektrik devresi pil (güç kaynağı), kablo (iletken) ve ampulden (yük) oluşur. Anahtar devreyi açıp kapatmaya yarar.",
                    images: ["https://images.unsplash.com/photo-1456428199391-a3b1cb5e93ab?w=800"],
                    video: "https://www.youtube.com/embed/example",
                    examples: ["Örnek: Pilin (+) ve (-) kutupları vardır.", "Örnek: Anahtar açıkken ampul yanmaz."],
                    activityType: "draw", 
                    activityText: "Bir pil, bir ampul ve bir anahtardan oluşan basit bir devre şeması çiz.",
                    quiz: [
                        { q: "Devrenin güç kaynağı nedir?", opts: ["Kablo", "Pil", "Ampul"], ans: 1 },
                        { q: "Devreyi kontrol eden eleman hangisidir?", opts: ["Anahtar", "Duy", "Pil"], ans: 0 },
                        { q: "Elektriği ileten maddeye ne denir?", opts: ["Yalıtkan", "İletken", "Plastik"], ans: 1 }
                    ]
                }
            },
            { 
                id: "fen3", title: "Işık ve Ses", points: 100, 
                content: {
                    text: "Işık bir enerjidir ve doğrusal yolla yayılır. Ses ise titreşim sonucu oluşur ve dalgalar halinde yayılır. Işık boşlukta yayılır ama ses boşlukta yayılmaz.",
                    images: ["https://images.unsplash.com/photo-1504198070170-4ca53bb1c1fa?w=800"],
                    video: "https://www.youtube.com/embed/example",
                    examples: ["Örnek: Güneş doğal ışık kaynağıdır.", "Örnek: Uzayda patlama olsa sesi duyulmaz (boşluk)."],
                    activityType: "text", 
                    activityText: "Çevrendeki doğal ve yapay ışık kaynaklarına ikişer örnek yaz.",
                    quiz: [
                        { q: "Işık nasıl yayılır?", opts: ["Dalgalar halinde", "Doğrusal", "Eğri"], ans: 1 },
                        { q: "Ses nerede yayılmaz?", opts: ["Suda", "Havada", "Boşlukta"], ans: 2 },
                        { q: "En büyük doğal ışık kaynağımız nedir?", opts: ["Ay", "Güneş", "Ateş"], ans: 1 }
                    ]
                }
            }
        ]
    },

    // --- 6. KİMYA ---
    {
        subject: "Kimya",
        lessons: [
            { 
                id: "kim1", title: "Maddenin Halleri", points: 100, 
                content: {
                    text: "Madde doğada üç temel halde bulunur: Katı, Sıvı ve Gaz. Katıların belli şekli vardır. Sıvılar konulduğu kabın şeklini alır. Gazlar ise bulunduğu ortama yayılır.",
                    images: ["https://images.unsplash.com/photo-1532634993-15f421e42ec0?w=800"],
                    video: "https://www.youtube.com/embed/example",
                    examples: ["Katı: Buz, Demir", "Sıvı: Su, Süt", "Gaz: Su buharı, Oksijen"],
                    activityType: "draw", 
                    activityText: "Katı (Buz), Sıvı (Su) ve Gaz (Bulut) hallerini gösteren bir döngü çiz.",
                    quiz: [
                        { q: "Belli bir şekli olan madde hali hangisidir?", opts: ["Sıvı", "Gaz", "Katı"], ans: 2 },
                        { q: "Sıvıdan gaza geçişe ne denir?", opts: ["Buharlaşma", "Donma", "Erime"], ans: 0 },
                        { q: "Hangisi gaz halindedir?", opts: ["Taş", "Hava", "Yağ"], ans: 1 }
                    ]
                }
            },
            { 
                id: "kim2", title: "Atomun Yapısı", points: 100, 
                content: {
                    text: "Atom, maddenin en küçük yapıtaşıdır. Merkezde Çekirdek (Proton ve Nötron), etrafında ise Katmanlar (Elektronlar) bulunur.",
                    images: ["https://images.unsplash.com/photo-1614728263952-84ea256f9679?w=800"],
                    video: "https://www.youtube.com/embed/example",
                    examples: ["Proton: Pozitif yüklü (+)", "Elektron: Negatif yüklü (-)", "Nötron: Yüksüz"],
                    activityType: "draw", 
                    activityText: "Basit bir atom modeli çiz: Ortada çekirdek, etrafta dönen elektronlar.",
                    quiz: [
                        { q: "Atomun merkezinde ne bulunur?", opts: ["Elektron", "Çekirdek", "Kabuk"], ans: 1 },
                        { q: "Negatif yüklü parçacık hangisidir?", opts: ["Proton", "Nötron", "Elektron"], ans: 2 },
                        { q: "Maddenin en küçük yapıtaşı nedir?", opts: ["Hücre", "Atom", "Molekül"], ans: 1 }
                    ]
                }
            },
            { 
                id: "kim3", title: "Periyodik Tablo", points: 100, 
                content: {
                    text: "Elementlerin artan atom numaralarına göre dizildiği tabloya Periyodik Tablo denir. Yatay sıralara Periyot, dikey sütunlara Grup denir.",
                    images: ["https://images.unsplash.com/photo-1532187863486-abf9dbad1bb3?w=800"],
                    video: "https://www.youtube.com/embed/example",
                    examples: ["H: Hidrojen (1 numaralı element)", "O: Oksijen", "Fe: Demir"],
                    activityType: "text", 
                    activityText: "Bildigin 3 elementin adını ve sembolünü yaz (Örn: Oksijen - O).",
                    quiz: [
                        { q: "Periyodik tablodaki ilk element nedir?", opts: ["Helyum", "Hidrojen", "Lityum"], ans: 1 },
                        { q: "Dikey sütunlara ne ad verilir?", opts: ["Periyot", "Grup", "Sıra"], ans: 1 },
                        { q: "Oksijenin sembolü nedir?", opts: ["Ok", "O", "Ox"], ans: 1 }
                    ]
                }
            }
        ]
    }
];
