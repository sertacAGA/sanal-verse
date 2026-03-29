// --- DATA ---

const parts = {
  body: [
    { id: "light", name:"Hafif Gövde", desc:"Hızlı ama zayıf", speed: 30, damage: 5 },
    { id: "heavy", name:"Ağır Gövde", desc:"Yavaş ama güçlü", speed: 10, damage: 25 }
  ],
  weapon: [
    { id: "laser", name:"Lazer", desc:"Hızlı atış", damage: 15 },
    { id: "cannon", name:"Top", desc:"Yüksek hasar", damage: 30 }
  ]
};

const modules = [
  { id: "target_ai", name: "Hedef AI", desc: "Otomatik hedef bulur" },
  { id: "nav_ai", name: "Navigasyon AI", desc: "En kısa yolu bulur" }
];

let currentProject = {
  parts: [],
  modules: []
};

// --- NAVIGATION ---
function loadPage(page) {
  if (page === "career") renderCareer();
  if (page === "project") renderProject();
}

// --- CAREER ---
function renderCareer() {
  document.getElementById("content").innerHTML = `
    <h2>Kariyer</h2>
    <div class="card">
      <p>🔧 Mühendis: Yeni parçalar açar</p>
      <p>🎨 Tasarımcı: Görsel ve modül açar</p>
      <p>💻 Yazılımcı: AI modülleri açar</p>
    </div>
  `;
}

// --- PROJECT MAIN ---
function renderProject() {
  document.getElementById("content").innerHTML = `
    <h2>Proje Editörü</h2>

    <div class="card">
      <h3>Parçalar</h3>
      <button onclick="openPartSelect()">+ Parça Ekle</button>
      <div id="partList"></div>
    </div>

    <div class="card">
      <h3>Modüller</h3>
      <button onclick="openModuleSelect()">+ Modül Ekle</button>
      <div id="moduleList"></div>
    </div>

    <div class="card">
      <h3>Performans</h3>
      <div id="stats"></div>
    </div>
  `;

  updateLists();
  updateStats();
}

// --- PART SELECT ---
function openPartSelect() {
  document.getElementById("content").innerHTML = `
    <h2>Parça Seç</h2>

    ${Object.keys(parts).map(type => `
      <h3>${type.toUpperCase()}</h3>
      <div class="parts">
        ${parts[type].map(p => `
          <div class="part" onclick="addPart('${type}','${p.id}')">
            <h4>${p.name}</h4>
            <p>${p.desc}</p>
          </div>
        `).join("")}
      </div>
    `).join("")}

    <button onclick="renderProject()">← Geri</button>
  `;
}

// --- MODULE SELECT ---
function openModuleSelect() {
  document.getElementById("content").innerHTML = `
    <h2>Modül Seç</h2>

    <div class="parts">
      ${modules.map(m => `
        <div class="part" onclick="addModule('${m.id}')">
          <h4>${m.name}</h4>
          <p>${m.desc}</p>
        </div>
      `).join("")}
    </div>

    <button onclick="renderProject()">← Geri</button>
  `;
}

// --- ADD FUNCTIONS ---
function addPart(type, id) {
  const part = parts[type].find(p => p.id === id);
  currentProject.parts.push(part);
  renderProject();
}

function addModule(id) {
  const mod = modules.find(m => m.id === id);
  currentProject.modules.push(mod);
  renderProject();
}

// --- UPDATE LISTS ---
function updateLists() {
  document.getElementById("partList").innerHTML =
    currentProject.parts.length
      ? currentProject.parts.map(p => `<p>🔧 ${p.name}</p>`).join("")
      : "<p>Henüz parça eklenmedi</p>";

  document.getElementById("moduleList").innerHTML =
    currentProject.modules.length
      ? currentProject.modules.map(m => `<p>🧠 ${m.name}</p>`).join("")
      : "<p>Henüz modül eklenmedi</p>";
}

// --- CALCULATE ---
function calculateStats() {
  let stats = { speed: 0, damage: 0 };

  currentProject.parts.forEach(p => {
    stats.speed += p.speed || 0;
    stats.damage += p.damage || 0;
  });

  return stats;
}

// --- UPDATE STATS ---
function updateStats() {
  const stats = calculateStats();

  document.getElementById("stats").innerHTML = `
    <p>Hız</p>
    <div class="bar"><div class="fill" style="width:${stats.speed}%"></div></div>

    <p>Hasar</p>
    <div class="bar"><div class="fill" style="width:${stats.damage}%"></div></div>
  `;
}

// --- INIT ---
loadPage("project");
