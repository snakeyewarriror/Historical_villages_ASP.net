<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="create_account.aspx.cs" Inherits="Final_project.create_account" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <main class="row">
        <div class="mt-5 d-flex justify-content-center align-items-center">
            <div class="form-container p-5 bg-light rounded">
                <h2 class="mb-4 text-center">Registo</h2>


                <%-- Name--%>
                <div class="mb-3">
                    <label for="TextBoxUsername" class="form-label">Nome de Utilizador</label>
                    <asp:TextBox runat="server" ID="TextBoxUsername" class="form-control" placeholder="Insira o seu nome" />

                    <asp:RequiredFieldValidator ErrorMessage="Obrigatório" ControlToValidate="TextBoxUsername" runat="server" Display="Dynamic" ForeColor="#CC0000" />

                    <!-- Label for error message -->
                    <asp:Label ID="LabelErrorMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                </div>


                <%-- Email --%>
                <div class="mb-3">
                    <label for="TextBoxEmail" class="form-label">Email</label>
                    <asp:TextBox ID="TextBoxEmail" runat="server" class="form-control" placeholder=" Insira o seu email" />

                    <asp:RequiredFieldValidator ErrorMessage="Obrigatório" ControlToValidate="TextBoxEmail" runat="server" Display="Dynamic" ForeColor="#CC0000" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Insira um formato de email valido"
                        ControlToValidate="TextBoxEmail" ForeColor="Red" Operator="DataTypeCheck" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        Display="Dynamic" />
                </div>


                <%-- Birthday --%>
                <div class="mb-3">
                    <label for="TextBoxDate" class="form-label">Data de nascimento</label>
                    <asp:TextBox ID="TextBoxDate" class="form-control" runat="server" TextMode="Date"></asp:TextBox>

                    <asp:RequiredFieldValidator ErrorMessage="Obrigatório" ControlToValidate="TextBoxDate" runat="server" Display="Dynamic" ForeColor="#CC0000" />
                    <asp:CompareValidator ID="validarDataNascimento" runat="server" ControlToValidate="TextBoxDate"/>
                </div>


                <%-- Password --%>
                <div class="mb-3">
                    <label for="password" class="form-label">Password</label>
                    <div class="input-group">
                            
                        <asp:TextBox ID="password" class="form-control" runat="server" TextMode="Password" placeholder="Insira a sua password" ClientIDMode="Static"></asp:TextBox>
                        <button type="button" class="btn btn-outline-secondary" id="togglePassword">
                            <i class="bi bi-eye" id="eyeIconPassword"></i>
                        </button>
                    </div>
                        
                    <small id="passwordHelpBlock" class="form-text text-muted">
                        A password tem de ter pelo menos 6 charaters.
                    </small>

                    <asp:RequiredFieldValidator ErrorMessage="Obrigatório" ControlToValidate="password" runat="server" Display="Dynamic" ForeColor="#CC0000" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="A password tem de ter pelo menos 6 charaters"
                        ControlToValidate="password" ForeColor="Red" Operator="DataTypeCheck" ValidationExpression="^\d{6,}$" />
                </div>


                <%-- Password Confirmation --%>
                <div class="mb-3">
                    <label for="passwordConfirmation" class="form-label">Confirmar Password</label>
                    <div class="input-group">

                        <asp:TextBox ID="passwordConfirmation" class="form-control" runat="server" TextMode="Password"
                            placeholder="Confirme a sua password" ClientIDMode="Static"></asp:TextBox>
                        <button type="button" class="btn btn-outline-secondary" id="toggleConfirmPassword">
                            <i class="bi bi-eye" id="eyeIconConfirmPassword"></i>
                        </button>
                    </div>

                    <asp:RequiredFieldValidator ErrorMessage="Obrigatório" ControlToValidate="passwordConfirmation" runat="server" Display="Dynamic" ForeColor="#CC0000" />
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="As passwords não são iguais" ForeColor="#CC0000"
                        ControlToCompare="password" ControlToValidate="passwordConfirmation" />
                </div>


                <%-- Terms and conditions Confirmation --%>
                <div class="mb-3 form-check">
                    <input type="checkbox" class="form-check-input" id="terms">
                    <label class="form-check-label" for="terms">Concordar com termos e condições</label>
                </div>


                <div class="mb-3 form-check">
                    <input type="checkbox" class="form-check-input" id="newsLetter" checked>
                    <label class="form-check-label" for="terms">Receber NewsLetter</label>
                </div>


                <%-- Register --%>
                <asp:Button ID="Button1" runat="server"  type="submit" class="btn" style="background-color: rgb(24,54,81); color: white;"
                    Text="Criar conta" Onclick="buttonCriarConta_Click"/>
            </div>
        </div>
    </main>
    <script src="Javascript/validation.js"></script>
</asp:Content>
