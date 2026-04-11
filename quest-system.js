// ============================================
// GLOBAL GÖREV SİSTEMİ - Mucit Simülatörü  
// quest-system.js - Ana dizine kaydedin
// ============================================

class QuestManager {
    constructor() {
        this.quests = this.loadQuests();
        this.activeQuests = this.loadActiveQuests();
        this.completedQuests = this.loadCompletedQuests();
        this.totalScore = parseInt(localStorage.getItem('userScore')) || 0;
        this.visitedLocations = this.loadVisitedLocations();
        this.productionCategories = this.loadProductionCategories();
        this.init();
    }

    init() {
        this.createQuestUI();
        this.updateQuestDisplay();
        this.checkMilestones();
    }

    loadQuests() {
        return [
            // OKUL
            { id: 'school_first_lesson', title: '📚 İlk Dersini Tamamla', description: 'Okulda herhangi bir dersi bitir', location: 'Okul', reward: 100, type: 'lesson' },
            { id: 'school_all_subjects', title: '🎓 Tüm Dersleri Bitir', description: 'Okuldaki 3 farklı dersi tamamla', location: 'Okul', reward: 500, type: 'lesson' },
            
            // CAFE
            { id: 'cafe_first_order', title: '☕ İlk Siparişi Al', description: 'Cafedeki iş panosundan bir sipariş kabul et', location: 'Cafe', reward: 50, type: 'order' },
            { id: 'cafe_delivery_complete', title: '🚚 Siparişi Teslim Et', description: 'Aldığın siparişi üret ve teslim et', location: 'Cafe → Atölye', reward: 200, type: 'order' },
            
            // ATÖLYE
            { id: 'workshop_first_vehicle', title: '🚁 İlk Aracını Üret', description: 'Atölyede herhangi bir araç üret', location: 'Atölye', reward: 150, type: 'production' },
            { id: 'workshop_test_success', title: '✅ Başarılı Test', description: 'Test sürüşünde 300+ puan al', location: 'Atölye', reward: 300, type: 'production' },
            { id: 'workshop_all_categories', title: '🏆 Tüm Kategoriler', description: 'Hava, Kara ve Robot kategorilerinden üretim yap', location: 'Atölye', reward: 600, type: 'production' },
            
            // OFİS
            { id: 'office_first_presentation', title: '💼 İlk Sunumun', description: 'Ofiste bir proje sunumu yap', location: 'Ofis', reward: 100, type: 'presentation' },
            { id: 'office_high_score', title: '⭐ Mükemmel Sunum', description: 'Sunumdan 85+ puan al', location: 'Ofis', reward: 250, type: 'presentation' },
            
            // EV
            { id: 'home_check_profile', title: '👤 Profilini İncele', description: 'Evde karakter özelliklerini kontrol et', location: 'Ev', reward: 25, type: 'exploration' },
            
            // GENEL
            { id: 'general_visit_all', title: '🗺️ Tüm Bölgeleri Keşfet', description: 'Okul, Cafe, Atölye, Ofis ve Evi ziyaret et', location: 'Kasaba', reward: 200, type: 'exploration' },
            { id: 'general_master_inventor', title: '🔧 Usta Mucit', description: 'Toplam 2000 puan kazan', location: 'Kasaba', reward: 1000, type: 'milestone' }
        ];
    }

    loadActiveQuests() {
        const saved = localStorage.getItem('activeQuests');
        return saved ? JSON.parse(saved) : [];
    }

    loadCompletedQuests() {
        const saved = localStorage.getItem('completedQuests');
        return saved ? JSON.parse(saved) : [];
    }

    loadVisitedLocations() {
        const saved = localStorage.getItem('visitedLocations');
        return saved ? JSON.parse(saved) : [];
    }

    loadProductionCategories() {
        const saved = localStorage.getItem('productionCategories');
        return saved ? JSON.parse(saved) : [];
    }

