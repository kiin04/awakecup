# ☕ AwakeCup Coffee Store

### 📖 Project Overview
AwakeCup is a high-performance, scalable coffee e-commerce platform where a premium shopping experience meets modern cloud-native engineering. The core focus extends beyond building a feature-rich store to establishing a sophisticated CI/CD infrastructure on AWS, ensuring that every software update is delivered as fresh, rapid, and reliable as a morning brew.

### 🌟 Key Achievements
- **Performance Optimization:** Achieved in deployment time by transitioning to an automated Jenkins Pipeline-as-Code and optimizing Docker multi-stage builds.
- **Full Automation:** Engineered a comprehensive **Jenkins CI/CD pipeline** that automates the entire lifecycle: Source Control → Image Building → Docker Hub Registry → Automated Production Deployment to AWS EC2.
- **Cloud Database Integration:** Migrated from local containers to **Amazon RDS (MySQL)**, improving data integrity, security, and ensuring zero-data-loss during deployments.

### 🛠 Tech Stack
<div align="center">
  <code><img width="50" src="https://raw.githubusercontent.com/marwin1991/profile-technology-icons/refs/heads/main/icons/aws.png" alt="AWS" title="AWS"/></code>
  <code><img width="50" src="https://raw.githubusercontent.com/marwin1991/profile-technology-icons/refs/heads/main/icons/_net_core.png" alt=".NET Core" title=".NET Core"/></code>
  <code><img width="50" src="https://raw.githubusercontent.com/marwin1991/profile-technology-icons/refs/heads/main/icons/react.png" alt="React" title="React"/></code>
  <code><img width="50" src="https://raw.githubusercontent.com/marwin1991/profile-technology-icons/refs/heads/main/icons/mysql.png" alt="MySQL" title="MySQL"/></code>
  <code><img width="50" src="https://raw.githubusercontent.com/marwin1991/profile-technology-icons/refs/heads/main/icons/jenkins.png" alt="Jenkins" title="Jenkins"/></code>
  <code><img width="50" src="https://raw.githubusercontent.com/marwin1991/profile-technology-icons/refs/heads/main/icons/docker.png" alt="Docker" title="Docker"/></code>
  <code><img width="50" src="https://raw.githubusercontent.com/marwin1991/profile-technology-icons/refs/heads/main/icons/linux.png" alt="Linux" title="Linux"/></code>
  <code><img width="50" src="https://raw.githubusercontent.com/marwin1991/profile-technology-icons/refs/heads/main/icons/nginx.png" alt="Nginx" title="Nginx"/></code>
</div>

### 🏗 DevOps & Cloud Architecture
The system is architected on **AWS (Amazon Web Services)** with a focus on security and scalability.

* **Compute:** **AWS EC2 (Ubuntu)** hosts the containerized applications orchestrated by **Docker Compose**.
* **Database:** Dedicated **Amazon RDS** instance for persistent storage, isolated from the application layer via **VPC Security Groups**.
* **Jenkins Pipeline:** * **Stage - Build:** Multi-stage Docker builds for Backend (.NET 8) and Frontend (React/Vite).
    * **Stage - Push:** Versioned images are pushed to **Docker Hub**.
    * **Stage - Deploy:** Environment variable injection and RDS schema initialization.


## 🖼️ Project Gallery

<details>
<summary><b>📱 User Interface </b> (Click to expand)</summary>
<br>
<p align="center">
  <img src="https://github.com/user-attachments/assets/98469482-98c4-4550-a816-fe5fc7234d32" width="80%" alt="User Interface" />
  <br><i>User Interface - Awakecup</i>
</p>
</details>


<details>
<summary><b>🚀 CI/CD Pipeline </b> (Click to expand)</summary>
<br>
<p align="center">
  <img src="https://github.com/user-attachments/assets/aff24d94-9e29-4ac8-a772-86b9d1c078c6" width="80%" alt="Jenkins Pipeline" />
  <br><i>Automated Jenkins Pipeline with Build & Push Stages</i>
</p>
</details>




<details>
<summary><b>📦 Container Registry (Docker Hub)</b> (Click to expand)</summary>
<br>
<p align="center">
  <img src="https://github.com/user-attachments/assets/a4d3d8eb-6f36-4ec9-b53d-375728f8f361" width="80%" alt="Docker Hub" />
  <br><i>Optimized Docker Images in Repository</i>
</p>
</details>
