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

// ---------------- MINI GAME ----------------

let canvas, ctx;
let player, bullets, targets;
let score = 0;
let timeLeft = 30;
let gameInterval;

// TEST EKRANINI DEĞİŞTİR
function renderTest() {
  document.getElementById("content").innerHTML = `
    <h2>Test Alanı</h2>
    <p>SPACE ile ateş et</p>
    <p>Süre: <span id="time">30</span></p>
    <p>Skor: <span id="score">0</span></p>
    <button onclick="loadStep('build')">← Geri</button>
  `;

  startGame();
}

// OYUN BAŞLAT
function startGame() {
  canvas = document.getElementById("gameCanvas");
  ctx = canvas.getContext("2d");
  canvas.style.display = "block";

  const stats = calculateStats();

  player = {
    x: 50,
    y: 200,
    speed: 2 + stats.speed * 0.1
  };

  bullets = [];
  targets = [];
  score = 0;
  timeLeft = 30;

  spawnTargets();

  document.addEventListener("keydown", shoot);

  gameInterval = setInterval(updateGame, 1000/60);
  setInterval(updateTimer, 1000);
}

// HEDEF OLUŞTUR
function spawnTargets() {
  setInterval(() => {
    targets.push({
      x: 600,
      y: Math.random() * 350,
      speed: 2
    });
  }, 1000);
}

// ATEŞ
function shoot(e) {
  if (e.code === "Space") {
    bullets.push({
      x: player.x,
      y: player.y,
      speed: 5
    });
  }
}

// TIMER
function updateTimer() {
  timeLeft--;
  document.getElementById("time").innerText = timeLeft;

  if (timeLeft <= 0) {
    endGame();
  }
}

// OYUN LOOP
function updateGame() {
  ctx.clearRect(0,0,600,400);

  // PLAYER
  ctx.fillStyle = "cyan";
  ctx.fillRect(player.x, player.y, 20, 20);

  // BULLETS
  bullets.forEach(b => {
    b.x += b.speed;
    ctx.fillStyle = "yellow";
    ctx.fillRect(b.x, b.y, 5, 5);
  });

  // TARGETS
  targets.forEach(t => {
    t.x -= t.speed;
    ctx.fillStyle = "red";
    ctx.fillRect(t.x, t.y, 20, 20);
  });

  // COLLISION
  bullets.forEach((b, bi) => {
    targets.forEach((t, ti) => {
      if (
        b.x < t.x + 20 &&
        b.x + 5 > t.x &&
        b.y < t.y + 20 &&
        b.y + 5 > t.y
      ) {
        bullets.splice(bi,1);
        targets.splice(ti,1);
        score += 10;
        document.getElementById("score").innerText = score;
      }
    });
  });
}

// OYUN BİTİŞ
function endGame() {
  clearInterval(gameInterval);

  document.getElementById("content").innerHTML = `
    <h2>Oyun Bitti</h2>
    <h3>Skor: ${score}</h3>
    <button onclick="restart()">🔄 Yeniden Başla</button>
  `;

  canvas.style.display = "none";
}