    saveProgress() {
        localStorage.setItem('activeQuests', JSON.stringify(this.activeQuests));
        localStorage.setItem('completedQuests', JSON.stringify(this.completedQuests));
        localStorage.setItem('userScore', this.totalScore);
        localStorage.setItem('visitedLocations', JSON.stringify(this.visitedLocations));
        localStorage.setItem('productionCategories', JSON.stringify(this.productionCategories));
    }

    createQuestUI() {
        if (document.getElementById('quest-panel')) return;

        const panel = document.createElement('div');
        panel.id = 'quest-panel';
        panel.style.cssText = `
            position: fixed; top: 80px; right: 20px; width: 350px; max-height: 500px;
            background: rgba(0,0,0,0.9); border: 3px solid #22c55e; border-radius: 15px;
            padding: 20px; color: white; z-index: 9000; overflow-y: auto;
            font-family: 'Segoe UI', sans-serif; box-shadow: 0 10px 30px rgba(0,0,0,0.5);
        `;

        panel.innerHTML = `
            <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 15px;">
                <h3 style="margin: 0; color: #22c55e;">📋 GÖREVLER</h3>
                <button id="toggle-quest-panel" style="background: none; border: none; color: white; font-size: 20px; cursor: pointer;">−</button>
            </div>
            <div style="margin-bottom: 15px; padding: 10px; background: rgba(34,197,94,0.2); border-radius: 8px; text-align: center;">
                <div style="font-size: 12px; color: #22c55e;">TOPLAM PUAN</div>
                <div id="quest-total-score" style="font-size: 24px; font-weight: bold; color: #fbbf24;">${this.totalScore}</div>
            </div>
            <div id="quest-list"></div>
        `;

        document.body.appendChild(panel);

        document.getElementById('toggle-quest-panel').addEventListener('click', () => {
            const list = document.getElementById('quest-list');
            const btn = document.getElementById('toggle-quest-panel');
            if (list.style.display === 'none') {
                list.style.display = 'block';
                btn.textContent = '−';
                panel.style.maxHeight = '500px';
            } else {
                list.style.display = 'none';
                btn.textContent = '+';
                panel.style.maxHeight = '80px';
            }
        });
    }

    updateQuestDisplay() {
        const listElement = document.getElementById('quest-list');
        const scoreElement = document.getElementById('quest-total-score');
        
        if (!listElement) return;
        if (scoreElement) scoreElement.textContent = this.totalScore;

        let html = '<h4 style="color: #fbbf24; font-size: 14px; margin-bottom: 10px;">⭐ AKTİF GÖREVLER</h4>';
        
        const activeQuestsList = this.quests.filter(q => 
            this.activeQuests.includes(q.id) && !this.completedQuests.includes(q.id)
        );

        if (activeQuestsList.length === 0) {
            html += '<p style="color: #888; font-size: 12px;">Görev almak için bölgeleri ziyaret et!</p>';
        } else {
            activeQuestsList.forEach(quest => html += this.createQuestCard(quest, 'active'));
        }

        html += '<h4 style="color: #22c55e; font-size: 14px; margin: 15px 0 10px 0;">✅ TAMAMLANAN</h4>';
        
        const completedQuestsList = this.quests.filter(q => this.completedQuests.includes(q.id));

        if (completedQuestsList.length === 0) {
            html += '<p style="color: #888; font-size: 12px;">Henüz görev tamamlanmadı</p>';
        } else {
            completedQuestsList.slice(-3).forEach(quest => html += this.createQuestCard(quest, 'completed'));
        }

        listElement.innerHTML = html;
    }

    createQuestCard(quest, status) {
        const isCompleted = status === 'completed';
        return `
            <div style="background: ${isCompleted ? 'rgba(34,197,94,0.2)' : 'rgba(59,130,246,0.2)'};
                border-left: 4px solid ${isCompleted ? '#22c55e' : '#3b82f6'};
                padding: 12px; margin-bottom: 10px; border-radius: 8px; ${isCompleted ? 'opacity: 0.7;' : ''}">
                <div style="font-weight: bold; font-size: 13px; margin-bottom: 5px;">${quest.title}</div>
                <div style="font-size: 11px; color: #ccc; margin-bottom: 5px;">${quest.description}</div>
                <div style="display: flex; justify-content: space-between; font-size: 11px;">
                    <span style="color: #888;">📍 ${quest.location}</span>
                    <span style="color: #fbbf24; font-weight: bold;">+${quest.reward} puan</span>
                </div>
            </div>
        `;
    }

