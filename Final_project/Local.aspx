<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Local.aspx.cs" Inherits="Final_project.Local" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.9.4/dist/leaflet.css"
     integrity="sha256-p4NxAoJBhIIN+hmNHrzRCf9tD/miZyoHS5obTRR9BMY="
     crossorigin=""/>

    <script src="https://unpkg.com/leaflet@1.9.4/dist/leaflet.js"
     integrity="sha256-20nQCchB9co0qIjJZRGuk2/Z9VM+kNiyxNV1lvTlZBo="
     crossorigin=""></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <!-- Main -->
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
        </main> 
    </div>

    <div class="container">

      <!-- Section Information text -->
      <section class="row" style="margin-top: 180px;" id="container_text"> 
        <div class="col-5 container_text_inside">
            <h3> 
                Quem já visitou
            </h3>

            <br/><br/>

            <!-- Comments list -->
            <asp:DataList runat="server" ID="listComentarios" CssClass="mt-3">
                <ItemTemplate>
                    <table class="table table-borderless mb-2">

                        <tr style="height: auto; padding-bottom: 20px;">
                            <td>
                                <asp:Label ID="comentario" runat="server"
                                Text='<%# Eval("comentario") %>' />

                                <br /><br />

                                <asp:Label Text='<%# "Classificação - " + Eval("classificacao")?.ToString() %>' runat="server" />
                            </td>
                        </tr>

                        <tr style="vertical-align: middle; height: 40px;">
                            <td>
                                <asp:Label ID="utilizador" runat="server" Text='<%# Eval("utilizador") %>' />
                            </td>
                        </tr>

                    </table>
                </ItemTemplate>
            </asp:DataList>

            <!-- Comments controls -->
            <div class="mt-1 container text-center" style="margin-bottom:100px;">
                <asp:LinkButton Text="Primeira" runat="server" ID="linkFirst" CssClass="text-decoration-none fs-5" OnClick="linkFirst_click" />

                <asp:LinkButton Text="Anterior" runat="server" ID="linkPrevious" CssClass="ms-3 text-decoration-none fs-5" OnClick="linkPrevious_click" />

                <asp:LinkButton Text="Seguinte" runat="server" ID="linkNext" CssClass="ms-3 text-decoration-none fs-5" OnClick="linkNext_click" />

                <asp:LinkButton Text="Última" runat="server" ID="linkLast" CssClass="ms-3 text-decoration-none fs-5" OnClick="linkLast_click" />
            </div>
        </div>

        <div class="col-2"></div> <!-- Empty collumn for extra space between -->

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

            <div class="col-12">
                <div id="map" style="height: 340px; width: 700px" class="w-100"></div>
            </div>
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

      <hr class="border-black" style="margin-top: 150px; margin-bottom: 50px;"> <!-- Divider -->

      <!-- Section Video --> 
      <section class="row">  
        <h3 class="text-center col-12 my-5">Videos</h3>  
        <div class="embed-responsive embed-responsive-16by9 col-5">
          <iframe class="w-100" height="415" src="https://www.youtube.com/embed/TMqO_767kQ8?si=mnrI65nXN05WeQOo" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
        </div>

        <div class="col-2"></div> <!-- Emopty collumn for extra space between -->

        <div class="embed-responsive embed-responsive-16by9 col-5">
          <iframe class="w-100" height="415" src="https://www.youtube.com/embed/RsGTn4lrSuA?si=TW8fzRbSg-2fCm4U" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
        </div>
      </section>

      <hr class="border-black" style="margin-top: 150px; margin-bottom: 0px;"> <!-- Divider -->

    </div>

    <div class="container-fluid">

  

      <!-- Information cards -->
      <section class="row" style="margin-top: 30px;">
        <h3 class="text-center col-12 my-5">O que ver</h3>

        <div class="col-4 d-flex justify-content-center">
          <div class="card" style="width: 18rem">
            <img class="card-img-top" src="Imagens/Aldeias/almeida/DSC6616-500x400.jpg" alt="Serra da Estrela">
            <div class="card-body">
              <h5 class="card-title">Viagens de aventura e natureza</h5>
              <p class="card-text">As nossas viagens são muito mais do que ir de A a B, e distinguimo-nos por termos um propósito maior: O de promover o conhecimento e a preservação do património histórico, cultural e ambiental de Portugal.</p>
            </div>
          </div>
        </div>
      
        <div class="col-4 d-flex justify-content-center">
          <div class="card" style="width: 18rem">
            <img class="card-img-top" src="Imagens/Aldeias/almeida/SSJ3-500x400.png" alt="Solar São João">
            <div class="card-body">
              <h5 class="card-title">São João Casa Memória Almeida</h5>
              <p class="card-text">O Solar São João da Praça encontra-se edificado no centro histórico da vila e fortaleza de Almeida. Foi mandado construir em 1726 pelo Coronel de Infantaria José Delgado Freire, casado com D. Maria de Azevedo, das famílias mais importantes de Riba Côa.</p>
            </div>
          </div>
        </div>    

        <div class="col-4 d-flex justify-content-center">
          <div class="card" style="width: 18rem">
            <img class="card-img-top" src="Imagens/Aldeias/almeida/850_1869-500x400.jpg" alt="Portugal A2Z Walking">
            <div class="card-body">
              <h5 class="card-title">Portugal A2Z Walking & Biking</h5>
              <p class="card-text">Movida pela paixão de viajar, a Portugal A2Z Walking & Biking foi criada para lhe oferecer experiências autênticas e memórias inesquecíveis nos locais mais exclusivos de Portugal, levando-o aos locais mais históricos e às paisagens mais selvagens.</p>
            </div>
          </div>
        </div>
      </section>
    </div>
    
    <label>Not here</label>
    <div class="my-5" id="divComment" runat="server">

        <label>GOt here</label>

        <table class="table table-borderless">
            <tr style="height: 120px; vertical-align: middle">
                <td style="width: 900px;" colspan="2">
                <asp:TextBox runat="server" ID="textComentario" CssClass="form-control border-secondary w-75" Height="100" TextMode="MultiLine" />
                </td>
            </tr>

            <tr>
                <td style="width: 150px">
                        <asp:DropDownList ID="listClassificacao" runat="server" Width="100px" CssClass="form-select w-75">
                            <asp:ListItem Text="1" Value="1" />
                            <asp:ListItem Text="2" Value="2" />
                            <asp:ListItem Text="3" Value="3" />
                            <asp:ListItem Text="4" Value="4" />
                            <asp:ListItem Text="5" Value="5" />
                            <asp:ListItem Text="6" Value="6" />
                            <asp:ListItem Text="7" Value="7" />
                            <asp:ListItem Text="8" Value="8" />
                            <asp:ListItem Text="9" Value="9" />
                            <asp:ListItem Text="10" Value="10" />
                        </asp:DropDownList>
                </td>

                <td>
                    <asp:Button Text="Comentar" runat="server" ID="comment_button" OnClick="button_comment" BackColor="#D7D3BF" CssClass="btn ms-3" />
                </td>
            </tr>
        </table>
    </div>


    <script>
        //get the values of the definined properties from the Load event with the values from the table
        var lat = parseFloat('<%= Latitude %>');
        var lng = parseFloat('<%= Longitude %>');
        var nome = '<%= Nome %>';
        var concelho = '<%= Concelho %>';


        if (isNaN(lat)) {

            let mapOptions = {
                center: [39.69484, -8.13031],
                zoom: 6
            }
            let map = new L.map('map', mapOptions);
            let layer = new L.TileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png');
            map.addLayer(layer);
            let marker = null;
        }


        else {

            // Center the map on the coordenates
            var map = L.map('map').setView([lat, lng], 11);
            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 15 }).addTo(map);

            // Add popup
            L.marker([lat, lng]).addTo(map).bindPopup(nome).openPopup();
        }
       
    </script>

</asp:Content>
