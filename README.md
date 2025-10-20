# 💖 DatingApp

Welcome to **DatingApp**! This is a full-stack web application built with ASP.NET Core (.NET) for the backend and Angular for the frontend. It’s designed to help users connect, chat, and find matches in a modern, secure environment.

---

## 🚀 Features
- 👤 User registration & authentication
- 🖼️ Profile management & photo uploads
- 💌 Messaging between users (real-time via SignalR)
- 👍 Like & match system
- 🛡️ Admin controls & role management
- 📊 Activity logging
- 🐞 Error handling & custom middleware
- 📦 RESTful API with pagination & filtering
- 🌐 Responsive Angular client

---

## 🛠️ Technologies Used
- **Backend:** ASP.NET Core (.NET 6+), Entity Framework Core, PostgreSQL
- **Frontend:** Angular, RxJS, TypeScript, SCSS
- **Real-time:** SignalR
- **Cloud:** Cloudinary (for photo storage)
- **Other:** AutoMapper, JWT Authentication

---

## 📦 Project Structure
```
DatingApp.sln
API/         # .NET backend
client/      # Angular frontend
```
- `API/` contains all backend logic, controllers, services, repositories, and models.
- `client/` contains the Angular app, components, services, and assets.

---

## 🏁 Getting Started

### Prerequisites
- [.NET 6+ SDK](https://dotnet.microsoft.com/download)
- [Node.js & npm](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)
- PostgreSQL database

### Backend Setup
1. **Clone the repo:**
   ```powershell
   git clone https://github.com/Vlad-Gheorghita/DatingApp.git
   cd DatingApp/API
   ```
2. **Configure database:**
   - Update `appsettings.Development.json` with your PostgreSQL connection string.
3. **Apply migrations:**
   ```powershell
   dotnet ef database update
   ```
4. **Run the API:**
   ```powershell
   dotnet watch run
   ```

### Frontend Setup
1. **Install dependencies:**
   ```powershell
   cd ../client
   npm install
   ```
2. **Run the Angular app:**
   ```powershell
   ng serve
   ```
3. **Open in browser:**
   - Visit [http://localhost:4200](http://localhost:4200)

---

## ⚙️ Configuration
- **Cloudinary:** Add your Cloudinary credentials to `API/appsettings.Development.json`.
- **JWT Secret:** Set a strong secret in your appsettings for token generation.
- **CORS:** Ensure CORS is enabled for local development.

---

## 🧪 Testing
- Backend: Use `dotnet test` for unit/integration tests (add your tests in the API project).
- Frontend: Use `ng test` for Angular unit tests.

---

## 🤝 Contributing
1. Fork the repo
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some feature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 🛟 Support & Contact
- For issues, use the [GitHub Issues](https://github.com/Vlad-Gheorghita/DatingApp/issues) page.
- For questions, contact the maintainer: Vlad Gheorghita

---

## 📄 License
This project is licensed under the MIT License.

---

## 🌟 Acknowledgements
- Thanks to all contributors and the open-source community!

---

Happy coding! 🚀
