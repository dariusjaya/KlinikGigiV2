# 🦷 KlinikGigiV2

Sistem manajemen klinik gigi berbasis web — dibangun untuk menggantikan pencatatan manual di kartu kertas menjadi sistem digital yang cepat dicari, aman, dan mudah dikelola.

Dibuat untuk kebutuhan nyata: mendigitalisasi rekam medis pasien di praktik dokter gigi keluarga yang sebelumnya masih menggunakan kartu fisik.

---

## ✨ Fitur

- **Autentikasi** — Login dengan JWT untuk dokter & perawat
- **Manajemen Pasien** — Tambah, cari, lihat, dan perbarui data pasien dengan pencarian real-time & pagination
- **Rekam Medis** — Catat riwayat kunjungan per pasien (diagnosa, terapi, catatan) — create, edit, hapus, dengan riwayat lengkap per pasien
- **Manajemen Pengguna** — Kelola akun perawat/dokter yang dapat mengakses sistem

---

## 🏗️ Arsitektur

Backend dibangun mengikuti prinsip **Clean Architecture** ala [Ardalis](https://github.com/ardalis/CleanArchitecture), memisahkan concern secara jelas antar layer:

```
src/
├── KlinikGigiV2.Core            → Domain entities, interfaces, business rules
├── KlinikGigiV2.UseCases        → Application logic (CQRS: Commands & Queries)
├── KlinikGigiV2.Infrastructure  → Data access (EF Core), repositories
├── KlinikGigiV2.SharedKernel    → Kontrak & tipe yang dipakai lintas layer
└── KlinikGigiV2.Web             → API endpoints (FastEndpoints)
```

Frontend berkomunikasi dengan backend melalui **client TypeScript yang di-generate otomatis** dari OpenAPI spec — sehingga tidak ada penulisan tipe atau endpoint secara manual, dan selalu sinkron dengan kontrak API terbaru.

---

## 🛠️ Tech Stack

**Backend**
- ASP.NET Core 10 + [FastEndpoints](https://fast-endpoints.com/)
- Entity Framework Core + PostgreSQL
- [Ardalis.Specification](https://github.com/ardalis/Specification) untuk query yang testable & reusable
- [Mediator](https://github.com/martinothamar/Mediator) (source-generated) untuk CQRS
- [ErrorOr](https://github.com/amantinband/error-or) untuk error handling tanpa exception
- JWT Bearer Authentication

**Frontend**
- Nuxt 4 + Vue 3 (Composition API)
- [Nuxt UI](https://ui.nuxt.com/) — komponen siap pakai berbasis Tailwind CSS
- Pinia — state management
- [@hey-api/openapi-ts](https://heyapi.dev/) — auto-generate TypeScript client dari OpenAPI

**Infrastructure**
- Docker Compose (PostgreSQL)

---

## 🚀 Menjalankan Secara Lokal

### Prasyarat
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) + [pnpm](https://pnpm.io/)
- [Docker](https://www.docker.com/)

### 1. Jalankan Database

```bash
docker-compose up -d
```

### 2. Backend

```bash
cd backend

# Terapkan migrasi database
dotnet ef database update \
  --project src/KlinikGigiV2.Infrastructure \
  --startup-project src/KlinikGigiV2.Web

# Jalankan API
dotnet run --project src/KlinikGigiV2.Web
```

API berjalan di `https://localhost:57679`, dokumentasi OpenAPI tersedia di `/openapi/v1.json`., swagger UI di `/swagger`.

### 3. Frontend

```bash
cd frontend
pnpm install

# Generate TypeScript client dari OpenAPI spec backend
pnpm generate:openapi

# Jalankan dev server
pnpm dev
```

Aplikasi dapat diakses di `http://localhost:3000`.

---

## 📁 Struktur Proyek

```
KlinikGigiV2/
├── backend/
│   ├── src/
│   │   ├── KlinikGigiV2.Core
│   │   ├── KlinikGigiV2.UseCases
│   │   ├── KlinikGigiV2.Infrastructure
│   │   ├── KlinikGigiV2.SharedKernel
│   │   └── KlinikGigiV2.Web
│   └── tests/
├── frontend/
│   ├── app/
│   │   ├── client/          # OpenAPI generated client (jangan diedit manual)
│   │   ├── components/      # Komponen Vue reusable, dikelompokkan per domain
│   │   ├── layouts/
│   │   ├── middleware/
│   │   ├── pages/
│   │   └── stores/
│   └── openapi-ts.config.ts
└── docker-compose.yml
```

---

## 🔄 Alur Pengembangan API

Setiap kali ada perubahan pada backend (endpoint baru, ubah schema), frontend cukup regenerate client-nya tanpa menulis ulang tipe atau fungsi fetch secara manual:

```bash
# 1. Jalankan backend
dotnet run --project backend/src/KlinikGigiV2.Web

# 2. Generate ulang client di frontend
cd frontend && pnpm generate:openapi
```

---

## 📝 Catatan

Proyek ini dikembangkan sebagai studi kasus penerapan Clean Architecture dan CQRS pada backend .NET, sekaligus alur kerja modern *API-first development* dengan auto-generated client di frontend Nuxt/Vue.