const API_BASE = "https://localhost:7158/api";
const ANIMAL_API = `${API_BASE}/animals`;
const OWNER_API = `${API_BASE}/owners`;

let deleteAnimalId = null;

document.addEventListener("DOMContentLoaded", () => {
    loadAnimals();
    loadOwners();
});

/* =======================
   LIST
======================= */
function loadAnimals() {
    fetch(ANIMAL_API)
        .then(res => res.json())
        .then(data => {
            const table = document.getElementById("animalTable");
            table.innerHTML = "";

            data.forEach(a => {
                table.innerHTML += `
                    <tr>
                        <td>${a.name}</td>
                        <td>${a.type}</td>
                        <td>${a.breed}</td>
                        <td>${a.age}</td>
                        <td>${a.ownerName ?? "-"}</td>
                        <td>
                            <button class="btn btn-sm btn-warning me-1"
                                onclick="editAnimal(${a.id})">✏️</button>
                            <button class="btn btn-sm btn-danger"
                                onclick="deleteAnimal(${a.id})">🗑️</button>
                        </td>
                    </tr>
                `;
            });
        });
}

/* =======================
   OWNER SELECT
======================= */
function loadOwners() {
    fetch(OWNER_API)
        .then(res => res.json())
        .then(data => {
            const createSel = document.getElementById("ownerId");
            const editSel = document.getElementById("editOwnerId");

            createSel.innerHTML = `<option value="">Sahip Seç</option>`;
            editSel.innerHTML = `<option value="">Sahip Seç</option>`;

            data.forEach(o => {
                const opt = `<option value="${o.id}">${o.fullName}</option>`;
                createSel.innerHTML += opt;
                editSel.innerHTML += opt;
            });
        });
}

/* =======================
   CREATE
======================= */
function addAnimal() {
    const animal = {
        name: document.getElementById("name").value.trim(),
        type: document.getElementById("type").value.trim(),
        breed: document.getElementById("breed").value.trim(),
        age: Number(document.getElementById("age").value),
        ownerId: Number(document.getElementById("ownerId").value)
    };

    if (!animal.name || !animal.type || !animal.breed || !animal.ownerId) {
        alert("Tüm alanları doldur");
        return;
    }

    fetch(ANIMAL_API, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(animal)
    }).then(() => {
        clearForm();
        loadAnimals();
    });
}

/* =======================
   EDIT
======================= */
function editAnimal(id) {
    fetch(`${ANIMAL_API}/${id}`)
        .then(res => res.json())
        .then(a => {
            document.getElementById("editAnimalId").value = a.id;
            document.getElementById("editName").value = a.name;
            document.getElementById("editType").value = a.type;
            document.getElementById("editBreed").value = a.breed;
            document.getElementById("editAge").value = a.age;
            document.getElementById("editOwnerId").value = a.ownerId;

            new bootstrap.Modal(
                document.getElementById("editAnimalModal")
            ).show();
        });
}

/* =======================
   UPDATE
======================= */
function updateAnimal() {
    const id = document.getElementById("editAnimalId").value;

    const animal = {
        name: document.getElementById("editName").value,
        type: document.getElementById("editType").value,
        breed: document.getElementById("editBreed").value,
        age: Number(document.getElementById("editAge").value),
        ownerId: Number(document.getElementById("editOwnerId").value)
    };

    fetch(`${API_BASE}/animals/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(animal)
    })
        .then(res => {
            if (!res.ok) throw new Error("PUT failed");
            return res.text();
        })
        .then(() => {
            bootstrap.Modal.getInstance(
                document.getElementById("editAnimalModal")
            ).hide();
            loadAnimals();
        })
        .catch(err => {
            console.error(err);
            alert("Güncelleme başarısız");
        });
}


/* =======================
   DELETE
======================= */
function deleteAnimal(id) {
    deleteAnimalId = id;
    new bootstrap.Modal(
        document.getElementById("deleteAnimalModal")
    ).show();
}

function confirmDeleteAnimal() {
    fetch(`${ANIMAL_API}/${deleteAnimalId}`, {
        method: "DELETE"
    }).then(() => {
        bootstrap.Modal.getInstance(
            document.getElementById("deleteAnimalModal")
        ).hide();
        loadAnimals();
    });
}

/* =======================
   UTIL
======================= */
function clearForm() {
    document.getElementById("name").value = "";
    document.getElementById("type").value = "";
    document.getElementById("breed").value = "";
    document.getElementById("age").value = "";
    document.getElementById("ownerId").value = "";
}

function filterTable() {
    const filter = document.getElementById("tableSearch").value.toLowerCase();
    document.querySelectorAll("tbody tr").forEach(row => {
        row.style.display = row.innerText.toLowerCase().includes(filter) ? "" : "none";
    });
}

