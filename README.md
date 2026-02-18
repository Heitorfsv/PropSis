<div align="center">
  <h1>🏍️ PropSis - Sistema de Gestão de Motos</h1>
  <p>Solução robusta para gestão de oficinas, ordens de serviço e integração com Google Calendar.</p>

  ![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
  ![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
  
  ![MySQL](https://img.shields.io/badge/MySQL-4479A1?style=for-the-badge&logo=mysql&logoColor=white)
  ![SQLite](https://img.shields.io/badge/SQLite-003B57?style=for-the-badge&logo=sqlite&logoColor=white)
  
  ![Google Calendar](https://img.shields.io/badge/Google_Calendar-4285F4?style=for-the-badge&logo=google-calendar&logoColor=white)
  ![PDF](https://img.shields.io/badge/QuestPDF-red?style=for-the-badge&logo=adobe-acrobat-reader&logoColor=white)
  
  ![Status](https://img.shields.io/badge/Status-Em_Desenvolvimento-orange?style=for-the-badge)
</div>

---

## 📝 Sobre o Projeto
O **PropSis** é um software desktop desenvolvido em C# focado na organização de oficinas de motocicletas. Ele permite gerir o fluxo de clientes e veículos, garantindo que nenhum serviço seja esquecido, pela interface intuitiva e limpa.

## ✨ Funcionalidades
- **Cadastro de Clientes:** Nome, contacto e histórico de visitas.
- **Base de Motos:** Registo por placa, modelo e proprietário.
- **Ordens de serviço:** Registro de informações completas, peças, serviços, observações, trocas futuras. Exibidos de forma intuitiva e facil. É possível gerar PDFs das OS.
- **Agendamento Inteligente:** Criação de compromissos na conta Google do utilizador para revisões e manutenções.
- **Sincronização remota:** Os dados são sincronizados para o banco de dados da rede local quando há internet dsponível, senão, são gravados localmente e quando a conexão voltar os dados são sincronizados

## 🛠️ Tecnologias Utilizadas
- **Linguagem:** C# (.NET)
- **Bibliotecas Google:** - `Google.Apis.Calendar.v3`
  - `Google.Apis.Auth`
- **Banco de Dados:** `MySQL` - `SQLite`
- **Geredor de PDF:** `QuestPFD`
- **Encriptador de senha:** `BCrypt.Net`
- **Busca CEP:** `viacep.com.br`

## ⚙️ Configuração do Ambiente

### 🔑 Credenciais Google API
Para rodar este projeto, você precisará configurar o console do Google Cloud:
1. Ative a **Google Calendar API** no [Google Console](https://console.cloud.google.com/).
2. Crie credenciais do tipo **OAuth 2.0 Client ID** para "Desktop App".
3. Baixe o JSON, renomeie para `credentials.json`.

### 🚀 Como Executar
1. Clone este repositório:
   ```bash
   git clone [https://github.com/Heitorfsv/PropSis.git](https://github.com/Heitorfsv/PropSis.git)
