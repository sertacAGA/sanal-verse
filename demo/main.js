// ---------------- STATE ----------------
let gameState = {
  step: "career",
  career: null,
  vehicle: null,
  parts: []
};

// ---------------- DATA ----------------
const vehicles = [
  { id: "drone_fast", name: "Hızlı Drone", desc: "Çok hızlı, düşük hasar" },
  { id: "drone_power", name: "Saldırı Drone", desc: "Yüksek hasar" }
];

const parts = [
  { id: "engine", name:"Motor", desc:"Hız artırır", speed: 20 },
  { id: "gun", name:"Silah", desc:"Hasar artırır", damage: 25 },
  { id: "armor", name:"Zırh", desc:"Dayanıklılık", damage: 5 }
];

// ---------------- STEP UI ----------------
function updateSteps() {
  document.querySelectorAll(".step").forEach(s => s.classList.remove("active"));

  const el = document.getElementById("step-" + gameState.step);
  if (el) el.classList.add("active");
}

// ---------------- NAV ----------------
function loadStep(step) {
  gameState.step = step;
  updateSteps();

  if (step === "career") renderCareer();
  if (step === "vehicle") renderVehicle();
  if (step === "build") renderBuild();
  if (step === "test") renderTest();
}

// ---------------- STEP 1: CAREER ----------------
function renderCareer() {
  document.getElementById("content").innerHTML = `
    <h2>Kariyer Seç</h2>

    <div class="card">
      <div class="part" onclick="selectCareer('engineer')">
        <h3>🔧 Mühendis</h3>
        <p>Drone üret ve geliştir</p>
      </div>
    </div>
  `;
}

function selectCareer(career) {
  gameState.career = career;
  loadStep("vehicle");
}

// ---------------- STEP 2: VEHICLE ----------------
function renderVehicle() {
  document.getElementById("content").innerHTML = `
    <h2>Drone Seç</h2>

    <div class="parts">
      ${vehicles.map(v => `
        <div class="part" onclick="selectVehicle('${v.id}')">
          <h3>${v.name}</h3>
          <p>${v.desc}</p>
        </div>
      `).join("")}
    </div>

    <button onclick="loadStep('career')">← Geri</button>
  `;
}

function selectVehicle(id) {
  gameState.vehicle = vehicles.find(v => v.id === id);
  loadStep("build");
}

// ---------------- STEP 3: BUILD ----------------
function renderBuild() {
  document.getElementById("content").innerHTML = `
    <h2>Drone Oluştur</h2>

    <div class="card">
      <h3>Parça Ekle</h3>
      <div class="parts">
        ${parts.map(p => `
          <div class="part" onclick="addPart('${p.id}')">
            <h4>${p.name}</h4>
            <p>${p.desc}</p>
          </div>
        `).join("")}
      </div>
    </div>

    <div class="card">
      <h3>Eklenen Parçalar</h3>
      <div id="partList"></div>
    </div>

    <div class="card">
      <h3>Performans</h3>
      <div id="stats"></div>
    </div>

    <button onclick="loadStep('vehicle')">← Geri</button>
    <button onclick="loadStep('test')">🚀 TEST ET</button>
  `;

  updateBuild();
}

function addPart(id) {
  const part = parts.find(p => p.id === id);
  gameState.parts.push(part);
  renderBuild();
}

// ---------------- BUILD UPDATE ----------------
function updateBuild() {
  // Parça listesi
  document.getElementById("partList").innerHTML =
    gameState.parts.length
      ? gameState.parts.map(p => `<p>🔧 ${p.name}</p>`).join("")
      : "<p>Henüz parça eklenmedi</p>";

  // Stat hesapla
  const stats = calculateStats();

  document.getElementById("stats").innerHTML = `
    <p>Hız</p>
    <div class="bar">
      <div class="fill" style="width:${stats.speed}%"></div>
    </div>

    <p>Hasar</p>
    <div class="bar">
      <div class="fill" style="width:${stats.damage}%"></div>
    </div>
  `;
}

// ---------------- CALC ----------------
function calculateStats() {
  let stats = { speed: 0, damage: 0 };

  gameState.parts.forEach(p => {
    stats.speed += p.speed || 0;
    stats.damage += p.damage || 0;
  });

  return stats;
}

// ---------------- STEP 4: TEST ----------------
function renderTest() {
  const stats = calculateStats();

  // Basit skor formülü
  const score = (stats.speed * 2) + (stats.damage * 3);

  document.getElementById("content").innerHTML = `
    <h2>Test Sonucu</h2>

    <div class="card">
      <p>🚀 Hız: ${stats.speed}</p>
      <p>💥 Hasar: ${stats.damage}</p>
      <h3>Skor: ${score}</h3>
    </div>

    <button onclick="loadStep('build')">← Geri</button>
    <button onclick="restart()">🔄 Yeniden Başla</button>
  `;
}

// ---------------- RESET ----------------
function restart() {
  gameState = {
    step: "career",
    career: null,
    vehicle: null,
    parts: []
  };

  loadStep("career");
}

// ---------------- INIT ----------------
loadStep("career");
