<div align="center">
  <h1>🏍️ PropSis - Sistema de Gestão de Motos</h1>
  <p>Solução robusta para cadastro de clientes e integração inteligente com Google Calendar.</p>

  ![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
  ![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
  ![Google Calendar API](https://img.shields.io/badge/Google_Calendar-4285F4?style=for-the-badge&logo=google-calendar&logoColor=white)
  ![Status](https://img.shields.io/badge/Status-Em_Desenvolvimento-orange?style=for-the-badge)
</div>

---

## 📝 Sobre o Projeto
O **PropSis** é um software desktop desenvolvido em C# focado na organização de oficinas de motocicletas. Ele permite gerir o fluxo de clientes e veículos, garantindo que nenhum serviço seja esquecido através da sincronização direta com a agenda do Google.

## ✨ Funcionalidades
- **Cadastro de Clientes:** Nome, contacto e histórico de visitas.
- **Base de Motos:** Registo por placa, modelo e proprietário.
- **Agendamento Inteligente:** Criação de compromissos na conta Google do utilizador para revisões e manutenções.

## 🛠️ Tecnologias Utilizadas
- **Linguagem:** C# (.NET)
- **Bibliotecas Google:** - `Google.Apis.Calendar.v3`
  - `Google.Apis.Auth`
- **Banco de Dados:** `MySQL`

## ⚙️ Configuração do Ambiente

### 🔑 Credenciais Google API
Para rodar este projeto, você precisará configurar o console do Google Cloud:
1. Ative a **Google Calendar API** no [Google Console](https://console.cloud.google.com/).
2. Crie credenciais do tipo **OAuth 2.0 Client ID** para "Desktop App".
3. Baixe o JSON, renomeie para `credentials.json`.
4. **Importante:** No Visual Studio, clique com o botão direito no `credentials.json` -> Propriedades -> **Copiar para Diretório de Saída** -> "Copiar se for mais novo".

### 🚀 Como Executar
1. Clone este repositório:
   ```bash
   git clone [https://github.com/Heitorfsv/PropSis.git](https://github.com/Heitorfsv/PropSis.git)
