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

let currentProject = {
  body: null,
  weapon: null
};

// --- NAV ---
function loadPage(page) {
  if (page === "career") renderCareer();
  if (page === "project") renderProject();
}

// --- CAREER ---
function renderCareer() {
  document.getElementById("content").innerHTML = `
    <h2>Kariyer</h2>
    <div class="card">
      <p>Mühendis: 1</p>
      <p>Tasarımcı: 1</p>
      <p>Yazılımcı: 1</p>
    </div>
  `;
}

// --- PROJECT ---
function renderProject() {
  document.getElementById("content").innerHTML = `
    <h2>Drone Projesi</h2>

    <div class="card">
      <h3>Gövde Seç</h3>
      <div class="parts">
        ${parts.body.map(p => partCard(p, "body")).join("")}
      </div>
    </div>

    <div class="card">
      <h3>Silah Seç</h3>
      <div class="parts">
        ${parts.weapon.map(p => partCard(p, "weapon")).join("")}
      </div>
    </div>

    <div class="card">
      <h3>Performans</h3>
      <div id="stats"></div>
    </div>
  `;

  updateStats();
}

function partCard(p, type) {
  const selected = currentProject[type]?.id === p.id ? "selected" : "";

  return `
    <div class="part ${selected}" onclick="selectPart('${type}','${p.id}')">
      <h4>${p.name}</h4>
      <p>${p.desc}</p>
    </div>
  `;
}

// --- LOGIC ---
function selectPart(type, id) {
  currentProject[type] = parts[type].find(p => p.id === id);
  renderProject();
}

function calculateStats() {
  let stats = { speed: 0, damage: 0 };

  Object.values(currentProject).forEach(p => {
    if (!p) return;
    stats.speed += p.speed || 0;
    stats.damage += p.damage || 0;
  });

  return stats;
}

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