    activateQuest(questId) {
        if (!this.activeQuests.includes(questId) && !this.completedQuests.includes(questId)) {
            this.activeQuests.push(questId);
            this.saveProgress();
            this.updateQuestDisplay();
            const quest = this.quests.find(q => q.id === questId);
            this.showNotification(`Yeni görev: ${quest.title}`, 'info');
        }
    }

    completeQuest(questId) {
        const quest = this.quests.find(q => q.id === questId);
        if (!quest || this.completedQuests.includes(questId)) return false;

        this.completedQuests.push(questId);
        this.totalScore += quest.reward;
        
        const index = this.activeQuests.indexOf(questId);
        if (index > -1) this.activeQuests.splice(index, 1);

        this.saveProgress();
        this.updateQuestDisplay();
        this.showNotification(`✅ Görev Tamamlandı!\n${quest.title}\n+${quest.reward} puan!`, 'success');
        this.playSuccessSound();
        this.checkMilestones();
        return true;
    }

    visitLocation(locationName) {
        if (!this.visitedLocations.includes(locationName)) {
            this.visitedLocations.push(locationName);
            this.saveProgress();

            if (this.visitedLocations.length >= 5) {
                this.completeQuest('general_visit_all');
            }
        }
    }

    trackProduction(category) {
        if (!this.productionCategories.includes(category)) {
            this.productionCategories.push(category);
            this.saveProgress();

            if (this.productionCategories.length >= 3) {
                this.completeQuest('workshop_all_categories');
            }
        }
    }

    checkMilestones() {
        if (this.totalScore >= 2000) {
            this.completeQuest('general_master_inventor');
        }
    }

    showNotification(message, type = 'info') {
        const notification = document.createElement('div');
        notification.style.cssText = `
            position: fixed; top: 100px; right: 20px;
            background: ${type === 'success' ? '#22c55e' : '#3b82f6'};
            color: white; padding: 20px; border-radius: 12px; z-index: 10000;
            min-width: 300px; box-shadow: 0 10px 25px rgba(0,0,0,0.3);
            animation: slideIn 0.3s ease; font-weight: bold; white-space: pre-line;
        `;
        
        notification.textContent = message;
        document.body.appendChild(notification);

        setTimeout(() => {
            notification.style.animation = 'slideOut 0.3s ease';
            setTimeout(() => notification.remove(), 300);
        }, 3000);
    }

    playSuccessSound() {
        try {
            const audioContext = new (window.AudioContext || window.webkitAudioContext)();
            [523, 659, 784].forEach((freq, i) => {
                setTimeout(() => {
                    const osc = audioContext.createOscillator();
                    const gain = audioContext.createGain();
                    osc.connect(gain);
                    gain.connect(audioContext.destination);
                    osc.frequency.value = freq;
                    gain.gain.setValueAtTime(0.2, audioContext.currentTime);
                    gain.gain.exponentialRampToValueAtTime(0.01, audioContext.currentTime + 0.3);
                    osc.start();
                    osc.stop(audioContext.currentTime + 0.3);
                }, i * 100);
            });
        } catch (e) { console.log('Ses hatası'); }
    }
}

// CSS
const style = document.createElement('style');
style.textContent = `
    @keyframes slideIn { from { transform: translateX(400px); opacity: 0; } to { transform: translateX(0); opacity: 1; } }
    @keyframes slideOut { from { transform: translateX(0); opacity: 1; } to { transform: translateX(400px); opacity: 0; } }
`;
document.head.appendChild(style);

window.questManager = new QuestManager();
