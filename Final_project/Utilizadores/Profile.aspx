<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Final_project.Utilizadores.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .disabled_btn {
            background-color: #ccc;
            color: #666;
            cursor: not-allowed;
            border: none;
            pointer-events: none; /* Prevent clicking */
        }

        .enabled_btn {
            background-color: rgb(45, 98, 147);
            color: white;
            cursor: pointer;
            border: none;    
            pointer-events: auto; /* Prevent clicking */
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    
    <main class="row">
        <div class="mt-5 d-flex justify-content-center align-items-center">
            <div class="form-container p-5 bg-light rounded text-center">
                <h2 class="mb-4">Perfil</h2>


                <%-- Name--%>
                <div class="mb-3">
                    <label for="TextBoxUsername" class="form-label">Nome de Utilizador</label>
                    <asp:TextBox runat="server" ID="TextBoxUsername" class="form-control" />

                    <asp:RequiredFieldValidator ErrorMessage="Obrigatório" ControlToValidate="TextBoxUsername" runat="server" Display="Dynamic" ForeColor="#CC0000"
                        SetFocusOnError="true" />

                    <!-- Label for error message -->
                    <asp:Label ID="LabelErrorMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                </div>


                <%-- Email --%>
                <div class="mb-3">
                    <label for="TextBoxEmail" class="form-label">Email</label>
                    <asp:TextBox ID="TextBoxEmail" runat="server" class="form-control" />

                    <asp:RequiredFieldValidator ErrorMessage="Obrigatório" ControlToValidate="TextBoxEmail" runat="server" Display="Dynamic" ForeColor="#CC0000" SetFocusOnError="true" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Insira um formato de email valido"
                        ControlToValidate="TextBoxEmail" ForeColor="Red" Operator="DataTypeCheck" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        Display="Dynamic" />
                </div>


                <%-- Alter button --%>
                <asp:Button ID="save_changes_button" runat="server"  type="submit" class="btn disabled_btn"
                    Text="Guardar mudanças" Onclick="ChangeProfileButton_Click"/>
            </div>
        </div>
    </main>

    <script>
        window.onload = function () {
            // Variables for the name field
            var name_field = document.getElementById('<%= TextBoxUsername.ClientID %>');
            var initial_value_name = name_field.value;

            // Variables for the email field
            var email_field = document.getElementById('<%= TextBoxEmail.ClientID %>');
            var initial_value_email = email_field.value;

            // Variables for save changes button
            var save_button = document.getElementById('<%= save_changes_button.ClientID %>');


            // Function to toggle the button state depding if the values are different then on page load
            function toggleButtonState() {
                if (name_field.value !== initial_value_name || email_field.value !== initial_value_email) {
                    save_button.classList.remove("disabled_btn");
                    save_button.classList.add("enabled_btn");
                    save_button.disabled = false; // Ensure button is clickable
                } else {
                    save_button.classList.remove("enabled_btn");
                    save_button.classList.add("disabled_btn");
                    save_button.disabled = true; // Ensure button is clickable
                }
            }

            // Attach event listeners to both input fields
            name_field.onkeyup = toggleButtonState;
            email_field.onkeyup = toggleButtonState;
        };

    </script>
</asp:Content>