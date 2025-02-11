<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Final_project.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>

        .custom-login {
            margin: auto; /* Center the login box */
        }

        td[colspan="2"][align="center"] {
            font-size: 22px;
            font-weight: bold;
        }

        .custom-login label {
            font-size: 16px;
            color: #333; /* Darker text for visibility */
            margin-bottom: 5px;
            margin-left: 15px;
            margin-right: 15px;
        }

        label[for="ContentPlaceHolder1_loginUser_UserName"] {
            margin-top: 30px;
        }
        
        label[for="ContentPlaceHolder1_loginUser_Password"]{
            margin-top: 5px;
        }

        .custom-login input[type="text"],
        .custom-login input[type="password"] {
            width: auto; /* Adjust width as needed */
            border-radius: 8px; /* Rounded corners */
            border: 1px solid #ccc;
            margin-right: 15px;
            margin-top: 30px;
        }

        
        .custom-login input[type="password"] {
            margin-top: 5px;
        }

        .custom-login input[type="submit"] {
            color: white;
            cursor: pointer;
            border: none;
            border-radius: 5px;
            background-color: rgb(53, 106, 152);
            margin-right: 260px;
        }

        
        /* Change button on hover */
        .custom-login input[type="submit"]:hover {
            background-color: rgb(60, 130, 200);
        }

        /* Style the "Create Account" link */
        .custom-login a {
            display: block;
            text-align: start;
            margin-left: 15px;
            margin-top: 10px;
            color: #007bff;
            text-decoration: none;
        }

        .custom-login a:hover {
            text-decoration: underline;
        }


        label[for="ContentPlaceHolder1_loginUser_RememberMe"] {
            margin-right: 70px;
            margin-top: 15px;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Login ID="loginUser" runat="server"
        CssClass="custom-login form-container bg-light rounded text-center mt-5"
        CreateUserText="Criar conta"
        CreateUserUrl="~/create_account.aspx"
        DestinationPageUrl="~/Main_page.aspx" OnLoggedIn="loginUtilizador_LoggedIn"></asp:Login>
</asp:Content>
