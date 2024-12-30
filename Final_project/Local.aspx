<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Local.aspx.cs" Inherits="Final_project.Local" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <main class="row mt-5">

        
            <asp:Label ID="nameLocal" runat="server" Text=""></asp:Label>
            <br />
            <br />
            <asp:Label ID="localization" runat="server" Text=""></asp:Label>

        
        <!-- Carousel -->
        <div id="bs-carousel" class="fade-carousel carousel slide" data-bs-ride="carousel"  data-bs-interval="3000">
            
            <!-- Carousel items -->
            <div class="carousel-inner">
                <asp:Repeater ID="listLocais" runat="server">
                    <ItemTemplate>

                    <div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>" id="carousel_fade">
                        <asp:Image ID="Image1" CssClass="d-block w-100" runat="server" ImageUrl='<%# Eval("Ficheiro") %>' Alt='<%# Eval("Legenda") %>' />
                    </div>

                    </ItemTemplate>
                </asp:Repeater>
                
            </div>
        </div>
            
        <div class="container">

            <!-- Section Information text -->
            <section class="row" style="margin-top: 180px;" id="container_text"> 
            <div class="col-5 container_text_inside">
                <h3> 
                    Almeida terá tido origem na migração dos habitantes de um castro lusitano, localizado a Norte do lugar do Enxido da Sarça, ocupado em 61 a.C. pelos Romanos, e depois pelos povos bárbaros. Dada a sua situação em planalto, os Árabes chamaram-na Al-Mêda (a Mesa), Talmeyda ou Almeydan, tendo construído um pequeno Castelo (séc. VIII- IX).
                </h3>
                <br/><br/>
                <img class="d-block w-100" src="Imagens/Aldeias/almeida/almeida5.jpg" alt="Imagen almeida">
            </div>

            <div class="col-2"></div> <!-- Emopty collumn for extra space between -->

            <div class="col-5 container_text_inside">
                <p>
                <asp:Label ID="description" runat="server" Text=""></asp:Label>
                </p>
            </div>
            </section>  

      
            <hr class="border-black" style="margin-top: 150px; margin-bottom: 30px;"> <!-- Divider -->

            <!-- Section Informações Uteis -->
            <section class="row"> 
            <div class="col-6">
                <h4 class="text-center my-5">Como chegar</h4>
                <iframe class="w-100" src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d48375.09771707126!2d-6.945430644390436!3d40.73026336857825!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0xd3c42c12d537ed7%3A0x1a0c5b4f6fcfb331!2sAlmeida!5e0!3m2!1sen!2spt!4v1731265159193!5m2!1sen!2spt" width="700" height="470" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
            </div>

            <!-- Weather -->
            <div class="col-6">
                <h4 class="text-center my-5">Como vai estar o tempo</h4>
                <table class="table w-100" id="previsao" >
                    <tbody id="weatherTableBody">
                    </tbody>
                </table>
            </div>
            </section>

        </div>

        <div id="container_text" class="container d-flex flex-wrap justify-content-between" style="margin-top: 300px;">

            <div class="col-5 container_text_inside">
                    
                <asp:Label ID="description2" runat="server" Text=""></asp:Label>
            </div>
        </div>

    </main>

</asp:Content>
