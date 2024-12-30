const observer = new IntersectionObserver(entries => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            const elements = document.querySelectorAll('.container_text_inside');
            elements[0].classList.add('fadeInLeft');
            elements[1].classList.add('fadeInUp');
        }
    });
});

// Observe the container div
observer.observe(document.querySelector('#container_text'));





/* Weather API */ 

let apiurl

if(window.location.pathname == '/Monsanto.html'){ apiurl = 'https://api.ipma.pt/open-data/forecast/meteorology/cities/daily/1090700.json'; }

else { apiurl = 'https://api.ipma.pt/open-data/forecast/meteorology/cities/daily/1050200.json'; }

fetch(apiurl)
    .then(response => response.json())
    .then(data => {
        const data_array = data.data;

        for (var i=0; i< data_array.length; i++){
            var table = document.getElementById("previsao");

            var row = table.insertRow(-1);

            var cell1 = row.insertCell(0);
            var cell2 = row.insertCell(1);
            var cell3 = row.insertCell(2);
            var cell4 = row.insertCell(3);

            cell1.innerHTML = data_array[i].forecastDate;

            if(data_array[i].precipitaProb < 25) { cell2.innerHTML = `<img src="Imagens/IPMA/sol.png" alt="sol" width="50">`; }

            else if(data_array[i].precipitaProb > 25 && data_array[i].precipitaProb < 50) { cell2.innerHTML = `<img src="Imagens/IPMA/nublado.png" alt="nublado" width="50">`; }

            else if(data_array[i].precipitaProb > 50 && data_array[i].precipitaProb < 75) { cell2.innerHTML = `<img src="Imagens/IPMA/aguaceiros.png" alt="aguaceiros" width="50">`; }

            else if(data_array[i].precipitaProb > 75) { cell2.innerHTML = `<img src="Imagens/IPMA/chuva.png" alt="chuva" width="50">`; }

            cell3.innerHTML = data_array[i].tMin;
            
            cell4.innerHTML = data_array[i].tMax;
        }
    })
    .catch(error => console.log(error))