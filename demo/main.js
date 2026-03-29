// --- DATA ---
const parts = {
  body: [
    { id: "light", speed: 20, damage: 5 },
    { id: "heavy", speed: 5, damage: 20 }
  ],
  weapon: [
    { id: "laser", damage: 15 },
    { id: "cannon", damage: 25 }
  ]
};

let currentProject = {
  body: null,
  weapon: null
};

// --- NAVIGATION ---
function loadPage(page) {
  if (page === "career") renderCareer();
  if (page === "project") renderProject();
}

// --- COMPONENTS ---

function renderCareer() {
  document.getElementById("content").innerHTML = `
    <h2>Kariyer</h2>
    <div class="card">
      <p>Mühendis: Seviye 1</p>
      <p>Tasarımcı: Seviye 1</p>
      <p>Yazılımcı: Seviye 1</p>
    </div>
  `;
}

// --- PROJECT SYSTEM ---

function renderProject() {
  document.getElementById("content").innerHTML = `
    <h2>Proje Oluştur</h2>

    <div class="card">
      <h3>Gövde Seç</h3>
      ${parts.body.map(p => `
        <button onclick="selectPart('body','${p.id}')">${p.id}</button>
      `).join("")}
    </div>

    <div class="card">
      <h3>Silah Seç</h3>
      ${parts.weapon.map(p => `
        <button onclick="selectPart('weapon','${p.id}')">${p.id}</button>
      `).join("")}
    </div>

    <div class="card">
      <h3>Statlar</h3>
      <div id="stats"></div>
    </div>
  `;

  updateStats();
}

// --- LOGIC ---

function selectPart(type, id) {
  currentProject[type] = parts[type].find(p => p.id === id);
  updateStats();
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
    <p>Speed: ${stats.speed}</p>
    <p>Damage: ${stats.damage}</p>
  `;
}

// --- INIT ---
loadPage("project");
