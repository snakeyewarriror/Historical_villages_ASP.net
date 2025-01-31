async function WeatherApi(name_local) {
    window.onload = async function () {

        let apiurl_local = 'https://api.ipma.pt/open-data/distrits-islands.json';
        let local_globalid = 0;

        try {
            // Wait for the first fetch to complete
            const response = await fetch(apiurl_local);
            const data = await response.json();
            const data_array = data.data;

            for (let i = 0; i < data_array.length; i++) {
                if (data_array[i]['local'] == name_local) {
                    local_globalid = data_array[i]['globalIdLocal'];
                    break;
                }
            }



            // Fetch weather data after global ID is set
            let apiurl = `https://api.ipma.pt/open-data/forecast/meteorology/cities/daily/${local_globalid}.json`;

            const weatherResponse = await fetch(apiurl);
            const weatherData = await weatherResponse.json();
            const weatherArray = weatherData.data;

            for (let i = 0; i < weatherArray.length; i++) {
                const table = document.getElementById("previsao");
                const row = table.insertRow(-1);

                const cell1 = row.insertCell(0);
                const cell2 = row.insertCell(1);
                const cell3 = row.insertCell(2);
                const cell4 = row.insertCell(3);

                cell1.innerHTML = weatherArray[i].forecastDate;

                if (weatherArray[i].precipitaProb < 25) {
                    cell2.innerHTML = `<img src="Imagens/IPMA/sol.png" alt="sol" width="50">`;
                } else if (weatherArray[i].precipitaProb > 25 && weatherArray[i].precipitaProb < 50) {
                    cell2.innerHTML = `<img src="Imagens/IPMA/nublado.png" alt="nublado" width="50">`;
                } else if (weatherArray[i].precipitaProb > 50 && weatherArray[i].precipitaProb < 75) {
                    cell2.innerHTML = `<img src="Imagens/IPMA/aguaceiros.png" alt="aguaceiros" width="50">`;
                } else if (weatherArray[i].precipitaProb > 75) {
                    cell2.innerHTML = `<img src="Imagens/IPMA/chuva.png" alt="chuva" width="50">`;
                }

                cell3.innerHTML = weatherArray[i].tMin;
                cell4.innerHTML = weatherArray[i].tMax;
            }
        } catch (error) {
            console.error(error);
        }
    };
}
